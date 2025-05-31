using Business.ContainerInterfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Mappers;
using Models.Models;

namespace OnderhoudsbuddyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarageController : ControllerBase
    {
        private readonly IGarageContainer _garageContainer;

        public GarageController(IGarageContainer garageContainer)
        {
            _garageContainer = garageContainer;
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<GarageModel>> GetGarageByIdAsync(int id)
        {
            var garage = await _garageContainer.GetGarageByIdAsync(id);
            if (garage == null)
            {
                return NotFound();
            }
            return Ok(GarageModelMapper.ToModel(garage));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GarageModel>>> GetAllGaragesAsync()
        {
            var garages = await _garageContainer.GetAllGaragesAsync();
            if (garages == null || !garages.Any())
            {
                return NotFound();
            }
            return Ok(garages.Select(GarageModelMapper.ToModel));
        }

        [HttpPost]
        public async Task<ActionResult<GarageModel>> CreateGarageAsync([FromBody] GarageModel garageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var garage = GarageModelMapper.ToEntity(garageModel);
            await _garageContainer.CreateGarageAsync(garage);
            return Ok(garageModel);
        }
    }
}
