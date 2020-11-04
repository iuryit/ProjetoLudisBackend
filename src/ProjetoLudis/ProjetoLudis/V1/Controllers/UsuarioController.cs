    
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
    public class UsuarioController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly Context _context;

        public UsuarioController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IRepository repo,
                              Context context,
                              IOptions<AppSettings> appSettings)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _context = context;
            _repo = repo;
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
                EmailConfirmed = true,

        };


            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);

            return Ok("Usuario Criado com sucesso");
           // return Ok(await GerarJwt(registerUser.Email));
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginUser.Email);
                var userClaims = await _userManager.GetRolesAsync(user);
               // return Ok(user);
                // Verifico se o usario tem role
                foreach (var claim in userClaims)
                {
                    //Caso tenha e seja esportista,
                    if (claim == "Esportista")
                    {
                        var usuario = _context.Usuarios.AsNoTracking().Where(X => X.Id == user.Id).FirstOrDefault();
                        var Retorno = _repo.GetUsuarioEsportistaId(usuario.IdIdentidade).Result;
                        Retorno.Token = await GerarJwt(loginUser.Email);
                        return Ok(Retorno);
                    }
                    //Caso tenha e seja comerciante,
                    else if (claim == "Comerciante")
                    {
                        var usuario = _context.Usuarios.AsNoTracking().Where(X => X.Id == user.Id).FirstOrDefault();
                        var Retorno = _repo.GetUsuarioComercianteId(usuario.IdIdentidade).Result;
                        Retorno.Token = await GerarJwt(loginUser.Email);
                        return Ok(Retorno);
                    }                   
                }
                // caso não tenha
                return BadRequest("Usuario não vinculado a Função. Defina se você é um Esportsta ou Comerciante.");              
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


            // Get User roles and add them to claims
            var roles = await _userManager.GetRolesAsync(user);
            AddRolesToClaims(claims, roles);

            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                // Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        private void AddRolesToClaims(List<Claim> claims, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                claims.Add(roleClaim);
            }
        }

    }
}
