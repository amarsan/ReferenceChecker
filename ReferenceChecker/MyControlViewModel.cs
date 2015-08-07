using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EnvDTE100;

namespace CareEvolution.ReferenceChecker
{
	public class MyControlViewModel : INotifyPropertyChanged
	{
		public IList<ReferenceInfoViewModel> References { get; set; }

		private ReferenceInfoViewModel _selectedItem;
		public ReferenceInfoViewModel SelectedItem
		{
			get { return _selectedItem;}
			set
			{
				_selectedItem = value;
				NotifyPropertyChanged("SelectedItem");
				NotifyPropertyChanged("ItemSelected");
			}
		}

		public bool ItemSelected
		{
			get { return SelectedItem != null; }
		}

		public MyControlViewModel(Solution4 solution)
		{
			var referenceChecker = new ReferenceChecker();

			var references = referenceChecker.GetReferences(solution);
			foreach (var reference in references)
			{
				reference.ProjectNames = reference.ProjectNames.OrderBy(n => n).ToList();
			}
			References = references.OrderBy(r => r.Name).ThenBy(r => r.Version).ToList();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
