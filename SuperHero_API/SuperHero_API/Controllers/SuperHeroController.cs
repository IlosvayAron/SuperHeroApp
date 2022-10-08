using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero_API.Data;
using SuperHero_API.Models;

namespace SuperHero_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHero()
        {
            return Ok(await _context.SuperHeros.ToListAsync()); 
            //Mock data
            //return new List<SuperHero> 
            //{
            //    new SuperHero
            //    {
            //        Name = "Ironman",
            //        FirstName = "Tony",
            //        LastName = "Stark",
            //        Place = "Long Island"
            //    },
            //    new SuperHero
            //    {
            //        Name = "Spider-Man",
            //        FirstName = "Peter",
            //        LastName = "Parker",
            //        Place = "New York City"
            //    },
            //};
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero)
        {
            var dbHero = await _context.SuperHeros.FindAsync(hero.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not exists!");
            }
            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var dbHero = _context.SuperHeros.Find(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found!");
            }
            _context.SuperHeros.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
