using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageMagick;

namespace MagickConvert.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Convert(string id)
        {
            Response.BufferOutput = false;
            Response.ContentType = MimeMapping.GetMimeMapping(id);
            Response.AppendHeader("content-disposition", $"attachment; filename=\"sample.{id}\"");
            TransformImage(Response.OutputStream, MagickFormatInfo.Create($"sample.{id}").Format);
            return new EmptyResult();
        }

        private void TransformImage(Stream stream, MagickFormat format)
        {
            using (MagickImage image = new MagickImage(HttpContext.Server.MapPath("~/test.png")))
            {
                image.Format = format;
                image.Write(stream, format);
            }
        }
    }
}
