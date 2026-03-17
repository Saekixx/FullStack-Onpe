using Microsoft.Data.SqlClient;
using webOnpeMVC.Models.Ubigeo;

namespace webOnpeMVC.Controllers.dao
{
    public class daoUbigeo
    {
        bd.clsBD clsbd = new bd.clsBD("Onpe");

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
    }
}
