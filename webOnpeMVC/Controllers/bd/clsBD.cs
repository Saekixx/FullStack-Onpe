using Microsoft.Data.SqlClient;
using System.Data;

namespace webOnpeMVC.Controllers.bd
{
    public class clsBD
    {
        readonly string CadenaConexion;

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;

        public clsBD(string BD)
        {
            CadenaConexion = "Data Source=localhost;Database=Onpe;Trusted_Connection=True;TrustServerCertificate=True";
            cn = new SqlConnection(CadenaConexion);
            cmd = new SqlCommand("", cn);
            da = new SqlDataAdapter(cmd);
        }

        internal DataTable getDataTable()
        {
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        internal string[][] getRegistros()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;

            int i = 0;
            string[][] mRegistros = new string[dt.Rows.Count][];
            foreach (DataRow dr in dt.Rows)
                mRegistros[i++] = System.Array.ConvertAll(dr.ItemArray, x => x.ToString().Trim());

            return mRegistros;
        }

        internal string[] getRegistro()
        {
            DataTable dt = getDataTable();
            if (dt.Rows.Count == 0) return null;

            return System.Array.ConvertAll(dt.Rows[0].ItemArray, x => x.ToString().Trim());
        }

        internal void Sentencia(string nombreSP, params SqlParameter[] parametros)
        {
            cmd.CommandText = nombreSP;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            if(parametros != null)
                cmd.Parameters.AddRange(parametros);
        }

    }
}
