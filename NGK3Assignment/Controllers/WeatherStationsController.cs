using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGK3Assignment.Data;
using NGK3Assignment.Models;

namespace NGK3Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherStationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeatherStationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WeatherStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherStation>>> GetWeatherStations()
        {
            return await _context.WeatherStations.ToListAsync();
        }

        // GET: api/WeatherStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherStation>> GetWeatherStation(string id)
        {
            var weatherStation = await _context.WeatherStations.FindAsync(id);

            if (weatherStation == null)
            {
                return NotFound();
            }

            return weatherStation;
        }

        // PUT: api/WeatherStations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherStation(string id, WeatherStation weatherStation)
        {
            if (id != weatherStation.PlaceId)
            {
                return BadRequest();
            }

            _context.Entry(weatherStation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherStationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherStations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeatherStation>> PostWeatherStation(WeatherStation weatherStation)
        {
            _context.WeatherStations.Add(weatherStation);
            try
            {
                await _context.SaveChangesAsync() ;
            }
            catch (DbUpdateException)
            {
                if (WeatherStationExists(weatherStation.PlaceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWeatherStation", new { id = weatherStation.PlaceId }, weatherStation);
        }

        // DELETE: api/WeatherStations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherStation(string id)
        {
            var weatherStation = await _context.WeatherStations.FindAsync(id);
            if (weatherStation == null)
            {
                return NotFound();
            }

            _context.WeatherStations.Remove(weatherStation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherStationExists(string id)
        {
            return _context.WeatherStations.Any(e => e.PlaceId == id);
        }
    }
}
