using ProjetoLudis.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

       /* Usuario[] GetAllUsuario();
        Usuario[] GetAllUsuarioByComercianteId(int comercianteId);
        Usuario[] GetAllUsuarioByEsportistaId(int esportistaId);
        Usuario GetUsuarioById(int UsuarioId);
        Usuario GetUsuarioLogin(string usurarioEmail, string usuarioSenha);*/


    }
}
