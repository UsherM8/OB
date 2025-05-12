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
            LicensePlate = car.LicensePlate,
            Mileage = car.Mileage,
        };
    }
    
    public static Car ToEntity(CarDto carDto)
    {
        return new Car()
        {
            CarId = carDto.CarId,
            LicensePlate = carDto.LicensePlate,
            Mileage = carDto.Mileage,
        };
    }
    
    public static Car MapFromRdw(CarDto CarDto)
    {
        return new Car
        {
            CarId = CarDto.CarId,
            Mileage = CarDto.Mileage,
            LicensePlate = CarDto.LicensePlate,
            Brand = CarDto.Brand,
            TradeName = CarDto.TradeName,
            VehicleType = CarDto.VehicleType,
            PrimaryColor = CarDto.PrimaryColor,
            EmptyVehicleMass = CarDto.EmptyVehicleMass,
            FirstAdmissionDate = CarDto.FirstAdmissionDate,
            MileageJudgment = CarDto.MileageJudgment,
            RegistrationDate = CarDto.RegistrationDate,
            ApkExpiryDate = CarDto.ApkExpiryDate,
        };
    }

    
    
}