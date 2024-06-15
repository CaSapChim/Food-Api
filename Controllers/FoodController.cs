using System.ComponentModel;
using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class FoodController: ControllerBase {
    private readonly FoodServices _foodService;

    public FoodController(FoodServices foodServices) => _foodService = foodServices;

    [HttpGet]
    public async Task<List<Food>> Get()
    {
        var foods = await _foodService.GetFoods();
        return foods;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? ingredientName, [FromQuery] string? foodName) {
        try {
            if (string.IsNullOrEmpty(ingredientName) && string.IsNullOrEmpty(foodName)) {
                return BadRequest("Vui lòng cung cấp tên nguyên liệu hoặc món ăn");
            }

            var results = await _foodService.SearchFoods(ingredientName, foodName);
            // if (results.length == 0)
            //     return Ok("Không tìm thấy tên món ăn");
            return Ok(results);

        } catch (Exception ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Food newFood) {
        await _foodService.CreateFood(newFood);
        return CreatedAtAction(nameof(Get), newFood);
    }
}