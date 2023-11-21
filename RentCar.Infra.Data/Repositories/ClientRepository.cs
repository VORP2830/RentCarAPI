using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Domain.Filter;
using RentCar.Domain.Interfaces;
using VoteAPI.Infra.Data.Context;

namespace RentCar.Infra.Data.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    private readonly ApplicationDbContext _context;
    public ClientRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Client>> GetAll(ClientFilter clientFilter)
    {
        var query = _context.Clients
                                .Where(c => c.Active);

        if (clientFilter.Name != null)
        {
            query = query.Where(c => c.Name
                                        .ToLower()
                                        .ToString()
                                        .Contains(clientFilter.Name
                                                                .ToLower()
                                                                .ToString()));
        }
        if (clientFilter.CPF != null)
        {
            query = query.Where(c => c.CPF.Contains(clientFilter.CPF));
        }
        return await query.ToListAsync();
    }

    public async Task<Client> GetById(Guid id)
    {
        return await _context.Clients
                                .FirstOrDefaultAsync(c => c.Active &&
                                                            c.Id == id);
    }
}
