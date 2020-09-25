using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoLudis.Data;
using ProjetoLudis.Tabelas;
using ProjetoLudis.Services;

namespace ProjetoLudis.Controllers
{
    /// <summary>
    /// Metodo de Login do sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public readonly IRepository _repo;
        /// <summary>
        /// Metodo construtor
        /// </summary>
        /// <param name="repo"></param>
        public HomeController(IRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Metodo de validação de Usuario(Body => Email, Senha)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
  /*      [HttpPost]
        [Route("login")]
        [AllowAnonymous]

        public async Task<ActionResult<dynamic>> Authenticate([FromQuery] string email, [FromQuery] string senha)
        {
           /* var usuario = _repo.GetUsuarioLogin(email, senha);

            if (usuario == null)
                return NotFound(new { message = "Email ou Senha Invalída" });

            var token = TokenService.GenerateToken(usuario);
            return new
            {
                usuario = usuario,
                token = token
            };
        }*/
    }
}
