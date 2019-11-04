using AthlosifyMobileApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AthlosifyMobileApp.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public ICommand OwnerWebsiteURLCommand { get; private set; }

		private string _name;
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		private string _version;
		public string Version
		{
			get => _version;
			set => SetProperty(ref _version, value);
		}

		private string _description;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

		private string _owner;
		public string Owner
		{
			get => _owner;
			set => SetProperty(ref _owner, value);
		}

		private string _ownerWebsiteURL;
		public string OwnerWebsiteURL
		{
			get => _ownerWebsiteURL;
			set => SetProperty(ref _ownerWebsiteURL, value);
		}

		private string _contributor;
		public string Contributor
		{
			get => _contributor;
			set => SetProperty(ref _contributor, value);
		}

		

		public AboutViewModel()
		{
			PageTitle = "About";

			Name = Constant.Setting_About_ApplicationName;
			Version = Constant.Setting_About_ApplicationVersion;
			Description = Constant.Setting_About_ApplicationDescription;
			Owner = Constant.Setting_About_Owner;
			OwnerWebsiteURL = Constant.Setting_About_OwnerWebsiteURL;
			Contributor = Constant.Setting_About_Contributor;

			OwnerWebsiteURLCommand = new Command(async () => await GoToOwnerWebsiteURL());

		}

		public async Task GoToOwnerWebsiteURL()
		{
			Device.OpenUri(new Uri(OwnerWebsiteURL));
		}
	}
}
