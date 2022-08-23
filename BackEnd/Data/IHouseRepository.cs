using BackEnd.DTOS;

namespace BackEnd.Data
{
    public interface IHouseRepository
    {
        Task<List<HouseDTO>> GetAll();
    }
}
