using BackEnd.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class HouseRepository : IHouseRepository
    {
        private readonly HouseDbContext _context;

        public HouseRepository(HouseDbContext context)
        {
            _context = context;
        }

        private static void DtoToEntity(HouseDetailDTO dto, HouseEntity e)
        {
            e.Address = dto.Address;
            e.Country = dto.Country;
            e.Description = dto.Description;
            e.Price = dto.Price;
            e.Photo = dto.Photo;
        }

        private static HouseDetailDTO EntityToDetailDTO(HouseEntity e)
        {
            return new HouseDetailDTO(e.Id, e.Address, e.Country,
                e.Price, e.Description, e.Photo);
        }

        public async Task<List<HouseDTO>> GetAll()
        {
            return await _context.Houses.Select(x => new HouseDTO(x.Id, x.Address,
                x.Country, x.Price)).ToListAsync();
        }

        public async Task<HouseDetailDTO?> Get(int id)
        {
            var entity = await _context.Houses
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return EntityToDetailDTO(entity);
        }

        public async Task<HouseDetailDTO> Add(HouseDetailDTO dto)
        {
            var entity = new HouseEntity();
            DtoToEntity(dto, entity);
            _context.Houses.Add(entity);
            await _context.SaveChangesAsync();
            return EntityToDetailDTO(entity);
        }

        public async Task<HouseDetailDTO> Update(HouseDetailDTO dto)
        {
            var entity = await _context.Houses.FindAsync(dto.Id);

            if (entity == null)
            {
                throw new ArgumentException($"Error updating house {dto.Id}");
            }

            DtoToEntity(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return EntityToDetailDTO(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Houses.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"Error deleting house {id}");
            }

            _context.Houses.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
