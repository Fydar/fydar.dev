using Portfolio.Services.Content.Blog;

namespace Portfolio.Component.Website.Server.Areas.Blog.Models
{
	public class BlogPostViewModel
	{
		public BlogPostModel BlogPost { get; set; }

		public BlogPostViewModel(BlogPostModel blogPost)
		{
			BlogPost = blogPost;
		}
	}
}
