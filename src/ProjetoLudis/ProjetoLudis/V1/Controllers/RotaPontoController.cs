using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Contracts;
using ProjetoLudis.Contracts.Requests.RotaPontoRequest;
using ProjetoLudis.Contracts.Responses;
using ProjetoLudis.Servicos.RotaPontoServico;

namespace ProjetoLudis.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RotaPontoController : Controller
    {
        private RotaPontoServico _rotaPontoServico;

        public RotaPontoController(RotaPontoServico rotaPontoServico)
        {
            _rotaPontoServico = rotaPontoServico;
        }

        [HttpGet(ApiRoute.RotaPontoRoute.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rotaPontoServico.GetRotaPontoAsync());
        }

        [HttpPut(ApiRoute.RotaPontoRoute.Update)]
        public async Task<IActionResult> Update([FromRoute]int rotaPontoId, [FromBody] UpdateRotaPontoRequest request)
        {
            var rotaPonto = _rotaPontoServico.GetRotaPontoByIdAsync(rotaPontoId).Result;

            if (rotaPonto != null)
            {
                rotaPonto.Descricao = request.descricao;
                rotaPonto.Latitude = request.latitude;
                rotaPonto.Longitude = request.longitude;
                rotaPonto.CategoriaPonto = request.categoriaPonto;

                var updated = await _rotaPontoServico.UpdateRotaPontoAsync(rotaPonto);
                if (updated)
                    return Ok(rotaPonto);
            }
            return NotFound();
        }

        [HttpGet(ApiRoute.RotaPontoRoute.Get)]
        public async Task<IActionResult> Get([FromRoute]int RotaPontoId)
        {
            var rotaPonto = await _rotaPontoServico.GetRotaPontoByIdAsync(RotaPontoId);

            if (rotaPonto == null)
                return NotFound();

            return Ok(rotaPonto);
        }

        [HttpPost(ApiRoute.RotaPontoRoute.Create)]
        public async Task<IActionResult> Create([FromBody] CreateRotaPontoRequest rotaPontoRequest)
        {
            var rotaPonto = new Tabelas.RotaPonto
            {
                Descricao = rotaPontoRequest.descricao,
                Latitude = rotaPontoRequest.latitude,
                Longitude = rotaPontoRequest.longitude,
                CategoriaPonto = rotaPontoRequest.categoriaPonto
            };

            await _rotaPontoServico.CreateRotaPontoAsync(rotaPonto);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoute.RotaPontoRoute.Get.Replace("{rotaPontoId}", rotaPonto.IdRotaPonto.ToString());

            var response = new RotaPontoResponse { IdRotaPonto = rotaPonto.IdRotaPonto };
            return Created(locationUrl, response);
        }

    }
}
