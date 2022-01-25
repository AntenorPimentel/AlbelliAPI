using AlbelliAPI.Business.Profiles;
using AutoMapper;

namespace AlbelliAPI.Test
{
    public class ServiceTestsBase
    {
        protected readonly IMapper _mapper;

        public ServiceTestsBase()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderServiceProfile());
            }).CreateMapper();
        }
    }
}
