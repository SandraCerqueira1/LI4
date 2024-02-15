using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IHistoricoCompraRepository
    {
        Task<List<HistoricoCompra>> GetHistoricoComprasByUtilizadorId(int utilizadorId);
        Task<bool> InsertHistoricoCompra(HistoricoCompra historicoCompra);
    }
}
