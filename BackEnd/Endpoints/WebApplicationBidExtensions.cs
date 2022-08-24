using BackEnd.Data;
using BackEnd.DTOS;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

namespace BackEnd.Endpoints
{
    public static class WebApplicationBidExtensions
    {
        public static void MapBidEndpoints(this WebApplication app)
        {
            app.MapGet("house/{houseId:int}/bids", async (int houseId, IHouseRepository houseRepo, IBidRepository bidRepo) =>
            {
                if (await houseRepo.Get(houseId) == null)
                {
                    return Results.Problem($"House {houseId} not found.",
                        statusCode: 400);
                }

                var bids = await bidRepo.Get(houseId);
                return Results.Ok(bids);
            }).ProducesProblem(400).Produces(StatusCodes.Status200OK);

            app.MapPost("house/{houseId:int}/bids", async (int houseId, [FromBody] BidDTO dto, IBidRepository repo) =>
            {
                if (dto.HouseId != houseId)
                {
                    return Results.Problem("No match", statusCode: StatusCodes.Status400BadRequest);
                }

                if (!MiniValidator.TryValidate(dto, out var errors))
                {
                    return Results.ValidationProblem(errors);
                }

                var newBid = await repo.Add(dto);
                return Results.Created($"/house/{newBid.HouseId}/bids", newBid);
            }).Produces<HouseDetailDTO>(StatusCodes.Status201Created)
            .ProducesValidationProblem().ProducesProblem(400)
            .Produces<BidDTO>(StatusCodes.Status201Created);
        }
    }
}
