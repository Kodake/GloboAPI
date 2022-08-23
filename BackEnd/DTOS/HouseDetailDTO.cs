using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOS
{
    public record HouseDetailDTO(int Id, [property: Required] string? Address, [property: Required] string? Country,
        int Price, string? Description, string? Photo);
}
