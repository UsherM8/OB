using Business.Classes;

namespace Business.ContainerInterfaces;

public interface ICarContainer
{
    Task<Car?> GetCarByIdAsync(int id);
    Task<Car?> GetCarByLicenseAsync(string licensePlate);
    Task<Car?> GetCarAsync(int id);
    Task AddCarAsync(int userId, string licensePlate, int mileage);
    Task UpdateCarAsync(Car car);
    Task DeleteCarAsync(int id);
    Task<List<Car>> GetAllFullCarsForUserAsync(int userId);
    Task<Car?> GetCarForUserAsync(int userId, int carId);
}
