using Microsoft.Data.SqlClient;
using webOnpeMVC.Models;

namespace webOnpeMVC.Controllers.dao
{
    public class daoVoto
    {
        bd.clsBD clsbd = new bd.clsBD("Onpe");
        
        internal List<VotosDepartamentos> getVotosDepartamentos()
        {
            clsbd.Sentencia("usp_getVotos", new SqlParameter("@inicio", 1), new SqlParameter("@fin", 25));
            return new VotosDepartamentos().getList( clsbd.getRegistros() );
        }
        internal List<VotosDepartamentos> getVotosExtranjero()
        {
            clsbd.Sentencia("usp_getVotos", new SqlParameter("@inicio", 26), new SqlParameter("@fin", 30));
            return new VotosDepartamentos().getList(clsbd.getRegistros());
        }

        internal List<VotosDepartamentos> getVotosProvincia(string departamento)
        {
            clsbd.Sentencia("usp_getVotosDepartamento", new SqlParameter("@Departamento", departamento));
            return new VotosDepartamentos().getList(clsbd.getRegistros());
        }

        internal List<VotosDepartamentos> getVotosDistrito(string provincia)
        {
            clsbd.Sentencia("usp_getVotosProvincia", new SqlParameter("@Provincia", provincia));
            return new VotosDepartamentos().getList(clsbd.getRegistros());
        }

        public GrupoVotacion getGrupoVotacion(string idGrupoVotacion)
        {
            clsbd.Sentencia("usp_getGrupoVotacion", new SqlParameter("@idGrupoVotacion", idGrupoVotacion));
            string[] registro = clsbd.getRegistro();

            if (registro == null) return null;

            return new GrupoVotacion(registro);
        }

    }
}
