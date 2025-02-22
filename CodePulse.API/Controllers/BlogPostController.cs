using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {

        // add new blog post
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request) {

            // convert DTO to Domain Model
            var blogPost = new BlogPost
            {
               Title = request.Title,
               ShortDescription = request.ShortDescription,
               Content = request.Content,
               FeaturedImageUrl = request.FeaturedImageUrl,
               UrlHandle = request.UrlHandle,
               PublishedDate = request.PublishedDate,
               Author = request.Author,
               IsVisible = request.IsVisible
            };


        }
    }
}
