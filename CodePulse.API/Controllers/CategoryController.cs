using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CodePulse.API.Models.DTO;
using CodePulse.API.Models.Domain;
using CodePulse.API.Data;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        // add new category
        [HttpPost]
        public async Task<IActionResult> CreateNewCategory(CreateCategoryRequestDto request)
        {
            // Map DTO to Domail Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            // map Domain Model to Dto
            var response = new Category
            {
                Id = category.Id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            return Ok(response);
        }
    }
}
