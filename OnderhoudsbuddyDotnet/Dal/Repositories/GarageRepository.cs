using Dal.Entities;
using Domain.Dtos;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Dal.Repositories
{
    public class GarageRepository : IGarage
    {
        private readonly OnderhoudsbuddyDbContext _context;

        public GarageRepository(OnderhoudsbuddyDbContext context)
        {
            _context = context;
        }

        public async Task  CreateGarageAsync(GarageDto garage)
        {
            if (garage == null)
            {
                throw new ArgumentNullException(nameof(garage), "Garage cannot be null");
            }
            var Garage = new Garage
            {
                Name = garage.Name,
                StreetName = garage.StreetName,
                HouseNumber = garage.HouseNumber,
                PostalCode = garage.PostalCode,
                City = garage.City,
                ExtraAddressInfo = garage.ExtraAddressInfo,
                PhoneNumber = garage.PhoneNumber,
                Email = garage.Email
            };
            await _context.Garages.AddAsync(Garage);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GarageDto>> GetAllGaragesAsync()
        {
            var garages = await _context.Garages.ToListAsync(); 
            if (garages == null || !garages.Any())
            {
                return Enumerable.Empty<GarageDto>();
            }

            var garagedto = garages.Select(g => new GarageDto
            {
                GarageId = g.GarageId,
                Name = g.Name,
                StreetName = g.StreetName,
                HouseNumber = g.HouseNumber,
                PostalCode = g.PostalCode,
                City = g.City,
                ExtraAddressInfo = g.ExtraAddressInfo,
                PhoneNumber = g.PhoneNumber,
                Email = g.Email
            });

            return garagedto.ToList();
        }

        public async Task<GarageDto> GetGarageByIdAsync(int id)
        {
            var garage = await _context.Garages.FirstOrDefaultAsync(g => g.GarageId == id);
            if (garage == null)
            {
                throw new KeyNotFoundException($"Garage with ID {id} not found.");
            }
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
    }
}
