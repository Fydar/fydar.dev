using System;

namespace Portfolio.Models
{
	public abstract class InstitutionModel : IComparable<InstitutionModel>
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Slug { get; set; }
		public string Excerpt { get; set; }
		public string Page { get; set; }
		public string Address { get; set; }
		public int Order { get; set; }

		public int CompareTo(InstitutionModel other)
		{
			return Order.CompareTo(other?.Order ?? 0);
		}
	}
}
