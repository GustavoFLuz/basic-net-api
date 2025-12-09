using AutoMapper;

namespace UserService.Profiles;

public class UserProfile: Profile
{

  public UserProfile()
  {
    CreateMap<Models.User, Dtos.CreateUserDto>();
    CreateMap<Dtos.CreateUserDto, Models.User>();
  }

}