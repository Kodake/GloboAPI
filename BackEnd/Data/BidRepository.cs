using BackEnd.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class BidRepository: IBidRepository
    {
        private readonly HouseDbContext _context;

        public BidRepository(HouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<BidDTO>> Get(int houseId)
        {
            return await _context.Bids.Where(x => x.HouseId == houseId)
                .Select(x => new BidDTO(x.Id, x.HouseId,
                    x.Bidder, x.Amount))
                .ToListAsync();
        }

        public async Task<BidDTO> Add(BidDTO dto)
        {
            var entity = new BidEntity();
            entity.HouseId = dto.HouseId;
            entity.Bidder = dto.Bidder;
            entity.Amount = dto.Amount;
            _context.Bids.Add(entity);
            await _context.SaveChangesAsync();
            return new BidDTO(entity.Id, entity.HouseId,
                entity.Bidder, entity.Amount);
        }
    }
}
