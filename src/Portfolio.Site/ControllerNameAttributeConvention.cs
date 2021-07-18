using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace Portfolio.Site.Areas.Portfolio.Controllers
{
	public class ControllerNameAttributeConvention : IControllerModelConvention
	{
		public void Apply(ControllerModel controller)
		{
			var areaAttribute = controller.Attributes
				.OfType<AreaAttribute>()
				.SingleOrDefault();

			if (areaAttribute != null)
			{
				string areaName = areaAttribute.RouteValue;
				if (controller.ControllerName.StartsWith(areaName))
				{
					// controller.ControllerName = controller.ControllerName[areaName.Length..];
				}
			}
		}
	}
}
