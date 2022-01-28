using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Cotacao
    {
        [Key]
        public Guid IdCotacao { get; set; }

        [DisplayName("CNPJ Comprador")]
        [Required(ErrorMessage = "Informe o {0}.")]
        [StringLength(14, ErrorMessage = " O Campo {0} deve ter {1} dígitos. ", MinimumLength = 14)]
        public string CNPJComprador { get; set; }

        [DisplayName("CNPJ Fornecedor")]
        [Required(ErrorMessage = "Informe o {0}.")]
        [StringLength(14, ErrorMessage = " O Campo {0} deve ter {1} dígitos. ", MinimumLength = 14)]
        public string CNPJFornecedor { get; set; }

        [DisplayName("Número")]
        [Required(ErrorMessage = "Informe o {0}.")]
        public int NumeroCotacao { get; set; }

        [DisplayName("Data Cotação")]
        public DateTime DataCotacao { get; set; }

        [DisplayName("Data Entrega Cotação")]
        [Required(ErrorMessage = "Informe a {0}.")]
        public DateTime DataEntregaCotacao { get; set; }

        [DisplayName("CEP")]
        [Required(ErrorMessage = "Informe o {0}.")]
        [StringLength(8, ErrorMessage = " O Campo {0} deve ter {1} dígitos. ", MinimumLength = 8)]
        public string CEP { get; set; }

        [DisplayName("Logradouro")]
        [StringLength(50, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Logradouro { get; set; }

        [DisplayName("Número")]
        [StringLength(10, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        [StringLength(20, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Complemento { get; set; }

        [DisplayName("Bairro")]
        [StringLength(50, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Bairro { get; set; }

        [DisplayName("Cidade")]
        [StringLength(50, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Cidade { get; set; }

        [DisplayName("UF")]
        [StringLength(2, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string UF { get; set; }

        [DisplayName("Logradouro")]
        [StringLength(100, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Observacao { get; set; }

        [NotMapped]
        public List<CotacaoItem> CotacaoItem { get; set; }
    }
}
