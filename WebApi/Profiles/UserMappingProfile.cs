using AutoMapper;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Profiles
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
			CreateMap<SignUpRequest, User>();
            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<CreateProjectRequest, Project>();
			CreateMap<CreateTaskRequest, Entities.Task>();
        }
	}
}
