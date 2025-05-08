namespace Models.Mappers;

using Business.Classes;

public static class CarModelMapper
{
    public static CarModel ToModel(Car car)
    {
        return new CarModel
        {
            CarId = car.CarId,
            LicensePlate = car.LicensePlate,
            Mileage = car.Mileage,
        };
    }

    public static Car ToEntity(CarModel carModel)
    {
        return new Car
        {
            CarId = carModel.CarId,
            LicensePlate = carModel.LicensePlate,
            Mileage = carModel.Mileage,
        };
    }
    
    public static CarModel FullCar(Car carModel)
    {
        return new CarModel
        {
            LicensePlate = carModel.LicensePlate,
            Brand = carModel.Brand,
            TradeName = carModel.TradeName,
            VehicleType = carModel.VehicleType,
            PrimaryColor = carModel.PrimaryColor,
            EmptyVehicleMass = carModel.EmptyVehicleMass,
            FirstAdmissionDate = carModel.FirstAdmissionDate,
            MileageJudgment = carModel.MileageJudgment,
            RegistrationDate = carModel.RegistrationDate,
            ApkExpiryDate = carModel.ApkExpiryDate,
        };
    }
}