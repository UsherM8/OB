using Business.ContainerInterfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Mappers;

namespace OnderhoudsbuddyWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarContainer _carContainer;

    public CarController(ICarContainer carContainer)
    {
        _carContainer = carContainer;
    }

    [HttpGet("by-id/{id}")]
    public async Task<ActionResult<CarModel>> GetCarByIdAsync(int id)
    {
        var car = await _carContainer.GetCarByIdAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return Ok(CarModelMapper.FullCar(car));
    }

    [HttpGet("by-license/{licensePlate}")]
    public async Task<ActionResult<CarModel>> GetCarByLicenseAsync(string licensePlate)
    {
        var car = await _carContainer.GetCarByLicenseAsync(licensePlate);
        if (car == null)
        {
            return NotFound();
        }
        return Ok(CarModelMapper.FullCar(car));
    }
    
    [HttpGet("details/{id}")]
    public async Task<ActionResult<CarModel>> GetCarAsync(int id)
    {
        var car = await _carContainer.GetCarAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return Ok(CarModelMapper.ToModel(car));
    }
    
    [HttpPost]
    public async Task<ActionResult> AddCar([FromBody] CarModel carModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var car = CarModelMapper.ToEntity(carModel);
        await _carContainer.AddCarAsync(car);

        return CreatedAtAction(nameof(GetCarAsync), new { id = car.CarId }, carModel);
    }
    
    [HttpPut("{licensePlate}")]
    public async Task<ActionResult> UpdateCar(string licensePlate, [FromBody] CarModel carModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (licensePlate != carModel.LicensePlate)
        {
            return BadRequest("License plate mismatch.");
        }

        var car = CarModelMapper.ToEntity(carModel);
        await _carContainer.UpdateCarAsync(car);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCar(int id)
    {
        await _carContainer.DeleteCarAsync(id);
        return NoContent();
    }
}