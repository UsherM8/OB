using Business.Classes;

namespace Business.ContainerInterfaces;

public interface ICarContainer
{
    Task<Car?> GetCarByIdAsync(int id);
    Task<Car?> GetCarByLicenseAsync(string licensePlate);
    Task<Car?> GetCarAsync(int id);
    Task AddCarAsync(Car car);
    Task UpdateCarAsync(Car car);
    Task DeleteCarAsync(int id);
}