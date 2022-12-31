using AspWebApi.Data;
using AspWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DemoController : Controller
    {
        private readonly DemoApiDB dbContext;

        public DemoController(DemoApiDB dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult>  Getdemo()
        {
            return Ok(await dbContext.ClassDemos.ToListAsync()); 
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> Getdm([FromRoute] Guid id)
        {
            var demo = await dbContext.ClassDemos.FindAsync(id);
            if (demo == null)
            {
                return NotFound();
            }
            return Ok(demo);
        }

        [HttpPost]
        public async Task<IActionResult> AddDemo(AddDemocls addDemocls)
        {
            var demo = new ClassDemo()
            {
                Id = Guid.NewGuid(),
                Name = addDemocls.Name,
                Email = addDemocls.Email
            };
            await dbContext.ClassDemos.AddAsync(demo);
            await dbContext.SaveChangesAsync();

            return Ok(demo);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDemo([FromRoute] Guid id,UpdateDemocls updateDemocls)
        {
            var demo = await dbContext.ClassDemos.FindAsync(id);

            if (demo != null)
            {
                demo.Name = updateDemocls.Name;
                demo.Email = updateDemocls.Email;

                await dbContext.SaveChangesAsync();
                return Ok(demo);
                

            }
            return NotFound();
            
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteDemo([FromRoute] Guid id)
        {
            var demo = await dbContext.ClassDemos.FindAsync(id);
            if (demo != null)
            {
                dbContext.Remove(demo);
                await dbContext.SaveChangesAsync();
                return Ok(demo);
            }

            return NotFound();
        }
    }
}
