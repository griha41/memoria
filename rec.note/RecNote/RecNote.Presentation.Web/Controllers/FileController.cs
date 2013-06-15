using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Domain.Core.Files;

namespace RecNote.Presentation.Web.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/

        IFileProvider FileProvider { get; set; }


        public ActionResult Upload()
        {
            var currentFile = Request.Files[0];
            var bytes = new byte[currentFile.ContentLength];
            currentFile.InputStream.Read(bytes, 0, currentFile.ContentLength);

            var file = this.FileProvider.Upload(currentFile.FileName, currentFile.ContentType, bytes);
            file.Url = Url.Action("Reader", "File", new { fileID = file.ID }, "http");
            return Json(new 
                { 
                    file = 
                        new { 
                            name = file.Name,
                            size = file.Size,
                            url = file.Url,
                            id = file.ID
                        } 
                }
                );
        }

        public ActionResult Reader(string fileID)
        {
            var file = this.FileProvider.FindByID(fileID);
            return File(this.FileProvider.GetFile(fileID), file.ContentType, file.Name);
        }

    }
}
