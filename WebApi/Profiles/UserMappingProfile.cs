using AutoMapper;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Profiles
{
	public class UserMappingProfile : Profile
	{
		public UserMappingProfile()
		{
            CreateMap<Company, CompanyResponse>();
            CreateMap<User, UserResponse>();
			CreateMap<SignUpRequest, User>();
            CreateMap<User, UserResponseForProject>();
            CreateMap<Comment, CommentResponse>();
            CreateMap<CreateCommentRequest, Comment>();
            CreateMap<Project, ProjectResponseForUsers>();
            CreateMap<Project, ProjectResponse>();
            CreateMap<CreateProjectRequest, Project>();
            CreateMap<Entities.Task, TaskResponse>();
            CreateMap<CreateTaskRequest, Entities.Task>();
        }
	}
}
