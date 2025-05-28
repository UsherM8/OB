using Business.Classes;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Mappers
{
    public class GarageModelMapper
    {
        public static GarageModel ToModel(Garage garage)
        {
            return new GarageModel
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

        public static Garage ToEntity(GarageModel garageModel)
        {
            return new Garage
            {
                GarageId = garageModel.GarageId,
                Name = garageModel.Name,
                StreetName = garageModel.StreetName,
                HouseNumber = garageModel.HouseNumber,
                PostalCode = garageModel.PostalCode,
                City = garageModel.City,
                ExtraAddressInfo = garageModel.ExtraAddressInfo,
                PhoneNumber = garageModel.PhoneNumber,
                Email = garageModel.Email
            };
        }
    }
}
