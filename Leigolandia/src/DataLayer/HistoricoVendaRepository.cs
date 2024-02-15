using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class HistoricoVendaRepository : IHistoricoVendaRepository
    {
        private readonly ISqlDataAccess _db;

        public HistoricoVendaRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<HistoricoVenda>> FindAll()
        {
            string sql = "SELECT * FROM HistoricoVendas";
            return _db.LoadData<HistoricoVenda, dynamic>(sql, new { });
        }

        public async Task<HistoricoVenda> GetHistoricoVendaById(int historicoVendaId)
        {
            string sql = "SELECT * FROM HistoricoVendas WHERE Id = @Id";
            var parameters = new { Id = historicoVendaId };
            List<HistoricoVenda> historicoVendas = await _db.LoadData<HistoricoVenda, dynamic>(sql, parameters);
            return historicoVendas.FirstOrDefault();
        }

        public async Task<bool> DeleteHistoricoVenda(int historicoVendaId)
        {
            string sql = "DELETE FROM HistoricoVendas WHERE Id = @Id;";
            var parameters = new { Id = historicoVendaId };

            await _db.SaveData(sql, parameters);
            return true;
        }

        public async Task<bool> InsertHistoricoVenda(HistoricoVenda historicoVenda)
        {
            string sql = @"
            INSERT INTO HistoricoVendas (Titulo, PrecoFinal, LegoId, UtilizadorId)
            VALUES (@Titulo, @PrecoFinal, @LegoId, @UtilizadorId);";

            var parameters = new
            {
                historicoVenda.Titulo,
                historicoVenda.PrecoFinal,
                historicoVenda.LegoId,
                historicoVenda.UtilizadorId
            };

            await _db.SaveData(sql, parameters);
            return true;
        }
    }
}
