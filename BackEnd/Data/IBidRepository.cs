using BackEnd.DTOS;

namespace BackEnd.Data
{
    public interface IBidRepository
    {
        Task<List<BidDTO>> Get(int houseId);
        Task<BidDTO> Add(BidDTO dto);
    }
}
