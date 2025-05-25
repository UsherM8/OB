using Dal.Entities;
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

    public async Task<CarDto?> GetCarByIdAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return null;
        var carDto = await _rdwApiClient.GetCarAsync(car.LicensePlate);
        if (carDto == null) return null;
        carDto.CarId = car.CarId;
        carDto.Mileage = car.Mileage;
        return carDto;
    }

    public async Task<CarDto?> GetCarByLicenseAsync(string licensePlate)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.LicensePlate == licensePlate);
        if (car == null) return null;
        var carDto = await _rdwApiClient.GetCarAsync(licensePlate);
        if (carDto == null) return null;
        carDto.CarId = car.CarId;
        carDto.Mileage = car.Mileage;
        return carDto;
    }

    public async Task<CarDto?> GetCarForUserAsync(int userId, int carId)
    {
        var userCar = await _context.UserCars
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CarId == carId);

        if (userCar == null)
            return null;

        var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.CarId == carId);

        if (car == null)
            return null;

        var carDto = await _rdwApiClient.GetCarAsync(car.LicensePlate);
        if (carDto == null) return null;
        carDto.CarId = car.CarId;
        carDto.Mileage = car.Mileage;
        return carDto;
    }

    public async Task<List<CarDto>> GetAllFullCarsForUserAsync(int userId)
    {
        var cars = await _context.UserCars
            .Where(uc => uc.UserId == userId)
            .Join(
                _context.Cars,
                uc => uc.CarId,
                c => c.CarId,
                (uc, c) => c
            )
            .ToListAsync();

        var carTasks = cars
            .Select(async car =>
            {
                var carDto = await _rdwApiClient.GetCarAsync(car.LicensePlate);
                if (carDto == null) return null;
                carDto.CarId = car.CarId;      // Set the local CarId
                carDto.Mileage = car.Mileage;  // Set the local Mileage
                return carDto;
            })
            .ToList();

        var fullCars = await Task.WhenAll(carTasks);

        return fullCars.Where(car => car != null).ToList()!;
    }

    public async Task<CarDto?> GetCarAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        if (car == null) return null;
        var carDto = new CarDto
        {
            CarId = car.CarId,
            LicensePlate = car.LicensePlate,
            Mileage = car.Mileage
        };
        return carDto;
    }

    public async Task UpdateCarAsync(CarDto carDto)
    {
        var existingCar = await _context.Cars.FindAsync(carDto.CarId);
        if (existingCar != null)
        {
            existingCar.Mileage = carDto.Mileage;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteCarAsync(int id)
    {
        var userCarLinks = await _context.UserCars
            .Where(uc => uc.CarId == id)
            .ToListAsync();

        if (userCarLinks.Any())
        {
            _context.UserCars.RemoveRange(userCarLinks);
        }

        var car = await _context.Cars.FindAsync(id);
        if (car != null)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddCarAsync(int userId, string licensePlate, int mileage)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.LicensePlate == licensePlate);

        if (car == null)
        {
            car = new Car
            {
                LicensePlate = licensePlate,
                Mileage = mileage
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        bool linkExists = await _context.UserCars.AnyAsync(uc => uc.UserId == userId && uc.CarId == car.CarId);

        if (!linkExists)
        {
            var userCar = new UserCar
            {
                UserId = userId,
                CarId = car.CarId
            };
            await _context.UserCars.AddAsync(userCar);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Car is already linked to the user.");
        }


    }
}
