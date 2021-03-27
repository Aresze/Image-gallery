using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Image_gallery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace Image_gallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageGalleryController : ControllerBase
    {
        ApplicationDbContext db;
        public ImageGalleryController(ApplicationDbContext context)
        {
            db = context;
            if (!db.Images.Any())
            {
                db.Images.Add(new Image { ImagePath = "test\\path", Name = "Image 1", Description = "image 1", CatalogID = 1 });
                db.Images.Add(new Image { ImagePath = "test\\path", Name = "Image 1", Description = "image 2", CatalogID = 2 });
                db.Images.Add(new Image { ImagePath = "test\\path", Name = "Image 1", Description = "image 3", CatalogID = 1 });
                db.SaveChanges();
            }
        }

        //получить все изображения
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> Get()
        {
            return await db.Images.ToListAsync();
        }

        //получить заданное по ID изображение
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> Get(int id)
        {
            Image img = await db.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (img == null)
                return NotFound();
            return new ObjectResult(img);
        }

        //добавить изображение
        [HttpPost]
        public async Task<ActionResult<Image>> Post([FromForm] Image img)
        {
            if (img == null)
            {
                return BadRequest();
            }

            db.Images.Add(img);
            await db.SaveChangesAsync();
            return Ok(img);
        }

        //удалить изображение
        [HttpDelete("{id}")]
        public async Task<ActionResult<Image>> Delete(int id)
        {
            Image img = db.Images.FirstOrDefault(x => x.Id == id);
            if (img == null)
            {
                return NotFound();
            }
            db.Images.Remove(img);
            await db.SaveChangesAsync();
            return Ok(img);
        }

        //изменить изображение
        [HttpPut]
        public async Task<ActionResult<Image>> Put(Image img)
        {
            if (img == null)
            {
                return BadRequest();
            }
            if (!db.Images.Any(x => x.Id == img.Id))
            {
                return NotFound();
            }

            db.Update(img);
            await db.SaveChangesAsync();
            return Ok(img);
        }
    }
}
