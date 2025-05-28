
using Microsoft.AspNetCore.Mvc;
using Models.Mappers;
using Models.Models;
using Business.ContainerInterfaces;
using Business.Containers;

namespace OnderhoudsbuddyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceContainer _serviceContainer; 

        public ServiceController(IServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<ServiceModel>> GetServiceByIdAsync(int id)
        {
            var service = await _serviceContainer.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(ServiceModelMapper.ToModel(service));
        }

        [HttpGet("Getallby-id/{id}")]
        public async Task<ActionResult<IEnumerable<ServiceModel>>> GetAllServicesAsync(int id)
        {
            var services = await _serviceContainer.GetAllServicesAsync(id);
            if (services == null || !services.Any())
            {
                return NotFound();
            }
            var serviceModels = services.Select(ServiceModelMapper.ToModel).ToList();
            return Ok(serviceModels);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceModel>> CreateGarageAsync([FromBody] ServiceModel serviceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = ServiceModelMapper.ToEntity(serviceModel);
            await _serviceContainer.CreateServiceAsync(service);
            return CreatedAtAction(nameof(GetServiceByIdAsync), new { id = service.ServiceId }, ServiceModelMapper.ToModel(service));
        }
    }
}
