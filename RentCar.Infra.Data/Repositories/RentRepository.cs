using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace RentCar.Infra.Data.Repositories;

public class RentRepository : GenericRepository<Rent>, IRentRepository
{
    private readonly ApplicationDbContext _context;
    public RentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rent>> GetAll(RentFilter rentFilter)
    {
        var query = _context.Rents
                                .Where(c => c.Active);

        if (rentFilter.ClientId != null)
        {
            query = query.Where(c => c.ClientId
                                        .ToString()
                                        .ToLower()
                                        .Contains(rentFilter.ClientId
                                                                .ToString()
                                                                .ToLower()));
        }
        if (rentFilter.CarId != null)
        {
            query = query.Where(c => c.CarId
                                        .ToString()
                                        .ToLower()
                                        .Contains(rentFilter.CarId
                                                                .ToString()
                                                                .ToLower()));
        }
        if (rentFilter.RentDate != null)
        {
            query = query.Where(c => c.RentDate.Date == rentFilter.RentDate.Date);
        }
        return await query.ToListAsync();
    }

    public async Task<Rent> GetById(Guid id)
    {
        return await _context.Rents
                                .FirstOrDefaultAsync(c => c.Active &&
                                                            c.Id == id);
    }
}
