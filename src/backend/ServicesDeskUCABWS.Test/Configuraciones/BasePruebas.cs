

using AutoMapper;
using ServicesDeskUCABWS.BussinessLogic.Mapper;
using Microsoft.EntityFrameworkCore;

namespace ServicesDeskUCABWS.Test.Configuraciones
{
    public class BasePrueba
    {
        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EtiquetaMapper());
            });
            return config.CreateMapper();
        }

        // BdContext 

    }
}