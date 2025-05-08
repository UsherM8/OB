using Domain.Dtos;
using Business.Containers;
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
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarByIdAsync(1))
            .ReturnsAsync(new CarDto
            {
                CarId = 1,
                LicencePlate = "83ZSJT",
                Mileage = 100000,

            });

        // Act
        var result = await _carContainer.GetCarAsync("test");

        // Assert
        Xunit.Assert.NotNull(result); // Controleer dat het resultaat niet null is
        Xunit.Assert.Equal(1, result.CarId); // Controleer de CarId
        Xunit.Assert.Equal("839747", result.LicensePlate); // Controleer het kenteken
        Xunit.Assert.Equal(100000, result.Mileage); // Controleer het kilometerstand
    }
    
    [Fact]
    public async Task GetCarbyLicenseFailTest()
    {
        // Arrange
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarByIdAsync(1))
            .ReturnsAsync(new CarDto
            {
                CarId = 1,
                LicencePlate = "83ZSJT",
                Mileage = 100000,

            });

        // Act
        var result = await _carContainer.GetCarAsync("test");

        // Assert
        Xunit.Assert.NotNull(result); // Controleer dat het resultaat niet null is
        Xunit.Assert.Equal(1, result.CarId); // Controleer de CarId
        Xunit.Assert.Equal("839747", result.LicensePlate); // Controleer het kenteken
        Xunit.Assert.Equal(100000, result.Mileage); // Controleer het kilometerstand
    }
    
    [Fact]
    public async Task GetCarbyIdSuccesTest()
    {
        // Arrange
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarByIdAsync(1))
            .ReturnsAsync(new CarDto
            {
                CarId = 1,
                LicencePlate = "83ZSJT",
                Mileage = 100000,

            });

        // Act
        var result = await _carContainer.GetCarAsync("test");

        // Assert
        Xunit.Assert.NotNull(result); // Controleer dat het resultaat niet null is
        Xunit.Assert.Equal(1, result.CarId); // Controleer de CarId
        Xunit.Assert.Equal("839747", result.LicensePlate); // Controleer het kenteken
        Xunit.Assert.Equal(100000, result.Mileage); // Controleer het kilometerstand
    }
    
    [Fact]
    public async Task GetCarbyIdFailTest()
    {
        // Arrange
        _mockRepository.GetCarRepository()
            .Setup(repo => repo.GetCarByIdAsync(1))
            .ReturnsAsync(new CarDto
            {
                CarId = 1,
                LicencePlate = "83ZSJT",
                Mileage = 100000,

            });

        // Act
        var result = await _carContainer.GetCarAsync("test");

        // Assert
        Xunit.Assert.NotNull(result); // Controleer dat het resultaat niet null is
        Xunit.Assert.Equal(1, result.CarId); // Controleer de CarId
        Xunit.Assert.Equal("839747", result.LicensePlate); // Controleer het kenteken
        Xunit.Assert.Equal(100000, result.Mileage); // Controleer het kilometerstand
    }
}

