
using Business.Classes;
using Business.ContainerInterfaces;
using Business.Mappers;
using Domain.RepositoryInterfaces;

namespace Business.Containers
{
    public class GarageContainer : IGarageContainer
    {

        private readonly IGarage _garageRepository;
        public GarageContainer(IGarage garageRepository, GarageMapper garageMapper)
        {
            _garageRepository = garageRepository;
        }

        public async Task CreateGarageAsync(Garage garage)
        {
            var garageDto = GarageMapper.ToDto(garage);
            await _garageRepository.CreateGarageAsync(garageDto);
        }

        public async Task<IEnumerable<Garage>> GetAllGaragesAsync()
        {
            var garageDtos = await _garageRepository.GetAllGaragesAsync();
            List<Garage> garages = new List<Garage>();
            foreach (var garageDto in garageDtos)
            {
                garages.Add(GarageMapper.ToClass(garageDto));
            }
            return garages;
        }

        public async Task<Garage> GetGarageByIdAsync(int id)
        {
            var garageDto = await _garageRepository.GetGarageByIdAsync(id);
            if (garageDto == null)
            {
                return new Garage();
            }
            return GarageMapper.ToClass(garageDto);
        }
    }
}
