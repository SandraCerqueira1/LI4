using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class HistoricoCompraRepository : IHistoricoCompraRepository
    {
        private readonly ISqlDataAccess _db;

        public HistoricoCompraRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<HistoricoCompra>> GetHistoricoComprasByUtilizadorId(int utilizadorId)
        {
            string sql = "SELECT * FROM HistoricoCompras WHERE UtilizadorId = @UtilizadorId";
            var parameters = new { UtilizadorId = utilizadorId };
            List<HistoricoCompra> historicoCompras = await _db.LoadData<HistoricoCompra, dynamic>(sql, parameters);
            return historicoCompras;
        }

        public async Task<bool> InsertHistoricoCompra(HistoricoCompra historicoCompra)
        {
            string sql = @"
                INSERT INTO HistoricoCompras (Titulo, PrecoFinal, LegoId, UtilizadorId)
                VALUES (@Titulo, @PrecoFinal, @LegoId, @UtilizadorId);";

            var parameters = new
            {
                historicoCompra.Titulo,
                historicoCompra.PrecoFinal,
                historicoCompra.LegoId,
                historicoCompra.UtilizadorId
            };

            await _db.SaveData(sql, parameters);
            return true;
        }
    }
}
