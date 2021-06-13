using System;

namespace Portfolio.Instance.ViewModels
{
	public class TicketIndexViewModel
	{
		public class TicketListItemModel
		{
			public string TicketId { get; set; }
			public string From { get; set; }
			public string Subject { get; set; }
			public DateTime ReceivedTime { get; set; }
		}

		public TicketListItemModel[] Tickets { get; set; }
	}
}
