namespace Sistema.Componentes.Estados
{
    public class Select
    {
        public int? id { get; set; }
        public string text { get; set; }
        public Sistema.Componentes.Paises.Select PaisSelect { get; set; }
    }
}