    
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ProjetoLudis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Tabelas;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProjetoLudis.Options;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjetoLudis.Models;

namespace ProjetoLudis.Controllers
{
    /// <summary>
    /// Metodos de  manipulação do Usuario 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly AppSettings _appSettings;

        public UsuarioController(SignInManager<Usuario> signInManager,
                              UserManager<Usuario> userManager,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModels registerUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var newUseId = Guid.NewGuid();

            var user = new Usuario

            {

                Id = newUseId.ToString(),
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
               /* Telefone = registerUser.Telefone,
                Endereco = registerUser.Endereco,
                CEP = registerUser.CEP,
                Cidade = registerUser.Cidade,
                Bairro = registerUser.Bairro,
                Nome = registerUser.Nome,
                Complemento = registerUser.Complemento,
                UF = registerUser.UF,
                IdEsportista = registerUser.IdEsportista,
                IdComerciante = registerUser.IdComerciante*/


            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);

            await _userManager.AddClaimAsync(user, new Claim(type: "carga.view", value: "true"));


            return Ok(await GerarJwt(registerUser.Email));
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return Ok(await GerarJwt(loginUser.Email));
            }

            return BadRequest("Usuário ou senha inválidos");
        }

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(await _userManager.GetClaimsAsync(user));

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var claims = new List<Claim> {
            new Claim(type: JwtRegisteredClaimNames.Email, value:user.Email),
            new Claim(type: JwtRegisteredClaimNames.Jti, value:Guid.NewGuid().ToString()),
            new Claim(type: JwtRegisteredClaimNames.Sub, value:user.Email),
            new Claim(type:"id", value:user.Id),
            new Claim(type:"Expira em", value:DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras).ToString()),


             };

            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                //Subject = new ClaimsIdentity(new[]
                //{
                //    new Claim(ClaimTypes.Name, user.Id)
                //}),

                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                // Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
       
    }
}
