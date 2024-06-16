using FoodAPI.Models;
using FoodAPI.Services.FoodService;
using Microsoft.AspNetCore.Mvc;

namespace FoodAPI.Controllers.FoodController;
public class FoodController(FoodServices foodServices) : BaseController
{
    private readonly FoodServices _foodService = foodServices;

    [HttpGet]
    public async Task<List<Food>> Get()
    {
        var foods = await _foodService.GetFoods();
        return foods;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? ingredientName, [FromQuery] string? foodName)
    {
        try
        {
            if (string.IsNullOrEmpty(ingredientName) && string.IsNullOrEmpty(foodName))
            {
                return BadRequest("Vui lòng cung cấp tên nguyên liệu hoặc món ăn");
            }

            var results = await _foodService.SearchFoods(ingredientName, foodName);
            if (results.Count == 0)
                return NotFound("Không tìm thấy tên món ăn");
            return Ok(results);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Food newFood)
    {
        await _foodService.CreateFood(newFood);
        return CreatedAtAction(nameof(Get), newFood);
    }
}