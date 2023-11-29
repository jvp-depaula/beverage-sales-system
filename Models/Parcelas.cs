namespace Sistema.Models
{
    public class Parcelas
    {
        public int? idFormaPgto { get; set; }
        public string dsFormaPgto { get; set; }
        public DateTime? dtVencimento { get; set; }
        public decimal vlParcela { get; set; }
        public double? nrParcela { get; set; }
        public string flSituacao { get; set; }
        public DateTime? dtPagamento { get; set; }
    }
}
