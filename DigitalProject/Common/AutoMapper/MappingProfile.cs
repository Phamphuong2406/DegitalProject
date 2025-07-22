using AutoMapper;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;
using DigitalProject.Models.Project;
using DigitalProject.Models.User;

namespace DigitalProject.Common.AutoMapper
{

    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDTO>().ReverseMap() ;//map User=> UserDTO
            CreateMap<UserRequestData, User>();
            CreateMap<ProjectDTO, Project>().ReverseMap();
            CreateMap<User, ClaimCreationData>();
            CreateMap<Gallery, GetGalleryDTO>().ReverseMap(); ;
        }
        
    }
}
