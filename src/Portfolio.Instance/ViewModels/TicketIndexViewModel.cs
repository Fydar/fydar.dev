using System;

namespace Portfolio.Instance.ViewModels
{
	public class TicketIndexViewModel
	{
		public class TicketListItemModel
		{
			public string TicketId { get; set; } = string.Empty;
			public string From { get; set; } = string.Empty;
			public string Subject { get; set; } = string.Empty;
			public DateTime ReceivedTime { get; set; }
		}

		public TicketListItemModel[] Tickets { get; set; } = Array.Empty<TicketListItemModel>();
	}
}
