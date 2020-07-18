using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Threading.Tasks;

namespace Portfolio.Instance.ViewComponents
{
    public class PortfolioItem : ViewComponent
    {
        public PortfolioItem()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(ProjectModel projectModel)
        {
            return View(projectModel);
        }
    }
}
