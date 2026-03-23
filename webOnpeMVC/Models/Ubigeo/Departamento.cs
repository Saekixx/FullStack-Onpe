namespace webOnpeMVC.Models.Ubigeo
{
    public class Departamento
    {
        public int idDepartamento { get; set; }
        public string detalle { get; set; }

        public Departamento() { }

        public Departamento(string[] registro)
        {
            idDepartamento = int.Parse(registro[0]);
            detalle = registro[1];
        }

        internal List<Departamento> getList(string[][] mRegistros)
        {
            if (mRegistros == null) return null;
            List<Departamento> lstDepartamentos = new List<Departamento>();
            foreach (string[] aRegistro in mRegistros)
                lstDepartamentos.Add(new Departamento(aRegistro));

            return lstDepartamentos;
        }
    }
}
