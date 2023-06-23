using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sistema.Componentes.Produtos
{
    public class Select
    {
        public int? id { get; set; }
        public string text { get; set; }
        public decimal? vlVenda { get; set; }
        public string unidade { get; set; }

        public static SelectListItem[] Unidade
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "", Text = " " },
                    new SelectListItem { Value = "U", Text = "UNIDADE" },
                };
            }
        }
    }
}
