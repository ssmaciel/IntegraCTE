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
        Task AdicionarXML(CTEModel xmlModel);
        Task<int> SaveChangesAsync();
    }
}
