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
            })
            .CreateMapper();
    }
}