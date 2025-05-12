using Domain.Dtos;
using Business.Containers;
using Business.Classes;
using Moq;
using UnitTests.MockRepository;
using Xunit;

namespace UnitTests.Test;


public class CarContainerTest
{
    private readonly CarContainer _carContainer;
    private readonly CarMockRepository _mockRepository;

    public CarContainerTest()
    {
        _mockRepository = new CarMockRepository();
        _carContainer = new CarContainer(_mockRepository.GetCarRepository().Object);
    }

    [Fact]
    public async Task GetCarbyLicenseSuccesTest()
    {
        // Arrange
        CarDto? car = new CarDto()
        {
            LicensePlate = "83ZSJT",
            Brand = "BMW",
            TradeName = "X1",
            VehicleType = "Hatchback",
            PrimaryColor = "Blue",
            EmptyVehicleMass = 100000,
            FirstAdmissionDate = "2025-05-09",
            MileageJudgment = "logical",
            RegistrationDate = "2025-05-09",
        };
        
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarByLicenseAsync("83ZSJT"))
            .ReturnsAsync(car);

        // Act
        var result = await _carContainer.GetCarByLicenseAsync("83ZSJT");

        // Assert
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal("83ZSJT", result.LicensePlate);
        Xunit.Assert.Equal("BMW", result.Brand);
        Xunit.Assert.Equal("X1", result.TradeName);
        Xunit.Assert.Equal("Hatchback", result.VehicleType);
        Xunit.Assert.Equal("Blue", result.PrimaryColor);
        Xunit.Assert.Equal(100000, result.EmptyVehicleMass);
        Xunit.Assert.Equal("2025-05-09", result.FirstAdmissionDate);
        Xunit.Assert.Equal("logical", result.MileageJudgment);
        Xunit.Assert.Equal("2025-05-09", result.RegistrationDate);
    }

    [Fact]
    public async Task GetCarbyId()
    {
        // Arrange
        CarDto? car = new CarDto()
        {
            CarId = 1,
            Mileage =100000,
            LicensePlate = "83ZSJT",
            Brand = "BMW",
            TradeName = "X1",
            VehicleType = "Hatchback",
            PrimaryColor = "Blue",
            EmptyVehicleMass = 100000,
            FirstAdmissionDate = "2025-05-09",
            MileageJudgment = "logical",
            RegistrationDate = "2025-05-09",
        };
        
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarByIdAsync(1))
            .ReturnsAsync(car);

        // Act
        var result = await _carContainer.GetCarByIdAsync(1);

        // Assert
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(1, result.CarId);
        Xunit.Assert.Equal(100000, result.Mileage);
        Xunit.Assert.Equal("83ZSJT", result.LicensePlate);
        Xunit.Assert.Equal("BMW", result.Brand);
        Xunit.Assert.Equal("X1", result.TradeName);
        Xunit.Assert.Equal("Hatchback", result.VehicleType);
        Xunit.Assert.Equal("Blue", result.PrimaryColor);
        Xunit.Assert.Equal(100000, result.EmptyVehicleMass);
        Xunit.Assert.Equal("2025-05-09", result.FirstAdmissionDate);
        Xunit.Assert.Equal("logical", result.MileageJudgment);
        Xunit.Assert.Equal("2025-05-09", result.RegistrationDate);  
    }

    [Fact]
    public async Task GetCarAsync()
    { 
        // This test only gets 3 values from the car. These three values are the only values saved in my own database.
        // The rest of the values are not saved in my own database.
        
        // Arrange
        CarDto? car = new CarDto()
        {
            CarId = 1,
            Mileage =100000,
            LicensePlate = "83ZSJT",
            Brand = "BMW",
            TradeName = "X1",
            VehicleType = "Hatchback",
            PrimaryColor = "Blue",
            EmptyVehicleMass = 100000,
            FirstAdmissionDate = "2025-05-09",
            MileageJudgment = "logical",
            RegistrationDate = "2025-05-09",
        };
        
        
        
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarAsync(1))
            .ReturnsAsync(car);
        // Act
        var result = await _carContainer.GetCarAsync(1);
        
        // Assert
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(1, result.CarId);
        Xunit.Assert.Equal(100000, result.Mileage);
        Xunit.Assert.Equal("83ZSJT", result.LicensePlate);
    }
    
    [Fact]
    public async Task UpdateCarAsync()
    { 
        // Arrange
        var carToUpdate = new CarDto
        {
            CarId = 1,
            Mileage = 100000,
            LicensePlate = "83ZSJT"
        };
        var updatedCar = new Car
        {
            CarId = 1,
            Mileage = 110000,
            LicensePlate = "83ZSJT"
        };
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarAsync(1))
            .Returns(Task.FromResult(carToUpdate)!);
    
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.UpdateCarAsync(It.IsAny<CarDto>()))
            .Returns(Task.FromResult(updatedCar));
    
        // Act
        await _carContainer.UpdateCarAsync(updatedCar);
    
        // Assert
        _mockRepository.GetCarRepository()
            .Verify(repo => repo.UpdateCarAsync(It.Is<CarDto>(c => 
                c.CarId == 1 && 
                c.Mileage == 110000)), Times.Once);
    }
    
    [Fact]
    public async Task DeleteCarAsync_ShouldDeleteCar()
    { 
        // Arrange
        int carIdToDelete = 1;
    
        var carToDelete = new CarDto
        {
            CarId = carIdToDelete,
            Mileage = 100000,
            LicensePlate = "83ZSJT"
        };
    
        // Setup GetCarAsync om te controleren of de auto bestaat
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarAsync(carIdToDelete))
            .ReturnsAsync(carToDelete);
    
        // Setup DeleteCarAsync
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.DeleteCarAsync(carIdToDelete))
            .Returns(Task.CompletedTask); 
    
        // Act
        await _carContainer.DeleteCarAsync(carIdToDelete);
        
        //Assert
        _mockRepository.GetCarRepository()
            .Verify(repo => repo.DeleteCarAsync(carIdToDelete), Times.Once);
    }
}

