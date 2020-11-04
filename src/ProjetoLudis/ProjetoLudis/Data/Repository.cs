using Microsoft.EntityFrameworkCore;
using ProjetoLudis.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoLudis.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ProjetoLudis.Data
{
    public class Repository : IRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Context _context;

        public Repository(Context context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public async Task<UsuarioComerciante[]> GetAllUsuarioComerciante()
        {
            IQueryable<Comerciante> queryItens = _context.Comerciantes;

            var UsuarioComercianteList = new List<UsuarioComerciante>();

            foreach (var query in queryItens.ToArray().ToList()) {

                var UsuarioComerciante = new UsuarioComerciante();

                UsuarioComerciante.Id = query.Id;
                UsuarioComerciante.Bairro = query.Bairro;
                UsuarioComerciante.CEP = query.CEP;
                UsuarioComerciante.Cidade = query.Cidade;
                UsuarioComerciante.Complemento = query.Complemento;
                UsuarioComerciante.CPFCNPJ = query.CPFCNPJ;
                UsuarioComerciante.Endereco = query.Endereco;
                UsuarioComerciante.IndicadorIE = query.IndicadorIE;
                UsuarioComerciante.InscricaoEstadual = query.InscricaoEstadual;
                UsuarioComerciante.InscricaoMunicipal = query.InscricaoMunicipal;
                UsuarioComerciante.Nome = query.Nome;
                UsuarioComerciante.RazaoSocial = query.RazaoSocial;
                UsuarioComerciante.Regime = query.Regime;
                UsuarioComerciante.Telefone = query.Telefone;
                UsuarioComerciante.UF = query.UF;

                IQueryable<Usuario> queryUserItens = _context.Usuarios;
                queryUserItens = queryUserItens.AsNoTracking()
                        .Where(usuario => usuario.IdIdentidade == query.Id);

                foreach (var queryUser in queryUserItens.ToArray().ToList())
                {
                    var userClaims = await _userManager.GetRolesAsync(queryUser);

                    foreach (var claim in userClaims)
                    {
                        if (claim == "Comerciante")
                        {
                            UsuarioComerciante.Email = queryUser.Email;
                            UsuarioComerciante.Senha = queryUser.PasswordHash;
                        }
                    }
                }
                UsuarioComercianteList.Add(UsuarioComerciante);
            }

            return UsuarioComercianteList.ToArray();
        }

        public async Task<UsuarioEsportista[]> GetAllUsuarioEsportista()
        {
            IQueryable<Esportista> queryItens = _context.Esportistas;

            var UsuarioEsportistaList = new List<UsuarioEsportista>();

            foreach (var query in queryItens.ToArray().ToList())
            {

                var UsuarioEsportista = new UsuarioEsportista();

                UsuarioEsportista.Id = query.Id;
                UsuarioEsportista.Bairro = query.Bairro;
                UsuarioEsportista.CEP = query.CEP;
                UsuarioEsportista.Cidade = query.Cidade;
                UsuarioEsportista.Complemento = query.Complemento;
                UsuarioEsportista.CPF = query.CPF;
                UsuarioEsportista.Endereco = query.Endereco;
                UsuarioEsportista.Nome = query.Nome;
                UsuarioEsportista.Telefone = query.Telefone;
                UsuarioEsportista.UF = query.UF;

                IQueryable<Usuario> queryUserItens = _context.Usuarios;
                queryUserItens = queryUserItens.AsNoTracking()
                        .Where(usuario => usuario.IdIdentidade == query.Id);

                foreach (var queryUser in queryUserItens.ToArray().ToList())
                {
                    var userClaims = await _userManager.GetRolesAsync(queryUser);

                    foreach (var claim in userClaims)
                    {
                        if (claim == "Esportista")
                        {
                            UsuarioEsportista.Email = queryUser.Email;
                            UsuarioEsportista.Senha = queryUser.PasswordHash;
                        }
                    }
                }
                UsuarioEsportistaList.Add(UsuarioEsportista);
            }

            return UsuarioEsportistaList.ToArray();
        }

        public async Task<UsuarioComerciante> GetUsuarioComercianteId(int comercianteId)
        {
            IQueryable<Comerciante> queryitens = _context.Comerciantes;

            Comerciante query = queryitens.AsNoTracking()
                    .Where(comerciante => comerciante.Id == comercianteId).FirstOrDefault();

            var UsuarioComerciante = new UsuarioComerciante();

            UsuarioComerciante.Id = query.Id;
            UsuarioComerciante.Bairro = query.Bairro;
            UsuarioComerciante.CEP = query.CEP;
            UsuarioComerciante.Cidade = query.Cidade;
            UsuarioComerciante.Complemento = query.Complemento;
            UsuarioComerciante.CPFCNPJ = query.CPFCNPJ;
            UsuarioComerciante.Endereco = query.Endereco;
            UsuarioComerciante.IndicadorIE = query.IndicadorIE;
            UsuarioComerciante.InscricaoEstadual = query.InscricaoEstadual;
            UsuarioComerciante.InscricaoMunicipal = query.InscricaoMunicipal;
            UsuarioComerciante.Nome = query.Nome;
            UsuarioComerciante.RazaoSocial = query.RazaoSocial;
            UsuarioComerciante.Regime = query.Regime;
            UsuarioComerciante.Telefone = query.Telefone;
            UsuarioComerciante.UF = query.UF;


            IQueryable<Usuario> queryUserItens = _context.Usuarios;
            queryUserItens = queryUserItens.AsNoTracking()
                               .Where(usuario => usuario.IdIdentidade == query.Id);

            foreach (var queryUser in queryUserItens.ToArray().ToList())
            {
                var userClaims = await _userManager.GetRolesAsync(queryUser);

                foreach (var claim in userClaims)
                {
                    if (claim == "Comerciante")
                    {
                        UsuarioComerciante.Email = queryUser.Email;
                        UsuarioComerciante.Senha = queryUser.PasswordHash;
                    }
                }
            }         

            return UsuarioComerciante;
        }

        public async Task<UsuarioEsportista> GetUsuarioEsportistaId(int esportistaId)
        {
            IQueryable<Esportista> queryitens = _context.Esportistas;

            Esportista query = queryitens.AsNoTracking()
                    .Where(comerciante => comerciante.Id == esportistaId).FirstOrDefault();

            var UsuarioEsportista = new UsuarioEsportista();

            UsuarioEsportista.Id = query.Id;
            UsuarioEsportista.Bairro = query.Bairro;
            UsuarioEsportista.CEP = query.CEP;
            UsuarioEsportista.Cidade = query.Cidade;
            UsuarioEsportista.Complemento = query.Complemento;
            UsuarioEsportista.CPF = query.CPF;
            UsuarioEsportista.Endereco = query.Endereco;
            UsuarioEsportista.Nome = query.Nome;
            UsuarioEsportista.Telefone = query.Telefone;
            UsuarioEsportista.UF = query.UF;


            IQueryable<Usuario> queryUserItens = _context.Usuarios;
            queryUserItens = queryUserItens.AsNoTracking()
                               .Where(usuario => usuario.IdIdentidade == query.Id);

            foreach (var queryUser in queryUserItens.ToArray().ToList())
            {
                var userClaims = await _userManager.GetRolesAsync(queryUser);

                foreach (var claim in userClaims)
                {
                    if (claim == "Esportista")
                    {
                        UsuarioEsportista.Email = queryUser.Email;
                        UsuarioEsportista.Senha = queryUser.PasswordHash;
                    }
                }
            }

            return UsuarioEsportista;
        }
       
    }
}
