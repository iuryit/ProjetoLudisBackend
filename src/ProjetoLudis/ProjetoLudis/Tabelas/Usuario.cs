using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjetoLudis.Tabelas
{
    public class Usuario : IdentityUser
    {

        public int IdIdentidade { get; set; } //Esportista/Comerciante

    }

}
