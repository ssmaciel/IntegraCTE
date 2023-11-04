using IntegraCTE.Core.Context;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Infra.Repository
{
    public class IntegraCTERepository : IIntegraCTERepository
    {
        public readonly IIntegraCTEContext _context;

        public IntegraCTERepository(IIntegraCTEContext context)
        {
            _context = context;
        }

        public async Task Adicionar(ArquivoModel xmlModel)
        {
            await _context.Adicionar(xmlModel);
        }

        public async Task Adicionar(CTEModel cteModel)
        {
            await _context.Adicionar(cteModel);
        }

        public async Task Adicionar(TransportadoraModel transportadoraModel)
        {
            await _context.Adicionar(transportadoraModel);
        }

        public async Task Adicionar(ValidacaoModel validacaoModel)
        {
            await _context.Adicionar(validacaoModel);
        }

        public async Task<ArquivoModel> BuscarArquivoCTE(Guid id)
        {
            return await _context.ArquivoCTE.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Guid>> BuscarArquivosCTENProcessados()
        {
            return await _context.ArquivoCTE.Where(x => !x.Processado).Select(x => x.Id).ToListAsync();
        }

        public async Task<CTEModel> BuscarCTE(Guid id)
        {
            return await _context.CTE.Include(x => x.Transportadora).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CTEModel>> BuscarCTEs()
        {
            return await _context.CTE.Include(x => x.Transportadora).ToListAsync();
        }

        public async Task<TransportadoraModel> BuscarTransportadoraPorCNPJ(string cnpj)
        {
            return await _context.Transportadora.Where(x => x.Cnpj == cnpj).SingleOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
