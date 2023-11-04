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
        public string TipoMensagem { get; set; }

        protected ValidacaoModel()
        {
            
        }

        public ValidacaoModel(Guid id, Guid idArquivo, string mensagem, string tipoMensagem)
        {
            Id = id;
            IdArquivo = idArquivo;
            Mensagem = mensagem;
            TipoMensagem = tipoMensagem;
        }

        public ValidacaoModel(Guid idArquivo, string mensagem, string tipoMensagem)
        {
            Id = Guid.NewGuid();
            IdArquivo = idArquivo;
            Mensagem = mensagem;
            TipoMensagem = tipoMensagem;
        }
    }
}
