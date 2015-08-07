using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE100;
using VsWebSite;
using VSLangProj;

namespace CareEvolution.ReferenceChecker
{
	public class ReferenceChecker
	{
		public IList<ReferenceInfoViewModel> GetReferences( Solution4 solution )
		{
			if ( solution == null )
			{
				return new List<ReferenceInfoViewModel>();
			}

			_references = new Dictionary<string, ReferenceInfoViewModel>();

			foreach ( var project in solution.Projects )
			{
				FindReferencesInProject( project );
			}

			return _references.Values.ToList();
		}

		private void FindReferencesInProject( object project )
		{
			var dteProject = project as Project;
			if (dteProject == null) return;

			switch (dteProject.Kind)
			{
				case VSLangProj.PrjKind.prjKindCSharpProject:
					var vsProject = (VSProject)dteProject.Object;
					foreach ( var vsReference in vsProject.References.Cast<Reference>() )
					{
						AddReference( vsReference, dteProject.Name);
					}
					break;
				case VsWebSite.PrjKind.prjKindVenusProject:
					var vsWebSite = (VSWebSite)dteProject.Object;
					foreach ( var vsReference in vsWebSite.References.Cast<Reference>() )
					{
						AddReference( vsReference, dteProject.Name );
					}
					break;
			}

			if (dteProject.ProjectItems == null) return;

			foreach (var projectItem in dteProject.ProjectItems.OfType<ProjectItem>())
			{
				FindReferencesInProject(projectItem.SubProject);
			}
		}

		private void AddReference(Reference vsReference, string projectName)
		{
			var key = vsReference.Name + vsReference.Version;
			if ( _references.ContainsKey( key ) )
			{
				_references[ key ].ProjectNames.Add( projectName );
			}
			else
			{
				var viewModel = new ReferenceInfoViewModel
				{
					Name = vsReference.Name,
					Description = vsReference.Description,
					Version = vsReference.Version,
					Location = vsReference.Path
				};
				viewModel.ProjectNames.Add( projectName );
				_references.Add( key, viewModel );
			}

		}
		private Dictionary<string, ReferenceInfoViewModel> _references;
	}
}