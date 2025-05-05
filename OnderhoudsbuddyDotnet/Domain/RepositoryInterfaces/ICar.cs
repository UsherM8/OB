using Domain.Dtos;

namespace Domain.RepositoryInterfaces;

public interface ICar
{
    Task<CarDto?> GetCarAsync(string identifier);
    Task AddCarAsync(CarDto carDto);
    Task UpdateCarAsync(CarDto carDto);
    Task DeleteCarAsync(string identifier);
}