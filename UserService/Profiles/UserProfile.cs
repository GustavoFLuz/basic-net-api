using AutoMapper;

namespace UserService.Profiles;

public class UserProfile: Profile
{

  public UserProfile()
  {
    CreateMap<Models.User, Dtos.CreateUserDTO>();
    CreateMap<Dtos.CreateUserDTO, Models.User>();
  }

}