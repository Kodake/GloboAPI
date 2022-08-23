using BackEnd.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class HouseRepository: IHouseRepository
    {
        private readonly HouseDbContext _context;

        public HouseRepository(HouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<HouseDTO>> GetAll()
        {
            return await _context.Houses.Select(x => new HouseDTO(x.Id, x.Address,
                x.Country, x.Price)).ToListAsync();
        }
    }
}
