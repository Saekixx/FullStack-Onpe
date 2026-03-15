namespace webOnpeMVC.Models
{
    public class Distrito
    {
        public int idDistrito { get; set; }
        public string detalle { get; set; }

        public Distrito() { }

        public Distrito(string[] registro)
        {
            idDistrito = int.Parse(registro[0]);
            detalle = registro[1];
        }

        internal List<Distrito> getList(string[][] mRegistros)
        {
            if (mRegistros == null) return null;
            List<Distrito> lstDistrito = new List<Distrito>();
            foreach (string[] aRegistro in mRegistros)
                lstDistrito.Add(new Distrito(aRegistro));

            return lstDistrito;
        }
    }
}
