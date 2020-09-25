using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Filtros
{
    public class PerfilFiltro : IAsyncActionFilter  
        {  
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        //Código :  antes que a action executa 
        await next();
        //Codigo  : depois que a action executa 
    }
}  
    }  
  