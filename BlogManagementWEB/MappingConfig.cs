using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Model;
using AutoMapper;

namespace BlogManagementWEB
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<Admin, AdminDTO>().ReverseMap();
        }
    }
}
