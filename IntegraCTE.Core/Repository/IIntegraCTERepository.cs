using IntegraCTE.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Repository
{
    public interface IIntegraCTERepository
    {
        Task Adicionar(ArquivoModel xmlModel);
        Task Adicionar(CTEModel cteModel);
        Task<ArquivoModel> BuscarArquivoCTE(Guid id);
        Task<IEnumerable<Guid>> BuscarArquivosCTENProcessados();
        Task<int> SaveChangesAsync();
    }
}
