using Domain.Dtos;

namespace Domain.RepositoryInterfaces;

public interface ICar
{
    Task<RdwCarDto?> GetCarByIdAsync(int id);
    Task<RdwCarDto?> GetCarByLicenseAsync(string licensePlate);
    Task<CarDto?> GetCarAsync(int id);
    Task AddCarAsync(CarDto carDto);
    Task UpdateCarAsync(CarDto carDto);
    Task DeleteCarAsync(int id);
}