using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOS
{
    public record BidDTO(int Id, int HouseId, 
        [property: Required] string Bidder, int Amount);
}
