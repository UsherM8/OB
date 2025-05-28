using Business.Classes;
using Domain.Dtos;

namespace Business.Mappers
{
    public class GarageMapper
    {
        public static GarageDto ToDto(Garage garage)
        {
            return new GarageDto
            {
                GarageId = garage.GarageId,
                Name = garage.Name,
                StreetName = garage.StreetName,
                HouseNumber = garage.HouseNumber,
                PostalCode = garage.PostalCode,
                City = garage.City,
                ExtraAddressInfo = garage.ExtraAddressInfo,
                PhoneNumber = garage.PhoneNumber,
                Email = garage.Email
            };
        }

        public static Garage ToClass(GarageDto garageDto)
        {
            return new Garage
            {
                GarageId = garageDto.GarageId,
                Name = garageDto.Name,
                StreetName = garageDto.StreetName,
                HouseNumber = garageDto.HouseNumber,
                PostalCode = garageDto.PostalCode,
                City = garageDto.City,
                ExtraAddressInfo = garageDto.ExtraAddressInfo,
                PhoneNumber = garageDto.PhoneNumber,
                Email = garageDto.Email
            };
        }
    }
}
