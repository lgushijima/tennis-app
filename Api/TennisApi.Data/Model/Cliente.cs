using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Cliente
    {
        public Guid ID { get; set; }
        public Guid IDLogin { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string FoneRes { get; set; }
        public string FoneCel { get; set; }
        public int IDStatus { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual Login IDLoginNavigation { get; set; }
    }
}
