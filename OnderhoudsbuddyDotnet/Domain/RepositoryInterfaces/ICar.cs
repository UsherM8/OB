using Domain.Dtos;

namespace Domain.RepositoryInterfaces;

public interface ICar
{
    Task<CarDto?> GetCarByIdAsync(int id);
    Task<CarDto?> GetCarByLicenseAsync(string licensePlate);
    Task<CarDto?> GetCarAsync(int id);
    Task AddCarAsync(CarDto carDto);
    Task UpdateCarAsync(CarDto carDto);
    Task DeleteCarAsync(int id);
}