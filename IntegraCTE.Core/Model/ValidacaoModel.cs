using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Model
{
    public class ValidacaoModel
    {
        public Guid Id { get; set; }
        public Guid IdArquivo { get; set; }
        public ArquivoModel Arquivo { get; set; }
        public string Mensagem { get; set; }

        protected ValidacaoModel()
        {
            
        }

        public ValidacaoModel(Guid id, Guid idArquivo, string mensagem)
        {
            Id = id;
            IdArquivo = idArquivo;
            Mensagem = mensagem;
        }

        public ValidacaoModel(Guid idArquivo, string mensagem)
        {
            Id = Guid.NewGuid();
            IdArquivo = idArquivo;
            Mensagem = mensagem;
        }
    }
}
