using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NGK3Assignment.Data;
using NGK3Assignment.Hubs;
using NGK3Assignment.Models;
using Xunit.Sdk;

namespace NGK3Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherStationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<SubcriberHub> _hubContext;

        public WeatherStationsController(AppDbContext context, IHubContext<SubcriberHub> subscriberHub)
        {
            _context = context;
            _hubContext = subscriberHub;
        }

        // GET: api/WeatherStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherStation>>> GetWeatherStations()
        {
            return await _context.WeatherStations.ToListAsync();
        }

        // GET: api/WeatherStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherStation>> GetWeatherStation(int id)
        {
            var weatherStation = await _context.WeatherStations.FindAsync(id);

            if (weatherStation == null)
            {
                return NotFound();
            }

            return weatherStation;
        }

        // GET: api/WeatherStations/latest
        [HttpGet("latest")]
        public async Task<ActionResult<IEnumerable<WeatherStation>>> GetLatestWeatherStations()
        {
            int first = 0;
            int second = 0;
            int third = 0;

            List<WeatherStation> list = new List<WeatherStation>();

            foreach (var v in _context.WeatherStations)
            {
                if (v.PlaceId > first)
                {
                    third = second;
                    second = first;
                    first = v.PlaceId;
                }
                else if (v.PlaceId > second)
                {
                    third = second;
                    second = v.PlaceId;
                }
                else if (v.PlaceId > third)
                {
                    third = v.PlaceId;
                }
            }

            list.Add(await _context.WeatherStations.FindAsync(first));
            list.Add(await _context.WeatherStations.FindAsync(second));
            list.Add(await _context.WeatherStations.FindAsync(third));

            return list;
        }

        // GET: api/WeatherStations/today/{date}
        [HttpGet("today/{date}")]
        public async Task<List<WeatherStation>> GetTodaysWeatherStations(string date)
        {
            var today = await _context.WeatherStations.Where(d => d.Date == DateTime.Parse(date)).ToListAsync();

            return today;
        }

        // GET: api/WeatherStations/today/{date}
        [HttpGet("timeinterval/{start}, {end}")]
        public async Task<List<WeatherStation>> GetTimeIntervalWeatherStations(DateTime start, DateTime end)
        {
            var timeinterval = await _context.WeatherStations.Where(d => d.Date >= start && d.Date <= end).ToListAsync();

            return timeinterval;
        }


        // PUT: api/WeatherStations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherStation(int id, WeatherStation weatherStation)
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
        [Authorize]
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

            await _hubContext.Clients.All.SendAsync("weatherUpdate", weatherStation);

            return CreatedAtAction("GetWeatherStation", new { id = weatherStation.PlaceId }, weatherStation);
        }

        // DELETE: api/WeatherStations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherStation(int id)
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

        private bool WeatherStationExists(int id)
        {
            return _context.WeatherStations.Any(e => e.PlaceId == id);
        }
    }
}
