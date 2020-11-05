    
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
using AutoMapper;
using ProjetoLudis.Dtos;

namespace ProjetoLudis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuadraController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Context _context;

        public QuadraController(Context context,
                                    IRepository repo, 
                                    IMapper mapper,
                                    UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }


        [HttpPost("cad-quadra")]
        [Authorize(Roles = "Comerciante")]
        public async Task<IActionResult> Registrar(QuadraRegistrar model)
        {
            var quadra = _mapper.Map<Quadra>(model);

            _repo.Add(quadra);
            if (_repo.SaveChanges())
            {
                return Ok(quadra);
            }

            return BadRequest("Quadra não cadastrada");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Comerciante")]
        public IActionResult Put(int id, Quadra model)
        {
            var quadra = _context.Quadras.AsNoTracking().Where(X => X.Id == id).FirstOrDefault(); 
            if (quadra == null) return BadRequest("Quadra não encontrada");

            _repo.Update(model);
            if (_repo.SaveChanges())
            {
                return Ok(model);
            }

            return BadRequest("Quadra não atualizada");
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Comerciante")]
        public IActionResult Patch(int id, Quadra model)
        {
            var quadra = _context.Quadras.AsNoTracking().Where(X => X.Id == id).FirstOrDefault();
            if (quadra == null) return BadRequest("Quadra não encontrada");

            _repo.Update(model);
            if (_repo.SaveChanges())
            {
                return Ok(model);
            }

            return BadRequest("Quadra não atualizado");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Comerciante")]
        public IActionResult Delete(int id)
        {
            var quadra = _context.Quadras.AsNoTracking().Where(X => X.Id == id).FirstOrDefault();
            if (quadra == null) return BadRequest("Quadra não encontrada");


            _repo.Delete(quadra);
            if (_repo.SaveChanges())
            {
                return Ok("Quadra deletada");
            }

            return BadRequest("Quadra não deletada");
        }

        [HttpGet("Get-all")]
        [Authorize]
        public IActionResult Get()
        {
            var quadra = _context.Quadras.AsNoTracking().ToArray();
            return Ok(quadra);

        }

        [HttpGet("Get-nome/{nome}")]
        [Authorize]
        public IActionResult GetId(string nome)
        {
            var quadra = _context.Quadras.AsNoTracking().Where(X => X.Nome.Contains(nome)).FirstOrDefault();
            if (quadra == null) return BadRequest("Quadra não encontrada");

            return Ok(quadra);

        }
    }
}
