using BackEnd.Data;
using BackEnd.DTOS;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

namespace BackEnd.Endpoints
{
    public static class WebApplicationHouseExtensions
    {
        public static void MapHouseEndpoints(this WebApplication app)
        {
            app.MapGet("/houses", (IHouseRepository repo) => repo.GetAll())
            .Produces<HouseDTO[]>(StatusCodes.Status200OK);

            app.MapGet("/house/{houseId:int}", async (int houseId, IHouseRepository repo) =>
            {
                var house = await repo.Get(houseId);

                if (house == null)
                {
                    return Results.Problem($"House with ID {houseId} not found.",
                        statusCode: 404);
                }

                return Results.Ok(house);
            }).ProducesProblem(404).Produces<HouseDetailDTO>(StatusCodes.Status200OK);

            app.MapPost("/houses", async ([FromBody] HouseDetailDTO dto, IHouseRepository repo) =>
            {
                if (!MiniValidator.TryValidate(dto, out var errors))
                    return Results.ValidationProblem(errors);

                var newHouse = await repo.Add(dto);
                return Results.Created($"/house/{newHouse.Id}", newHouse);
            }).Produces<HouseDetailDTO>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

            app.MapPut("/houses", async ([FromBody] HouseDetailDTO dto, IHouseRepository repo) =>
            {
                if (!MiniValidator.TryValidate(dto, out var errors))
                    return Results.ValidationProblem(errors);

                if (await repo.Get(dto.Id) == null)
                {
                    return Results.Problem($"House with ID {dto.Id} not found.",
                        statusCode: 404);
                }

                var updatedHouse = await repo.Update(dto);
                return Results.Ok(updatedHouse);
            }).ProducesProblem(404).Produces<HouseDetailDTO>(StatusCodes.Status200OK)
            .ProducesValidationProblem();

            app.MapDelete("/house/{houseId:int}", async (int houseId, IHouseRepository repo) =>
            {
                if (await repo.Get(houseId) == null)
                {
                    return Results.Problem($"House with ID {houseId} not found.",
                        statusCode: 404);
                }
                await repo.Delete(houseId);
                return Results.Ok();
            }).ProducesProblem(404).Produces<HouseDetailDTO>(StatusCodes.Status200OK);
        }
    }
}
