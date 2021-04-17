namespace Portfolio.Instance.ViewModels
{
	public class ResumeViewModel
	{
		public string Company { get; set; }
		public string Position { get; set; }

		public ResumeViewModel(string company, string position)
		{
			Company = company;
			Position = position;
		}
	}
}
