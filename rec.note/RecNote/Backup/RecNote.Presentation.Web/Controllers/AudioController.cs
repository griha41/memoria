using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Audios;
using RecNote.Domain.Core.Audios;
using RecNote.Domain.Core.Files;
using Model = RecNote.Presentation.Web.Models.Audio;

using Lib.Web.Mvc;

namespace RecNote.Presentation.Web.Controllers
{
    public class AudioController : Controller
    {
        //
        // GET: /Audio/
        IAudioProvider AudioProvider { get; set; }
        IFileProvider FileProvider { get; set; }
        int InitialSecondsDelay { get; set; }
        float Similarity { get; set; }
        

        public ActionResult List(string projectID)
        {
            var audios = this.AudioProvider.FindByProjectID(projectID);
            return View(audios);
        }

        public ActionResult Remove(string audioID)
        {
            return Json(this.AudioProvider.Remove(audioID), JsonRequestBehavior.AllowGet);
        }

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

        public ActionResult Reader(string audioID)
        {
            var audio = this.AudioProvider.FindByID(audioID);
            var file = this.FileProvider.FindByID(audio.FileID);
            return new Lib.Web.Mvc.RangeFilePathResult(file.ContentType, file.FullPath, audio.Date, file.Size);
        }

        public ActionResult Play(string audioID, int? init, int? end)
        {
            return View(new Model.Play
            {
                Audio = this.AudioProvider.FindByID(audioID),
                Init = init,
                End = end
            });
        }

        public ActionResult New(string projectID)
        {
            return View(new Model.New
            {
                ProjectID = projectID,
                Audio = new Audio()
            });
        }

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

        public ActionResult Search()
        {

            return View();
        }

    }
}
