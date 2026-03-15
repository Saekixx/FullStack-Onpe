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
            return new VotosDepartamentos().getList(clsbd.getRegistros());
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

        public List<Departamento> getDepartamentos()
        {
            clsbd.Sentencia("usp_getDepartamentos");
            return new Departamento().getList(clsbd.getRegistros());
        }

        public List<Provincia> GetProvincias(string idDepartamento)
        {
            clsbd.Sentencia("usp_getProvincias", new SqlParameter("@idDepartamento", idDepartamento));
            return new Provincia().getList(clsbd.getRegistros());
        }

        public List<Distrito> GetDistritos(string idProvincia)
        {
            clsbd.Sentencia("usp_getDistritos", new SqlParameter("@idProvincia", idProvincia));
            return new Distrito().getList(clsbd.getRegistros());
        }

        public LocalesVotacion getLocalesVotacion(string idDistrito)
        {
            clsbd.Sentencia("usp_getLocalesVotacion", new SqlParameter("@idDistrito", idDistrito));
            string[] registro = clsbd.getRegistro();

            if (registro == null) return null;

            return new LocalesVotacion(registro);
        }

        public List<GruposVotacion> GetGruposVotacions(string idLocalVotacion)
        {
            clsbd.Sentencia("usp_getGruposVotacion", new SqlParameter("@idLocalVotacion", idLocalVotacion));
            return new GruposVotacion().getList(clsbd.getRegistros());
        }
    }
}
