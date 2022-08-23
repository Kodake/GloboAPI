using BackEnd.DTOS;

namespace BackEnd.Data
{
    public interface IHouseRepository
    {
        Task<List<HouseDTO>> GetAll();
        Task<HouseDetailDTO?> Get(int id);
        Task<HouseDetailDTO> Add(HouseDetailDTO dto);
        Task<HouseDetailDTO> Update(HouseDetailDTO dto);
        Task Delete(int id);
    }
}
