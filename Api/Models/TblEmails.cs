using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class TblEmails
    {
        public int NIdEmail { get; set; }
        public int NIdPessoa { get; set; }
        public string SEmail { get; set; }
        public int NValidado { get; set; }
        public DateTime DInicio { get; set; }
        public DateTime DAlteracao { get; set; }
        public DateTime? DFim { get; set; }

        public virtual TblPessoas NIdPessoaNavigation { get; set; }
    }
}
