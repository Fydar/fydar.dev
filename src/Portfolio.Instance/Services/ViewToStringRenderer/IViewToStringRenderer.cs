using System.Threading.Tasks;

namespace Portfolio.Instance.Services.ViewRenderer
{
	public interface IViewToStringRenderer
	{
		Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
	}
}
