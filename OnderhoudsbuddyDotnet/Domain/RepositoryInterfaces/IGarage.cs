using Domain.Dtos;

namespace Domain.RepositoryInterfaces
{
    public interface IGarage
    {
        Task<IEnumerable<GarageDto>> GetAllGaragesAsync();
        Task<GarageDto> GetGarageByIdAsync(int id);
        Task CreateGarageAsync(GarageDto garage);
    }
}
