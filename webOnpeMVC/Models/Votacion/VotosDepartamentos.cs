namespace webOnpeMVC.Models.Votacion
{
    public class VotosDepartamentos
    {
        public string DPD { get; set; }
        public int TV { get; set; }
        public string PTV { get; set; }
        public int TA { get; set; }
        public string PTA { get; set; }
        public int EH { get; set; }

        public bool Valido { get; set; }

        public VotosDepartamentos() { }

        public VotosDepartamentos(string[] aRegistro)
        {
            setRegistro(aRegistro);
        }

        internal void setRegistro(string[] aRegistro)
        {
            Valido = aRegistro != null;
            if (!Valido) return;

            DPD = aRegistro[0];
            TV = int.Parse(aRegistro[1]);
            PTV = aRegistro[2];
            TA = int.Parse(aRegistro[3]);
            PTA = aRegistro[4];
            EH = int.Parse(aRegistro[5]);
        }

        internal List<VotosDepartamentos> getList(string[][] mRegistros)
        {
            if (mRegistros == null) return null;
            List<VotosDepartamentos> lstVotosDepartamentos = new List<VotosDepartamentos>();
            foreach (string[] aRegistro in mRegistros)
                lstVotosDepartamentos.Add(new VotosDepartamentos(aRegistro));

            return lstVotosDepartamentos;
        }
    }
}
