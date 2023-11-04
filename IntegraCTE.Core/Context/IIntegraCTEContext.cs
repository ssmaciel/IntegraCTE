using IntegraCTE.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Context
{
    public interface IIntegraCTEContext
    {
        IQueryable<ArquivoModel> ArquivoCTE { get; }
        IQueryable<CTEModel> CTE { get; }
        IQueryable<TransportadoraModel> Transportadora { get; }
        IQueryable<ValidacaoModel> Validacao { get; }

        Task Adicionar<T>(T t);
        Task Atualizar<T>(T t);
        Task Remover<T>(T t);

        Task<int> SaveChangesAsync();
    }
}
