using Dal.Services;
using Domain.Dtos;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class CarRepository : ICar
{
    private readonly OnderhoudsbuddyDbContext _context;
    private readonly RdwApiClient _rdwApiClient;

    public CarRepository(OnderhoudsbuddyDbContext context, RdwApiClient rdwApiClient)
    {
        _context = context;
        _rdwApiClient = rdwApiClient;
    }

    public async Task<RdwCarDto?> GetCarByIdAsync(int id)
    {   
        CarDto carDto = await _context.Cars.FindAsync(id);
        RdwCarDto rdwCarDto = await _rdwApiClient.GetCarAsync(carDto.LicencePlate); 
        return rdwCarDto;
    }
    
    public async Task<RdwCarDto?> GetCarByLicenseAsync(string licensePlate)
    {
        RdwCarDto rdwCarDto = await _rdwApiClient.GetCarAsync(licensePlate); 
        return rdwCarDto;
    }

    public async Task<CarDto?> GetCarAsync(int id)
    {
        CarDto carDto = await _context.Cars.FindAsync(id);
        return carDto;
    }

    public async Task AddCarAsync(CarDto carDto)
    {
        await _context.Cars.AddAsync(carDto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCarAsync(CarDto carDto)
    {
        _context.Cars.Update(carDto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCarAsync(int id)
    {
        var carDto = await GetCarAsync(id);
        if (carDto != null)
        {
            _context.Cars.Remove(carDto);
            await _context.SaveChangesAsync();
        }
    }
}