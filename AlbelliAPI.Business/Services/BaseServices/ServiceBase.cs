using AutoMapper;
using Serilog;

namespace AlbelliAPI.Business.Services
{
    public class ServiceBase
    {
        protected readonly ILogger _logger = Log.Logger;
        protected IMapper _mapper;
    }
}