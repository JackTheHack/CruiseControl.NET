using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Core.Builder;
using ThoughtWorks.CruiseControl.Core.Sourcecontrol;
using ThoughtWorks.CruiseControl.WebDashboard.ServerConnection;

namespace ThoughtWorks.CruiseControl.WebDashboard.MVC.Cruise
{
	public class AddProjectModelGenerator
	{
		private readonly ICruiseManagerWrapper cruiseManagerWrapper;

		public AddProjectModelGenerator(ICruiseManagerWrapper cruiseManagerWrapper)
		{
			this.cruiseManagerWrapper = cruiseManagerWrapper;
		}

		public AddProjectModel GenerateModel(IRequest request)
		{
			string selectedServerName = request.GetText("ServersDropDown");
			string[] serverNames = cruiseManagerWrapper.GetServerNames();

			Project project = new Project();
			project.Name = request.GetText("Project.Name");

			P4 p4 = new P4();
			p4.View = request.GetText("Project.SourceControl.View");
			p4.Executable = request.GetText("Project.SourceControl.Executable");
			p4.Client = request.GetText("Project.SourceControl.Client");
			p4.User = request.GetText("Project.SourceControl.User");
			p4.Port = request.GetText("Project.SourceControl.Port");
			p4.ApplyLabel = request.GetChecked("Project.SourceControl.ApplyLabel");
			p4.AutoGetSource = request.GetChecked("Project.SourceControl.AutoGetSource");
			project.SourceControl = p4;

			NAntBuilder nantBuilder = new NAntBuilder();
			nantBuilder.Executable = request.GetText("Project.Builder.Executable");
			nantBuilder.BaseDirectory = request.GetText("Project.Builder.BaseDirectory");
			nantBuilder.BuildFile = request.GetText("Project.Builder.BuildFile");
			nantBuilder.BuildArgs = request.GetText("Project.Builder.BuildArgs");
			nantBuilder.BuildTimeoutSeconds = request.GetInt("Project.Builder.BuildTimeoutSeconds", 0); // Todo - defaults from config?
			project.Builder = nantBuilder;

			return new AddProjectModel(project, selectedServerName, serverNames);
		}
	}
}
