using System.Threading.Tasks;

namespace Portfolio.Site.Services.ViewToStringRenderer
{
	public interface IViewToStringRenderer
	{
		Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
	}
}
