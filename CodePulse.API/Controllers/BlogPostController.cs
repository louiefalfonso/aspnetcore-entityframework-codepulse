using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

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

            // add new blog post
            blogPost = await blogPostRepository.CreateAsync(blogPost);

            // Convert Domain Model to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisible = blogPost.IsVisible
            };


            return Ok(response);


        }
    }
}
