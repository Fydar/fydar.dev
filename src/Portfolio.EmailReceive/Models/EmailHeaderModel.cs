using System;
using System.Collections.Generic;

namespace Portfolio.EmailReceive
{
	public class EmailHeaderModel
	{
		public string MessageId { get; set; }
		public DateTime Timestamp { get; set; }
		public IList<string> From { get; set; }
		public IList<string> To { get; set; }
		public string Subject { get; set; }
	}
}
