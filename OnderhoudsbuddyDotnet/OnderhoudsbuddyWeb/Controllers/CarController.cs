﻿using Business.ContainerInterfaces;
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
    
   [HttpPost("user/{userId}")]
    public async Task<ActionResult<CarModel>> AddCar(int userId, [FromBody] CarModel carModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var car = CarModelMapper.ToEntity(carModel);
        await _carContainer.AddCarAsync(userId, car.LicensePlate, car.Mileage);
        return Ok();
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

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCar(int id)
    {
        await _carContainer.DeleteCarAsync(id);
        return NoContent();
    }
    [HttpGet("user/{userId}/cars")]
    public async Task<ActionResult<List<CarModel>>> GetAllFullCarsForUserAsync(int userId)
    {
        var cars = await _carContainer.GetAllFullCarsForUserAsync(userId);
        if (cars == null || !cars.Any())
            return NotFound();

        var carModels = cars.Select(CarModelMapper.FullCar).ToList();
        return Ok(carModels);
    }

    [HttpGet("user/{userId}/car/{carId}")]
    public async Task<ActionResult<CarModel>> GetCarForUserAsync(int userId, int carId)
    {
        var car = await _carContainer.GetCarForUserAsync(userId, carId);
        if (car == null)
            return NotFound();

        return Ok(CarModelMapper.FullCar(car));
    }
}