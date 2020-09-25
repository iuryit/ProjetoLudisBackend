using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Contracts;
using ProjetoLudis.Contracts.Requests.Rota;
using ProjetoLudis.Contracts.Responses;
using ProjetoLudis.Servicos.RotaServico;

namespace ProjetoLudis.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RotaController : Controller
    {
        private RotaServico _rotaServico;

        public RotaController(RotaServico rotaServico)
        {
            _rotaServico = rotaServico;
        }

        [HttpGet(ApiRoute.RotaRoute.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rotaServico.GetRotaAsync());
        }

        [HttpPut(ApiRoute.RotaRoute.Update)]
        public async Task<IActionResult> Update([FromRoute]int rotaId, [FromBody] UpdateRotaRequest request)
        {
            var rota = _rotaServico.GetRotaByIdAsync(rotaId).Result;

            if (rota != null)
            {
                rota.Origem = request.origem;
                rota.Destino = request.destino;
                rota.Pontos = request.pontos;

                var updated = await _rotaServico.UpdateRotaAsync(rota);
                if (updated)
                    return Ok(rota);
            }
            return NotFound();
        }

        [HttpGet(ApiRoute.RotaRoute.Get)]
        public async Task<IActionResult> Get([FromRoute]int rotaId)
        {
            var rota = await _rotaServico.GetRotaByIdAsync(rotaId);

            if (rota == null)
                return NotFound();

            return Ok(rota);
        }

        [HttpPost(ApiRoute.RotaRoute.Create)]
        public async Task<IActionResult> Create([FromBody] CreateRotaRequest rotaRequest)
        {
            var rota = new Tabelas.Rota
            {
                Origem = rotaRequest.origem,
                Destino = rotaRequest.destino,
                Pontos = rotaRequest.pontos,
            };

            await _rotaServico.CreateRotaAsync(rota);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoute.RotaRoute.Get.Replace("{rotaId}", rota.IdRota.ToString());

            var response = new RotaResponse { IdRota = rota.IdRota };
            return Created(locationUrl, response);

        }
        }
}
