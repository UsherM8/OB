
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public class CarRepository
{
    private readonly DbContext _context;

    public CarRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        return await _context.Set<CarDto>().ToListAsync();
    }

    public async Task<CarDto?> GetByIdAsync(int id)
    {
        return await _context.Set<CarDto>().FindAsync(id);
    }

    public async Task AddAsync(CarDto carDto)
    {
        await _context.Set<CarDto>().AddAsync(carDto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CarDto carDto)
    {
        _context.Set<CarDto>().Update(carDto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var carDto = await GetByIdAsync(id);
        if (carDto != null)
        {
            _context.Set<CarDto>().Remove(carDto);
            await _context.SaveChangesAsync();
        }
    }
}

public class CarDto
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}