using System.Collections.Generic;

namespace CareEvolution.ReferenceChecker
{
	public class ReferenceInfoViewModel
	{
		public string Name { get; set; }
		public string Version { get; set; }
		public string Description { get; set; }
		public string Location { get; set; }
		public IList<string> ProjectNames { get; set; }

		public ReferenceInfoViewModel()
		{
			ProjectNames = new List<string>();
		}
	}
}
