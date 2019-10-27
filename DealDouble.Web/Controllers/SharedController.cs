using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DealDouble.Entities;
using DealDoubleServices;

namespace DealDouble.Web.Controllers
{
    public class SharedController : Controller
    {
        SharedServices service = new SharedServices();
        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();
            List<object> picturesJSON = new List<object>();

            var pictures = Request.Files;
            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];
             
                var filename =Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var path = Server.MapPath("~/Content/images/") + filename;
                picture.SaveAs(path);

                Picture dbPicture = new Picture();
                dbPicture.URL = filename;

                int pictureID = service.SavePicture(dbPicture);

                picturesJSON.Add(new { ID = pictureID, pictureURL = filename });
            }
            result.Data=picturesJSON;
            return result;
        }
    }
}