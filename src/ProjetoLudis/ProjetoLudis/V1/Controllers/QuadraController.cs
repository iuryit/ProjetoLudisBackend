    
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

        /// <summary>
        /// Cadastro de quadra - Comerciante
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("cad-quadra")]
        [Authorize(Roles = "Comerciante")]
        public async Task<IActionResult> Registrar(QuadraRegistrar model)
        {
            var quadra = _mapper.Map<Quadra>(model);

            quadra.ComercianteId = _repo.GetIdComercianteLogado().Result;
            if (quadra.ComercianteId == 0)
            {
                return BadRequest("Problema com seu cadastro de comerciante, porfavor tente novamente.");
            }

            _repo.Add(quadra);
            if (_repo.SaveChanges())
            {
                return Ok(quadra);
            }

            return BadRequest("Quadra não cadastrada");
        }
        /// <summary>
        /// Consulta de quadras por comerciante - Comerciante
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-quadraComerciante")]
        [Authorize(Roles = "Comerciante")]
        public IActionResult GetQuadraComerciante()
        {
            int ComercianteId = _repo.GetIdComercianteLogado().Result;
            if (ComercianteId == 0)
            {
                return BadRequest("Problema com seu cadastro de comerciante, porfavor tente novamente.");
            }

            var quadra = _repo.GetQuadrasComerciante(ComercianteId);
            if (quadra == null) return BadRequest("Quadras não encontradas");

            return Ok(quadra);

        }
        /// <summary>
        /// Alteracao de quadra - Comerciante
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Alteracao parcial quadra - Comerciante
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Deleção de quadra - Comerciante
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Consulta geral de quadra - Comerciante/Esportista
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-all")]
        [Authorize]
        public IActionResult Get()
        {
            var quadra = _context.Quadras.AsNoTracking().ToArray();
            return Ok(quadra);

        }
        /// <summary>
        /// Agendamento de horario em quadra - Esportista
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("agenda-quadra")]
        [Authorize(Roles = "Esportista")]
        public async Task<IActionResult> Agendar(QuadraAgendar model)
        {
            var agenda = _mapper.Map<AgendaQuadra>(model);

            agenda.EsportistaId = _repo.GetIdEsportistaLogado().Result;
            if (agenda.EsportistaId == 0)
            {
                return BadRequest("Problema com seu cadastro de esportista, porfavor tente novamente.");
            }

            if (_repo.VerificaHorarioDisponivel(agenda.HoraInicio, agenda.HoraFim))
            {
                _repo.Add(agenda);
            }
            else return BadRequest("Horário indisponível, por favor escolha outro horário");

            if (_repo.SaveChanges())
            {
                return Ok(agenda);
            }
            else return BadRequest("Quadra não Agendada");
        }
        /// <summary>
        /// Consulta de quadra por localização - Esportista
        /// </summary>
        /// <param name="cidade"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        [HttpGet("Get-quadraLocalizacao/{cidade}")]
        [Authorize(Roles = "Esportista")]
        public IActionResult GetQuadraLocalizacao(string cidade, string nome)
        {
            var quadra = _repo.GetQuadraLocalizacao(cidade, nome);
            if (quadra == null) return BadRequest("Quadra não encontrada");

            return Ok(quadra);

        }
        /// <summary>
        /// Consulta de horarios agendados em determinada quadra - Esportista
        /// </summary>
        /// <param name="idQuadra"></param>
        /// <param name="dia"></param>
        /// <returns></returns>
        [HttpGet("Get-quadrHorarios/{idQuadra}")]
        [Authorize(Roles = "Esportista")]
        public IActionResult GetQuadraHorarios(int idQuadra, DateTime? dia)
        {
            var Agendaquadra = _repo.GetQuadraHorariosAgendados(idQuadra, dia);

            return Ok(Agendaquadra);

        }
    }
}
