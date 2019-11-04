using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
    public class RegisterModel : BaseModel
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public float? Handicap { get; set; }

		public string UserRole { get; set; }
		public string Email { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		private string _selectedUserRole;

		public string SelectedUserRole
		{
			get => _selectedUserRole;
			set => SetProperty(ref _selectedUserRole, value);
		}
	}
}
