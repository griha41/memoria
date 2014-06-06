using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Audio;
using RecNote.Domain.Core.Audio;
using RecNote.Domain.Core.Files;
using RecNote.Domain.Core.Projects;
using RecNote.Entities.Projects;
using Model = RecNote.Presentation.Web.Models.Audio;

using Lib.Web.Mvc;

namespace RecNote.Presentation.Web.Controllers
{
    /// <summary>
    /// Controladore de audios
    /// </summary>
    public class AudioController : Controller
    {
        //
        // GET: /Audio/
        IAudioProvider AudioProvider { get; set; }
        IFileProvider FileProvider { get; set; }
        IProjectProvider ProjectProvider { get; set; }
        int InitialSecondsDelay { get; set; }
        float Similarity { get; set; }
        protected RoleType GetRole(string projectID)
        {
            return this.ProjectProvider.GetRole(projectID, MvcApplication.CurrentUser.ID);
        }

        /// <summary>
        /// Lista los audios por proyecto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <returns></returns>
        public ActionResult List(string projectID)
        {
            if(this.GetRole(projectID) == RoleType.Developer)
                return View(this.AudioProvider.FindByProjectID(projectID));
            else
                return View("ListLimited", this.AudioProvider.FindByProjectID(projectID));
        }
        /// <summary>
        /// Elimina un audio del proyecto
        /// </summary>
        /// <param name="audioID">Identificador del audio</param>
        /// <returns></returns>
        public ActionResult Remove(string audioID)
        {
            return Json(this.AudioProvider.Remove(audioID), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Lista resultados de similaridad
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="fileID">Identificador del audio subido para comparación</param>
        /// <param name="similarity">Similaridad mínima</param>
        /// <returns></returns>
        public ActionResult Result(string projectID, string fileID, float? similarity)
        {
            if (!similarity.HasValue) { similarity = this.Similarity; }
            var files = this.AudioProvider.FindByProjectID(projectID);
            var list = new List<Model.Partial>();
            
            foreach (var file in files)
            {
                var result = this.AudioProvider.Compare(file.ID, fileID, (float)similarity).items.Select(
                    p =>
                        new Model.Partial
                        {
                            Init = (int)(p.MillisecondFile1/1000) - this.InitialSecondsDelay,
                            End = (int)(p.MillisecondFile1/1000) + this.InitialSecondsDelay,
                            Audio = file,
                            Similitary = p.Similitary
                        }
                        ).ToList();
                Model.Partial last = null;
                foreach (var c in result.ToArray())
                {
                    if (last != null)
                    {
                        if (last.End + this.InitialSecondsDelay >= c.Init)
                        {
                            last.End = c.End;
                            last.Similitary = Math.Max(last.Similitary, c.Similitary);
                            result.Remove(c);
                            continue;
                        }
                    }
                    else
                        last = c;
                }
                

                list.AddRange(result);
            }
            return View(list);
        }
        /// <summary>
        /// Lee un audio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public ActionResult Reader(string audioID)
        {
            var audio = this.AudioProvider.FindByID(audioID);
            var file = this.FileProvider.FindByID(audio.FileID);
            return new Lib.Web.Mvc.RangeFilePathResult(file.ContentType, file.FullPath, audio.Date, file.Size);
        }
        /// <summary>
        /// Lee un audio según un inicio en tiempo y fin
        /// </summary>
        /// <param name="audioID"></param>
        /// <param name="init"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ActionResult Play(string audioID, int? init, int? end)
        {
            return View(new Model.Play
            {
                Audio = this.AudioProvider.FindByID(audioID),
                Init = init,
                End = end
            });
        }
        /// <summary>
        /// Pantalla de nuevo audio
        /// </summary>
        /// <param name="projectID">Identificador de audios</param>
        /// <returns></returns>
        public ActionResult New(string projectID)
        {
            return View(new Model.New
            {
                ProjectID = projectID,
                Audio = new Audio()
            });
        }
        /// <summary>
        /// Agrega audio al proyecto
        /// </summary>
        /// <param name="projectID">Identificador de proyecto</param>
        /// <param name="fileID">Identificador del archivo</param>
        /// <param name="audioName">Nombre del audio</param>
        /// <returns></returns>
        public ActionResult Append(string projectID, string fileID, string audioName)
        {
            var audio = this.AudioProvider.Save(new Audio
            {
                Date = DateTime.Now,
                FileID = fileID,
                ProjectID = projectID,
                Name = audioName
            });
            return Json(audio);
        }
        /// <summary>
        /// Pantalla de busqueda
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {

            return View();
        }

    }
}
