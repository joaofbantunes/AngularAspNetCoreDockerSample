using CodingMilitia.AngularAspNetCoreDockerSample.Data;
using CodingMilitia.AngularAspNetCoreDockerSample.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodingMilitia.AngularAspNetCoreDockerSample.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CountersController : Controller
    {
        private readonly CounterContext _db;
        public CountersController(CounterContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Counter>> Get(CancellationToken ct)
        {
            return await _db.Counters.Select(c => new Counter { Id = c.Id, Name = c.Name, Value = c.Value }).ToListAsync(ct);
        }

        [HttpGet("{id}")]
        public async Task<Counter> Get(int id, CancellationToken ct)
        {
            var counter = await _db.Counters.SingleAsync(c => c.Id == id, ct);
            ++counter.Value;
            await _db.SaveChangesAsync(ct);

            return new Counter { Id = counter.Id, Name = counter.Name, Value = counter.Value };
        }
    }
}
