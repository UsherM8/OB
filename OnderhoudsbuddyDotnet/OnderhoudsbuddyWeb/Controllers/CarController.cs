using Business.ContainerInterfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Mappers;
// Voor CarModel
// Voor CarModelMapper

// Voor ICarContainer

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

    // GET: api/car/{licensePlate}
    [HttpGet("{licensePlate}")]
    public async Task<ActionResult<CarModel>> GetCar(string licensePlate)
    {
        var car = await _carContainer.GetCarAsync(licensePlate);
        if (car == null)
        {
            return NotFound(); // Return 404 als de auto niet gevonden is
        }

        return Ok(CarModelMapper.ToModel(car)); // Converteer het response naar CarModel
    }

    // POST: api/car
    [HttpPost]
    public async Task<ActionResult> AddCar([FromBody] CarModel carModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return 400 als het model niet valide is
        }

        var car = CarModelMapper.ToEntity(carModel); // Map het model naar de businesslaag
        await _carContainer.AddCarAsync(car);

        return CreatedAtAction(nameof(GetCar), new { licensePlate = car.LicensePlate }, carModel); // Return 201 Created
    }

    // PUT: api/car/{licensePlate}
    [HttpPut("{licensePlate}")]
    public async Task<ActionResult> UpdateCar(string licensePlate, [FromBody] CarModel carModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return 400 bij ongeldige data
        }

        if (licensePlate != carModel.LicensePlate)
        {
            return BadRequest("License plate mismatch."); // Controleer of de license plate klopt
        }

        var car = CarModelMapper.ToEntity(carModel); // Map naar een business object
        await _carContainer.UpdateCarAsync(car);

        return NoContent(); // Return 204 No Content als de update succesvol is
    }

    // DELETE: api/car/{licensePlate}
    [HttpDelete("{licensePlate}")]
    public async Task<ActionResult> DeleteCar(string licensePlate)
    {
        await _carContainer.DeleteCarAsync(licensePlate);
        return NoContent(); // Return 204 als delete succesvol is
    }
}