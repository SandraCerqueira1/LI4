using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IHistoricoVendaRepository
    {
        Task<List<HistoricoVenda>> FindAll();
        Task<HistoricoVenda> GetHistoricoVendaById(int historicoVendaId);
        Task<bool> DeleteHistoricoVenda(int historicoVendaId);
        Task<bool> InsertHistoricoVenda(HistoricoVenda historicoVenda);
    }
}
