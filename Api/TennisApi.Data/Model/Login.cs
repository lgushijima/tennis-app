using System;
using System.Collections.Generic;

namespace TennisApi.Data.Model
{
    public partial class Login
    {
        public Login()
        {
            Cliente = new HashSet<Cliente>();
            NotaNegociacao = new HashSet<NotaNegociacao>();
        }

        public Guid ID { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Tentativas { get; set; }
        public int IDStatus { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        public virtual ICollection<Cliente> Cliente { get; set; }
        public virtual ICollection<NotaNegociacao> NotaNegociacao { get; set; }
    }
}
