using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class NotaNegociacaoItem
    {
        public Guid IDNotaNegociacaoItem { get; set; }
        public Guid IDNotaNegociacao { get; set; }
        public string Negociacao { get; set; }
        public string TipoNegociacao { get; set; }
        public string TipoOperacao { get; set; }
        public string TipoMercado { get; set; }
        public string Prazo { get; set; }
        public string Titulo { get; set; }
        public string Sigla { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal ValorOperacao { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        public virtual NotaNegociacao IDNotaNegociacaoNavigation { get; set; }
    }
}
