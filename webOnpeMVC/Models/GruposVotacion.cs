namespace webOnpeMVC.Models
{
    public class GruposVotacion
    {
        public int idGrupoVotacion { get; set; }

        public GruposVotacion() { }

        public GruposVotacion(string[] aRegistro)
        {
                idGrupoVotacion = int.Parse(aRegistro[0]);
        }

        internal List<GruposVotacion> getList(string[][] mRegistros)
        {
            if (mRegistros == null) return null;
            List<GruposVotacion> lstGruposVotacion = new List<GruposVotacion>();
            foreach (string[] aRegistro in mRegistros)
                lstGruposVotacion.Add(new GruposVotacion(aRegistro));

            return lstGruposVotacion;
        }
    }
}
