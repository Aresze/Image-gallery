using Image_gallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Image_gallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : Controller
    {
        public static IWebHostEnvironment _envorpnment;
        ApplicationDbContext _db;
        public ImageUploadController(IWebHostEnvironment environment, ApplicationDbContext dbContext)
        {
            _envorpnment = environment;
            _db = dbContext;
        }

        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }

        [HttpPost]
        public async Task<string> Post([FromForm] FileUploadAPI obj)
        {
            try
            {
                if (obj.files.Length > 0)
                {
                    if (!Directory.Exists(_envorpnment.WebRootPath + "\\Uploaad\\")) ;
                    {
                        Directory.CreateDirectory(_envorpnment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fs = System.IO.File.Create(_envorpnment.WebRootPath + "\\Upload\\" + obj.files.FileName))
                    {
                        obj.files.CopyTo(fs);
                        fs.Flush();
                        return "\\Upload\\" + obj.files.FileName;
                    }
                }
                else
                    return "Failed";
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

    }
}

