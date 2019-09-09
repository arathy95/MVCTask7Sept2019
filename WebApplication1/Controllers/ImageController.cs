using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace WebApplication1.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult ImageUpload()
        {
            ViewBag.files = ReadImages();
            return View();
        }

        [HttpPost]
        public ActionResult ImageUpload(HttpPostedFileBase[] fileBase)
        {
            try
            {
                string DirPath = Server.MapPath("~/Content/Images/");

                if (!Directory.Exists(DirPath))
                {
                    Directory.CreateDirectory(DirPath);
                }
                foreach (var file in fileBase)
                {
                    string fname = Guid.NewGuid().ToString().Replace('-', '_');
                    string[] arr = file.FileName.Split('.');
                    string ext = arr[arr.Length - 1];
                    string filename = string.Format("img_{0}.{1}", fname, ext);
                    string filepath = DirPath + "\\" + filename;
                    file.SaveAs(filepath);
                }

                ViewBag.message = "Image uploaded.";

                ViewBag.files = ReadImages();
            }

            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
            }

            return View();
        }

        public List<string> ReadImages()
        {
            string[] PhysicalPath = Directory.GetFiles(Server.MapPath("~/Content/Images/"));
            List<string> filepath = new List<string>();

            foreach (var item in PhysicalPath)
            {
                string[] array = item.Split('\\');
                string fn = array[array.Length - 1];
                filepath.Add("/Content/Images/" + fn);
            }

            return filepath;
        }
    }
}