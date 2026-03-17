namespace webOnpeMVC.Models.Votacion
{
    public class GrupoVotacion
    {
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public int idLocalVotacion { get; set; }
        public string idGrupoVotacion { get; set; }
        public string nCopia { get; set; }
        public int idEstadoActa { get; set; }
        public int ElectoresHabiles { get; set; }
        public int TotalVotantes { get; set; }
        public int P1 { get; set; }
        public int P2 { get; set; }
        public int VotosBlancos { get; set; }
        public int VotosNulos { get; set; }
        public int VotosImpugnados { get; set; }
        
        public GrupoVotacion() { }

        public GrupoVotacion(string[] aRegistro)
        {
            setRegistro(aRegistro);
        }

        internal void setRegistro(string[] aRegistro)
        {
            Departamento = aRegistro[0];
            Provincia = aRegistro[1];
            Distrito = aRegistro[2];
            RazonSocial = aRegistro[3];
            Direccion = aRegistro[4];
            idLocalVotacion = int.Parse(aRegistro[5]);
            idGrupoVotacion = aRegistro[6];
            nCopia = aRegistro[7];
            idEstadoActa = int.Parse(aRegistro[8]);
            ElectoresHabiles = int.Parse(aRegistro[9]);
            TotalVotantes = int.Parse(aRegistro[10]);
            P1 = int.Parse(aRegistro[11]);
            P2 = int.Parse(aRegistro[12]);
            VotosBlancos = int.Parse(aRegistro[13]);
            VotosNulos = int.Parse(aRegistro[14]);
            VotosImpugnados = int.Parse(aRegistro[15]);
        }



    }
}
