using BasicApi.BackEnd.Data;
using BasicApi.BackEnd.Repositories.Interfaces;
using BasicApi.Shared.Entities;
using BasicApi.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace BasicApi.BackEnd.Repositories.Implementations;

public class StatesRepository : GenericRepository<State>, IStatesRepository
{
    private readonly DataContext _context;

    public StatesRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
    {
        var states = await _context.States
            .Include(s => s.Cities)
            .ToListAsync();
        return new ActionResponse<IEnumerable<State>>
        {
            WasSuccess = true,
            Result = states
        };
    }

    public override async Task<ActionResponse<State>> GetAsync(int id)
    {
        var state = await _context.States
             .Include(s => s.Cities)
             .FirstOrDefaultAsync(s => s.Id == id);

        if (state == null)
        {
            return new ActionResponse<State>
            {
                WasSuccess = false,
                Message = "Estado no existe"
            };
        }

        return new ActionResponse<State>
        {
            WasSuccess = true,
            Result = state
        };
    }
}