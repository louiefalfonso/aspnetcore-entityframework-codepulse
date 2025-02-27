using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;


namespace CodePulse.API.Repositories.Interface
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Add New Blog Post
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        // Get All Blog Posts
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        // Get Blog Post by ID
        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        // Get Blog Post By Url Handle
        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        // Update Blog Post
        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            // fetch the blog post using ID
            var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }

            // Update Blogpost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            // Update Category
            existingBlogPost.Categories = blogPost.Categories;

            await dbContext.SaveChangesAsync();

            return existingBlogPost;
        }

        // Delete Blog Post
        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
               dbContext.BlogPosts.Remove(existingBlogPost);
               await dbContext.SaveChangesAsync();
                return existingBlogPost;
            }

            return null;
        }

      
    }
}
