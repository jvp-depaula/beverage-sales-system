using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Pessoas
    {
        [Key]
        [DisplayName("Código")]
        public int id { get; set; }

        // CONTATO
        [Display(Name = "Número Celular")]
        public string nrTelefoneCelular { get; set; }
        [Display(Name = "Número Telefone")]
        public string nrTelefoneFixo { get; set; }
        [Display(Name = "Email")]
        public string dsEmail { get; set; }

        // TIPO
        [Display(Name = "Pessoa")]
        public string flTipo { get; set; }

        // ENDERECO
        [Display(Name = "CEP")]
        public string nrCEP { get; set; }
        [Display(Name = "Logradouro")]
        public string dsLogradouro { get; set; }
        [Display(Name = "Número")]
        public string nrEndereco { get; set; }
        [Display(Name = "Bairro")]
        public string dsBairro { get; set; }
        [Display(Name = "Complemento")]
        public string dsComplemento { get; set; }
        [Display(Name = "Cidade")]
        public int idCidade { get; set; }


        // CONTROLE
        [Display(Name = "Data de cadastro")]
        public DateTime? dtCadastro { get; set; }

        [Display(Name = "Data da últ. alteração")]
        public DateTime? dtUltAlteracao { get; set; }

        #region FLAGS
        #region TIPO
        public static SelectListItem[] Tipo
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Value = "F", Text = "FÍSICA" },
                    new SelectListItem { Value = "J", Text = "JURÍDICA" }                    
                };
            }
        }
        #endregion
        #endregion
    }
}
