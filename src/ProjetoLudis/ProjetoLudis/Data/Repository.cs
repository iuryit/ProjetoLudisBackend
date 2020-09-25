using Microsoft.EntityFrameworkCore;
using ProjetoLudis.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjetoLudis.Data
{
    public class Repository : IRepository
    {
        private readonly Context _context;
        public Repository(Context context)
        {
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

       /* public Usuario[] GetAllUsuario()
        {
            IQueryable<Usuario> query = _context.Usuarios;

            query = query.Include(a => a.Esportista)
                         .Include(a => a.Comerciante);   

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public Usuario[] GetAllUsuarioByComercianteId(int comercianteId)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            query = query.Include(a => a.Comerciante);

            query = query.AsNoTracking()
                         .OrderBy(A => A.Id)
                         .Where(usuario => usuario.Comerciante.Id == comercianteId);

            return query.ToArray();
        }

        public Usuario[] GetAllUsuarioByEsportistaId(int esportistaId)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            query = query.Include(a => a.Esportista);

            query = query.AsNoTracking()
                         .OrderBy(A => A.Id)
                         .Where(usuario => usuario.Esportista.Id == esportistaId);

            return query.ToArray();
        }

        public Usuario GetUsuarioById(int UsuarioId)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            query = query.Include(a => a.Esportista)
                         .Include(a => a.Comerciante);

            query = query.AsNoTracking()
                         .OrderBy(A => A.Id)
                         .Where(usuario => usuario.Id == UsuarioId);

            return query.FirstOrDefault();
        }

        public Usuario GetUsuarioLogin(string usurarioEmail, string usuarioSenha)
        {
            IQueryable<Usuario> query = _context.Usuarios;

            query = query.AsNoTracking()
                         .OrderBy(A => A.Id)
                         .Where(usuario => usuario.Email == usurarioEmail)
                         .Where(usuario => usuario.Senha == usuarioSenha);

            return query.FirstOrDefault();
        }*/
    }
}
