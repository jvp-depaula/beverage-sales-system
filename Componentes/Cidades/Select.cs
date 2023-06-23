namespace Sistema.Componentes.Cidades
{
    public class Select
    {
        public int? id { get; set; }
        public string text { get; set; }
        public Sistema.Componentes.Estados.Select EstadoSelect { get; set; }
    }
}
