using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace RentCar.Infra.Data.Repositories;

public class CarRepository : GenericRepository<Car>, ICarRepository
{
    private readonly ApplicationDbContext _context;
    public CarRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetAll(CarFilter carFilter)
    {
        var query = _context.Cars
                                .Where(c => c.Active);

        if (carFilter.Model != null)
        {
            query = query.Where(c => c.Model
                                        .ToLower()
                                        .ToString()
                                        .Contains(carFilter.Model
                                                                .ToLower()
                                                                .ToString()));
        }
        if (carFilter.Brand != null)
        {
            query = query.Where(c => c.Brand.Contains(carFilter.Brand));
        }
        return await query.ToListAsync();
    }

    public async Task<Car> GetById(Guid id)
    {
        return await _context.Cars
                                .FirstOrDefaultAsync(c => c.Active &&
                                                            c.Id == id);
    }
}
