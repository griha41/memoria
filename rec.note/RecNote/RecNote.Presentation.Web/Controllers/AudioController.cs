using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Audios;
using RecNote.Domain.Core.Audios;
using RecNote.Domain.Core.Files;
using Model = RecNote.Presentation.Web.Models.Audio;

namespace RecNote.Presentation.Web.Controllers
{
    public class AudioController : Controller
    {
        //
        // GET: /Audio/
        IAudioProvider AudioProvider { get; set; }
        IFileProvider FileProvider { get; set; }
        

        public ActionResult List(string projectID)
        {
            var audios = this.AudioProvider.FindByProjectID(projectID);
            return View(audios);
        }

        public ActionResult Remove(string audioID)
        {
            return Json(this.AudioProvider.Remove(audioID), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Reader(string audioID)
        {
            var audio = this.AudioProvider.FindByID(audioID);
            var file = this.FileProvider.FindByID(audio.FileID);
            return File(this.FileProvider.GetFile(audio.FileID), file.ContentType, file.Name);
        }

        public ActionResult Play(string audioID)
        {
            return View(this.AudioProvider.FindByID(audioID));
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

    }
}
