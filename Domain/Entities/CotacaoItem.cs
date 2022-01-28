using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CotacaoItem
    {

        [Key]
        public Guid IdCotacaoItem { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Informe a {0}.")]
        [StringLength(100, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Descricao { get; set; }

        [DisplayName("Número Item")]
        [Required(ErrorMessage = "Informe o {0}.")]
        public int NumeroItem { get; set; }

        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        [DisplayName("Quantidade")]
        [Required(ErrorMessage = "Informe a {0}.")]
        public int Quantidade { get; set; }

        [DisplayName("Marca")]
        [StringLength(30, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Marca { get; set; }

        [DisplayName("Unidade")]
        [StringLength(10, ErrorMessage = " O Campo {0} deve ter no máximo {1} dígitos. ")]
        public string Unidade { get; set; }

        [ForeignKey("Cotacao")]
        public Guid IdCotacao { get; set; }

    }
}
