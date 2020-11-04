    
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ProjetoLudis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProjetoLudis.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjetoLudis.Models;
using ProjetoLudis.Tabelas;

namespace ProjetoLudis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsportistaController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Context _context;

        public EsportistaController(Context context,
                                    IRepository repo,
                                    UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _repo = repo;
        }


        [HttpPost("cad-esportista")]
        public async Task<IActionResult> Registrar(Esportista esportista, String emailUser)
        {
            try
            {   // Crio o esportista
                var user = await _userManager.FindByEmailAsync(emailUser);
                _context.Add(esportista);
                _context.SaveChanges();
                // Vinculo o esportista ao usuario logado
                var usuario = _context.Usuarios.AsNoTracking().Where(X => X.Id == user.Id).FirstOrDefault();
                usuario.IdIdentidade = esportista.Id;
                _context.Entry(user).CurrentValues.SetValues(usuario);
                // altero a tabla de usuario
                var result = await _userManager.UpdateAsync(user);
                // vinculo o usuario a uma role
                if (!await _roleManager.RoleExistsAsync("Esportista"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Esportista"));
                }
                await _userManager.AddToRoleAsync(user, "Esportista");

                //montar um DTO, e fazer a consulta e mostra de acordo com esse DTO, e depois mostrar o DTO
                return Ok(esportista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("Get-all")]
        [Authorize(Roles = "Esportista")]
        public IActionResult Get()
        {
            var esportista = _repo.GetAllUsuarioEsportista().Result;
            return Ok(esportista);

        }

        [HttpGet("Get-id/{id}")]
        [Authorize(Roles = "Esportista")]
        public IActionResult GetId(int id)
        {
            var esportista = _repo.GetUsuarioEsportistaId(id).Result;
            if (esportista == null) return BadRequest("Esportista não encontrado");

            return Ok(esportista);

        }
    }
}
