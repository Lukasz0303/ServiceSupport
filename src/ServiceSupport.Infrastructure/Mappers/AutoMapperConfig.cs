using AutoMapper;
using ServiceSupport.Core.Domain;
using ServiceSupport.Infrastructure.DTO;

namespace ServiceSupport.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Device, DeviceDto>();
                cfg.CreateMap<Person, PersonDto>();
                cfg.CreateMap<PersonDto, Person>();
                cfg.CreateMap<ServiceOrder, ServiceOrderDto>();
                cfg.CreateMap<Shop, ShopDto>();
                cfg.CreateMap<ServiceOrderDescription, ServiceOrderDescriptionDto>();
                cfg.CreateMap<ShopTime, ShopTimeDto>();
            })
            .CreateMapper();
    }
}