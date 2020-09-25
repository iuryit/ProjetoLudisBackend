using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api_Ocr.Contracts.Requests.EmpresaRequest
{
    public class CreateEmpresaRequest
    {
       public string NomeFantasia { get; set; }

        public string Cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Bairro { get; set; }

        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Email { get; set; }

        public string TipoEmpresa { get; set; }


    }
}
