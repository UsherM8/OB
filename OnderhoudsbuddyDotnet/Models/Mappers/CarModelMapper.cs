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
            BrandName = car.BrandName,
            CarType = car.CarType,
            Mileage = car.Mileage,
            ExpiryDateApk = car.ExpiryDateApk
        };
    }

    public static Car ToEntity(CarModel carModel)
    {
        return new Car
        {
            CarId = carModel.CarId,
            LicensePlate = carModel.LicensePlate,
            BrandName = carModel.BrandName,
            CarType = carModel.CarType,
            Mileage = carModel.Mileage,
            ExpiryDateApk = carModel.ExpiryDateApk
        };
    }
}