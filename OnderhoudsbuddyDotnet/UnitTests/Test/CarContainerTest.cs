using Business.Containers;
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
        
        [Fact]
        public void GetCarAsyncTest()
        {
            //Act 
            
            //Arrange
            
            //Assert
        }
        
        
        
        
        
        
    }

}