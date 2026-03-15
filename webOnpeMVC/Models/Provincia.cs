namespace webOnpeMVC.Models
{
    public class Provincia
    {
        public int idProvincia { get; set; }
        public string detalle { get; set; }

        public Provincia() { }

        public Provincia(string[] registro)
        {
            idProvincia = int.Parse(registro[0]);
            detalle = registro[1];
        }

        internal List<Provincia> getList(string[][] mRegistros)
        {
            if (mRegistros == null) return null;
            List<Provincia> lstProvincias = new List<Provincia>();
            foreach (string[] aRegistro in mRegistros)
                lstProvincias.Add(new Provincia(aRegistro));

            return lstProvincias;
        }
    }
}
