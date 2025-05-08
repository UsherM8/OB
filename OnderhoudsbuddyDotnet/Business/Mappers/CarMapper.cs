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
            Mileage = car.Mileage,
 
        };
    }
    
    public static Car ToEntity(CarDto carDto)
    {
        return new Car
        {
            CarId = carDto.CarId,
            LicensePlate = carDto.LicencePlate,
            Mileage = carDto.Mileage,
        };
    }
    
    public static Car MapFromRdw(RdwCarDto rdwCarDto)
    {
        return new Car
        {
            LicensePlate = rdwCarDto.LicensePlate,
            Brand = rdwCarDto.Brand,
            TradeName = rdwCarDto.TradeName,
            VehicleType = rdwCarDto.VehicleType,
            PrimaryColor = rdwCarDto.PrimaryColor,
            EmptyVehicleMass = rdwCarDto.EmptyVehicleMass,
            FirstAdmissionDate = rdwCarDto.FirstAdmissionDate,
            MileageJudgment = rdwCarDto.MileageJudgment,
            RegistrationDate = rdwCarDto.RegistrationDate,
            ApkExpiryDate = rdwCarDto.ApkExpiryDate,
        };
    }

    
    
}