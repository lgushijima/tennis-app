using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class NotaNegociacao
    {
        public NotaNegociacao()
        {
            NotaNegociacaoItem = new HashSet<NotaNegociacaoItem>();
        }

        public Guid IDNotaNegociacao { get; set; }
        public Guid IDLogin { get; set; }
        public DateTime DataReferencia { get; set; }
        public int? AnoMesReferencia { get; set; }
        public string NomeCorretora { get; set; }
        public long Numero { get; set; }
        public int Tipo { get; set; }
        public decimal ValorLiquidoOperacoes { get; set; }
        public decimal TaxaLiquidacao { get; set; }
        public decimal TaxaRegistro { get; set; }
        public decimal? TotalCBLC { get; set; }
        public decimal TaxaTermoOpcao { get; set; }
        public decimal TaxaANA { get; set; }
        public decimal Emolumentos { get; set; }
        public decimal? TotalBovespa { get; set; }
        public decimal TaxaOperacional { get; set; }
        public decimal Execucao { get; set; }
        public decimal TaxaCustodia { get; set; }
        public decimal Impostos { get; set; }
        public decimal IRRF { get; set; }
        public decimal Outros { get; set; }
        public decimal? TotalCustos { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        public virtual Login IDLoginNavigation { get; set; }
        public virtual ICollection<NotaNegociacaoItem> NotaNegociacaoItem { get; set; }
    }
}
