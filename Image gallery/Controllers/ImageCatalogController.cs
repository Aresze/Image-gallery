using Image_gallery.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Image_gallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCatalogController : ControllerBase
    {
        ApplicationDbContext db;
        public ImageCatalogController(ApplicationDbContext context)
        {
            db = context;
            if (!db.ImageCatalog.Any())
            {
                db.ImageCatalog.Add(new ImageCatalog { Name = "house"});
                db.ImageCatalog.Add(new ImageCatalog { Name = "dog"});
                db.SaveChanges();
            }

        }

        //Скисок каталогов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageCatalog>>> Get()
        {
            return await db.ImageCatalog.ToListAsync();
        }

        //Получить все изображения с заданного каталога
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Image>>> Get(int id)
        {
            return await db.Images.Where(x => x.CatalogID == id).ToListAsync();
        }

        //Добавить каталог
        [HttpPost]
        public async Task<ActionResult<ImageCatalog>> Post(ImageCatalog catalog)
        {
            if (catalog == null)
            {
                return BadRequest();
            }

            db.ImageCatalog.Add(catalog);
            await db.SaveChangesAsync();
            return Ok(catalog);
        }

        //удалить каталог
        [HttpDelete("{id}")]
        public async Task<ActionResult<ImageCatalog>> Delete(int id)
        {
            ImageCatalog catalog= db.ImageCatalog.FirstOrDefault(x => x.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }
            db.ImageCatalog.Remove(catalog);
            await db.SaveChangesAsync();
            return Ok(catalog);
        }

        //изменить каталог
        [HttpPut]
        public async Task<ActionResult<ImageCatalog>> Put(ImageCatalog catalog)
        {
            if (catalog == null)
            {
                return BadRequest();
            }
            if (!db.Images.Any(x => x.Id == catalog.Id))
            {
                return NotFound();
            }

            db.Update(catalog);
            await db.SaveChangesAsync();
            return Ok(catalog);
        }

    }
}
