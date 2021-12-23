using Portfolio.Models.Blog;

namespace Portfolio.Site.Areas.Blog.Models
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
