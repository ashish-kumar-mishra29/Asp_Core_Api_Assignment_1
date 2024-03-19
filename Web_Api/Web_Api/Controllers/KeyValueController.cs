using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web_Api.Data;
using Web_Api.Entities;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyValueController : ControllerBase
    {
        private readonly DataContext _context;

        public KeyValueController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<KeyValue>> GetKeyValues()
        {
            var pair = await _context.KeyValues.ToListAsync();
            return Ok(pair);
        }

        [HttpGet("{Key}")]
        public async Task<ActionResult<KeyValue>> GetKeyValue( string Key)
        {
            var pair = await _context.KeyValues.FindAsync(Key);// Find async we used to find the specific value through key
            if(pair == null)
            {
                return StatusCode(404 ,"Not Found");
            }
            return Ok(pair);
        }

        [HttpPost]
        public async Task<ActionResult<KeyValue>> AddKey([FromBody]KeyValue pair)
        {
            var existingKey = await _context.KeyValues.FirstOrDefaultAsync(kv => kv.Key == pair.Key);

            if (existingKey != null)
            {
                // Key already exists, return a customized error message
                return StatusCode(409,"Conflict Code");
            }
            _context.KeyValues.Add(pair);
            await _context.SaveChangesAsync();//This will save the changes in the database
            
            return Ok(await _context.KeyValues.ToListAsync());
        }

        [HttpPatch("{Key}")]
        public async Task<ActionResult<KeyValue>> UpdateKey( KeyValue updatedPair)
        {
            var dbPair = await _context.KeyValues.FindAsync(updatedPair.Key);// Find async we used to find the specific value through key
            if (dbPair == null)
            {
                return StatusCode(404, "Not Found");
            }

            dbPair.Value = updatedPair.Value;
            await _context.SaveChangesAsync();
            return Ok(updatedPair);
        }

        [HttpDelete("{Key}")]
        public async Task<ActionResult<KeyValue>> DeleteKey(string Key)
        {
            var dbPair = await _context.KeyValues.FindAsync(Key);// Find async we used to find the specific value through key
            if (dbPair == null)
            {
                return StatusCode(404, "Not Found");
            }
            _context.KeyValues.Remove(dbPair);
            await _context.SaveChangesAsync();
            return Ok(await _context.KeyValues.ToListAsync());
        }
    }
}
