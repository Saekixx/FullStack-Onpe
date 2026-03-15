namespace webOnpeMVC.Models
{
    public class LocalesVotacion
    {
        public int idLocalVotacion { get; set; }
        public string RazonSocial { get; set; }

        public LocalesVotacion() { }

        public LocalesVotacion(string[] registro)
        {
            idLocalVotacion = int.Parse(registro[0]);
            RazonSocial = registro[1];
        }
    }
}
