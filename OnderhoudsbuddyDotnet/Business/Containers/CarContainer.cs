﻿using Business.Classes;
using Business.ContainerInterfaces;
using Business.Mappers;
using Domain.Dtos;
using Domain.RepositoryInterfaces;

namespace Business.Containers;

public class CarContainer : ICarContainer
{
    private readonly ICar _carRepository;

    public CarContainer(ICar carRepository)
    {
        _carRepository = carRepository;
    }
    
    public async Task<Car?> GetCarByIdAsync(int id)
    {
        var carDto = await _carRepository.GetCarByIdAsync(id);
        Car car = CarMapper.MapFromRdw(carDto!);
        return car;
    }

    public async Task<Car?> GetCarByLicenseAsync(string licensePlate)
    {
        var carRdwDto = await _carRepository.GetCarByLicenseAsync(licensePlate);
        if (carRdwDto == null)
        {
            return new Car();
        }
        Car car = CarMapper.MapFromRdw(carRdwDto!);
        return car;
    }

    public async Task<Car?> GetCarAsync(int id)
    {
        var carDto = await _carRepository.GetCarAsync(id);
        Car car = CarMapper.ToEntity(carDto!);
        return car;
    }

    public async Task AddCarAsync(int userId, string licensePlate, int mileage)
    {
        await _carRepository.AddCarAsync( userId,  licensePlate, mileage);
    }

    public async Task UpdateCarAsync(Car car)
    {
        var carDto = CarMapper.ToDto(car);
        await _carRepository.UpdateCarAsync(carDto);
    }

    public async Task DeleteCarAsync(int id)
    {
        await _carRepository.DeleteCarAsync(id);
    }
    public async Task<List<Car>> GetAllFullCarsForUserAsync(int userId)
    {
        var carDtos = await _carRepository.GetAllFullCarsForUserAsync(userId);
        return carDtos.Select(CarMapper.MapFromRdw).ToList();
    }

    public async Task<Car?> GetCarForUserAsync(int userId, int carId)
    {
        var carDto = await _carRepository.GetCarForUserAsync(userId, carId);
        return carDto != null ? CarMapper.MapFromRdw(carDto) : null;
    }
}