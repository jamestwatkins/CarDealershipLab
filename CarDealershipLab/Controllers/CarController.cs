using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly CarsDbContext _context;
        public CarController(CarsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cars>>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();

            return cars;
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<Cars>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                return car;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cars>> AddCar(Cars newCar)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(newCar);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCar(int id, Cars updatedCar)
        {
            if (id != updatedCar.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(updatedCar).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}   
