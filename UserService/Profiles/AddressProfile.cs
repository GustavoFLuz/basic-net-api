using AutoMapper;

namespace UserService.Profiles;

public class AddressProfile : Profile
{

  public AddressProfile()
  {
    CreateMap<Dtos.AddAddressDto, Models.Address>();
  }

}