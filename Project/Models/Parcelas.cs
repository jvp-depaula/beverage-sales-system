namespace Sistema.Models
{
    public class Parcelas
    {
        public int? idCondicaoPgto { get; set; }
        public string dsCondicaoPgto { get; set; }
        public int? idFormaPgto { get; set; }
        public string dsFormaPgto { get; set; }
        public double? nrParcela { get; set; }
        public int? qtDias { get; set; }
        public decimal txPercentual { get; set; }
        public DateTime? dtVencimento { get; set; }
        public decimal vlParcela { get; set; }
        public DateTime? dtPagamento { get; set; }
    }
}
