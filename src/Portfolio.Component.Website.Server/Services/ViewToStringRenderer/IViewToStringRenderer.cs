using System.Threading.Tasks;

namespace Portfolio.Component.Website.Server.Services.ViewToStringRenderer
{
	public interface IViewToStringRenderer
	{
		Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
	}
}
