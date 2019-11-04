using AthlosifyWebAPI.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AthlosifyWebAPI
{
	public class ClientMappingProfile : Profile
	{
		public ClientMappingProfile()
		{
			CreateMap<Activity, ActivityDTO>()
				.ForMember(dst => dst.OwnerId, src => src.MapFrom(ol => ol.User.Id))
				.ForMember(dst => dst.CategoryName, src => src.MapFrom(ol => ol.Category.Name));

			CreateMap<ActivityDTO, Activity>()
				.ForMember(dst => dst.UserId, opt => opt.MapFrom(src => HttpContext.Current.User.Identity.GetUserId()));

			CreateMap<Category, CategoryDTO>()
				.ForMember(dst => dst.OwnerId, src => src.MapFrom(ol => ol.User.Id))
				.ForMember(dst => dst.ParentName, src => src.MapFrom(ol => ol.Parent.Name));

			CreateMap<CategoryDTO, Category>()
				.ForMember(dst => dst.UserId, opt => opt.MapFrom(src => HttpContext.Current.User.Identity.GetUserId()));

			CreateMap<UserProfile, UserProfileDTO>()
				.ForMember(dst => dst.OwnerId, src => src.MapFrom(ol => ol.User.Id));

			CreateMap<UserProfileDTO, UserProfile>()
				.ForMember(dst => dst.UserId, opt => opt.MapFrom(src => HttpContext.Current.User.Identity.GetUserId()));

		}
	}

	public class AutoMapperConfig
	{
		public MapperConfiguration Configure()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<ClientMappingProfile>();
			});
			return config;
		}
	}
}