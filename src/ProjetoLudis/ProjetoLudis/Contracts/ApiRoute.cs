using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoLudis.Contracts
{
    public class ApiRoute
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;


        public static class EmpresaRoute
        {
            public const string GetAll = Base + "/empresa";

            public const string Update = Base + "/empresa/{empresaId}";

            public const string Delete = Base + "/empresa/{empresaId}";

            public const string Get = Base + "/empresa/{empresaId}";

            public const string Create = Base + "/empresa";
        }

        public static class RotaPontoRoute
        {
            public const string GetAll = Base + "/rotaPonto";

            public const string Update = Base + "/rotaPonto/{rotaPontoId}";

            public const string Get = Base + "/rotaPonto/{rotaPontoId}";

            public const string Create = Base + "/rotaPonto";
        }

        public static class RotaRoute
        {
            public const string GetAll = Base + "/rota";

            public const string Update = Base + "/rota/{rotaId}";

            public const string Get = Base + "/rota/{rotaId}";

            public const string Create = Base + "/rota";
        }

    }
}
