using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class TblPessoas
    {
        public TblPessoas()
        {
            TblDocumentosNIdPessoaNavigation = new HashSet<TblDocumentos>();
            TblEmailsNIdPessoaNavigation = new HashSet<TblEmails>();
            TblSenhas = new HashSet<TblSenhas>();
        }

        public int NIdPessoa { get; set; }
        public string SNome { get; set; }
        public string SNomeApelido { get; set; }
        public DateTime? DNascimento { get; set; }
        public DateTime DInicio { get; set; }
        public DateTime DAlteracao { get; set; }
        public DateTime? DFim { get; set; }

        public virtual ICollection<TblDocumentos> TblDocumentosNIdPessoaNavigation { get; set; }
        public virtual ICollection<TblEmails> TblEmailsNIdPessoaNavigation { get; set; }
        public virtual ICollection<TblSenhas> TblSenhas { get; set; }
    }
}
