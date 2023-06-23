namespace Sistema.Componentes.Cidades
{
    public class Select
    {
        public int? id { get; set; }
        public string text { get; set; }
        public string ddd { get; set; }
        public string sigla { get; set; }
        public Sistema.Componentes.Estados.Select EstadoSelect { get; set; }
        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltAlteracao { get; set; }
    }
}
