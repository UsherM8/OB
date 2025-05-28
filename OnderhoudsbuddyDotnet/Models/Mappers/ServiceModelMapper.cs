using Business.Classes;
using Domain.Dtos;
using Models.Models;

namespace Models.Mappers
{
    public class ServiceModelMapper
    {
        public static ServiceModel ToModel(Service service)
        {
            return new ServiceModel
            {
                ServiceId = service.ServiceId,
                CarId = service.CarId,
                GarageId = service.GarageId,
                ServiceType = service.ServiceType,
                Status = service.Status,
                ServiceDate = service.ServiceDate,
                NextServiceDate = service.NextServiceDate,
                Description = service.Description
            };
        }

        public static Service ToEntity(ServiceModel serviceModel)
        {
            return new Service
            {
                ServiceId = serviceModel.ServiceId,
                CarId = serviceModel.CarId,
                GarageId = serviceModel.GarageId,
                ServiceType = serviceModel.ServiceType,
                Status = serviceModel.Status,
                ServiceDate = serviceModel.ServiceDate,
                NextServiceDate = serviceModel.NextServiceDate,
                Description = serviceModel.Description
            };
        }

    }
}
