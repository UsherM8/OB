using Business.Classes;

namespace Business.ContainerInterfaces;

public interface ICarContainer
{
    Task<Car?> GetCarAsync(string licensePlate);
    Task AddCarAsync(Car car);
    Task UpdateCarAsync(Car car);
    Task DeleteCarAsync(string licensePlate);
}