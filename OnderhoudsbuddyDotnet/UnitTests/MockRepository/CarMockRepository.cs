using Domain.RepositoryInterfaces;
using Moq;

namespace UnitTests.MockRepository;

public class CarMockRepository 
{
    private readonly Mock<ICar> _carRepository;
    public CarMockRepository()
    {
        _carRepository = new Mock<ICar>();
    }
    public Mock<ICar> GetCarRepository()
    {
        return _carRepository;
    }
    
}