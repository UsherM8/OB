using Domain.Dtos;

namespace Domain.RepositoryInterfaces;

public interface ICar
{
    Task<CarDto?> GetCarByIdAsync(int id);
    Task<CarDto?> GetCarByLicenseAsync(string licensePlate);
    Task<CarDto?> GetCarAsync(int id);
    Task AddCarAsync(int userId, string licensePlate, int mileage);
    Task<List<CarDto>> GetAllFullCarsForUserAsync(int userId);
    Task<CarDto?> GetCarForUserAsync(int userId, int carId);
    Task UpdateCarAsync(CarDto carDto);
    Task DeleteCarAsync(int id);
}