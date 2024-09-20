using Flower_Management.Entities;
using Flower_Management.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flower_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly IFlowerService _flowerService;

        public FlowersController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        // GET: api/flowers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flower>>> GetFlowers()
        {
            var flowers = await _flowerService.GetAllFlowersAsync();
            return Ok(flowers);
        }

        // GET: api/flowers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Flower>> GetFlower(int id)
        {
            var flower = await _flowerService.GetFlowerByIdAsync(id);
            if (flower == null)
            {
                return NotFound();
            }

            return Ok(flower);
        }

        // POST: api/flowers
        [HttpPost]
        public async Task<ActionResult<Flower>> PostFlower([FromBody] Flower flower)
        {
            if (flower == null)
            {
                return BadRequest("Flower cannot be null.");
            }

            await _flowerService.AddFlowerAsync(flower);
            return CreatedAtAction(nameof(GetFlower), new { id = flower.FlowerId }, flower);
        }

        // PUT: api/flowers/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutFlower(int id, [FromBody] Flower flower)
        {
            if (flower == null || id != flower.FlowerId)
            {
                return BadRequest("Invalid flower data.");
            }

            var existingFlower = await _flowerService.GetFlowerByIdAsync(id);
            if (existingFlower == null)
            {
                return NotFound();
            }

            await _flowerService.UpdateFlowerAsync(flower);
            return NoContent();
        }

        // DELETE: api/flowers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFlower(int id)
        {
            var existingFlower = await _flowerService.GetFlowerByIdAsync(id);
            if (existingFlower == null)
            {
                return NotFound();
            }

            await _flowerService.DeleteFlowerAsync(id);
            return NoContent();
        }
    }
}
