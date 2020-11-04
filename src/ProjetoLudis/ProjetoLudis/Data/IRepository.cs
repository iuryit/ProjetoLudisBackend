using ProjetoLudis.Dtos;
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

        Task<UsuarioComerciante[]> GetAllUsuarioComerciante();
        Task<UsuarioEsportista[]> GetAllUsuarioEsportista();
        Task<UsuarioComerciante> GetUsuarioComercianteId(int comercianteId);
        Task<UsuarioEsportista> GetUsuarioEsportistaId(int esportistaId);


    }
}
