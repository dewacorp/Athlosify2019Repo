using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
    public class Activity : BaseModel
    {
		private int _id = 0;
		public int Id
		{
			get => _id;
			set => SetProperty(ref _id, value);
		}

		private string _ownerId;

		public string OwnerId
		{
			get => _ownerId;
			set => SetProperty(ref _ownerId, value);
		}

		private int _categoryId;

		public int CategoryId
		{
			get => _categoryId;
			set => SetProperty(ref _categoryId, value);
		}

		private string _categoryName;
		public string CategoryName
		{
			get => _categoryName;
			set => SetProperty(ref _categoryName, value);
		}

		private string _name;

		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		private string _description;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

		private int? _noOfMinutes;

		public int? NoOfMinutes
		{
			get => _noOfMinutes;
			set => SetProperty(ref _noOfMinutes, value);
		}

		private DateTime _createdDate;

		public DateTime CreatedDate
		{
			get => _createdDate;
			set => SetProperty(ref _createdDate, value);
		}

		private DateTime? _modifiedDate;

		public DateTime? ModifiedDate
		{
			get => _modifiedDate;
			set => SetProperty(ref _modifiedDate, value);
		}

		private Category _selectedCategory;

		public Category SelectedCategory
		{
			get => _selectedCategory;
			set => SetProperty(ref _selectedCategory, value);
		}
	}
}
