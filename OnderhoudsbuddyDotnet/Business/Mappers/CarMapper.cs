namespace Business.Mappers;
using Classes;
using Domain.Dtos;

public static class CarMapper
{
    public static CarDto ToDto(Car car)
    {
        return new CarDto
        {
            CarId = car.CarId,
            LicencePlate = car.LicensePlate,
            BrandName = car.BrandName,
            CarType = car.CarType,
            Mileage = car.Mileage,
            ExpiryDateApk = car.ExpiryDateApk
        };
    }
    
    public static Car ToEntity(CarDto carDto)
    {
        return new Car
        {
            CarId = carDto.CarId,
            LicensePlate = carDto.LicencePlate,
            BrandName = carDto.BrandName,
            CarType = carDto.CarType,
            Mileage = carDto.Mileage,
            ExpiryDateApk = carDto.ExpiryDateApk
        };
    }
}