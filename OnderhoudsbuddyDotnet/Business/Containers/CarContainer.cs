using Business.Classes;
using Business.ContainerInterfaces;
using Business.Mappers;
using Domain.RepositoryInterfaces;

namespace Business.Containers;

public class CarContainer : ICarContainer
{
    private readonly ICar _carRepository;

    public CarContainer(ICar carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Car?> GetCarAsync(string licensePlate)
    {
        var carDto = await _carRepository.GetCarAsync(licensePlate);
        return carDto != null ? CarMapper.ToEntity(carDto) : null;
    }

    public async Task AddCarAsync(Car car)
    {
        var carDto = CarMapper.ToDto(car);
        await _carRepository.AddCarAsync(carDto);
    }

    public async Task UpdateCarAsync(Car car)
    {
        var carDto = CarMapper.ToDto(car);
        await _carRepository.UpdateCarAsync(carDto);
    }

    public async Task DeleteCarAsync(string licensePlate)
    {
        await _carRepository.DeleteCarAsync(licensePlate);
    }
}