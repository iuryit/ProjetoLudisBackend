using AutoMapper;
using ProjetoLudis.Data;
using ProjetoLudis.Dtos;
using ProjetoLudis.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludis.API.Helpers
{
    public class LudisProfile : Profile
    {

        public LudisProfile() 
        { 

          /* CreateMap<QuadraRegistrar, Quadra>().re
              / .ForMember(
                    dest => dest.Comerciante,
                    opt => opt.MapFrom(src => _context.Comerciantes.Where(X => X.Id == src.ComercianteId).FirstOrDefault()) 
                );*/

            CreateMap<Quadra, QuadraRegistrar>().ReverseMap();

            CreateMap<AgendaQuadra, QuadraAgendar>().ReverseMap();

        }
    }
}
