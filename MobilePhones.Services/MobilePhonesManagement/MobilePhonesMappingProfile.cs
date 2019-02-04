using AutoMapper;

namespace MobilePhones.Services
{
    public class MobilePhonesMappingProfile : Profile
    {
        public MobilePhonesMappingProfile()
        {
            CreateMap<DataAccess.MobilePhone, Models.MobilePhone>();
        }
    }

    public static class MapperInitializer
    {
        public static void MapperConfiguration()
        {
            Mapper.Initialize(c => c.AddProfile<MobilePhonesMappingProfile>());
        }
    }
}
