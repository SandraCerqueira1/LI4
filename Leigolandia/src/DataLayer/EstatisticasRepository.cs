using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EstatisticasRepository : IEstatisticasRepository
    {
        private readonly ISqlDataAccess _db;

        public EstatisticasRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<Estatisticas>> FindAll()
        {
            string sql = "SELECT * FROM Estatisticas";
            return _db.LoadData<Estatisticas, dynamic>(sql, new { });
        }

        public async Task<Estatisticas> GetEstatisticasByUserId(int userId)
        {
            string sql = "SELECT * FROM Estatisticas WHERE UtilizadorId = @UserId";
            var parameters = new { UserId = userId };
            List<Estatisticas> estatisticas = await _db.LoadData<Estatisticas, dynamic>(sql, parameters);
            return estatisticas.FirstOrDefault();
        }

        public async Task<bool> InsertEstatisticas(Estatisticas estatisticas)
        {
            string sql = @"
                INSERT INTO Estatisticas (LucroTotal, LucroMedio, NumeroDeAvaliacoes, AvaliacaoMedia,
                                           LeiloesCriados, LeiloesParticipados, LeiloesGanhos, GastoTotal,
                                           GastoMedio, UtilizadorId)
                VALUES (@LucroTotal, @LucroMedio, @NumeroDeAvaliacoes, @AvaliacaoMedia,
                        @LeiloesCriados, @LeiloesParticipados, @LeiloesGanhos, @GastoTotal,
                        @GastoMedio, @UtilizadorId);";

            var parameters = new
            {
                estatisticas.LucroTotal,
                estatisticas.LucroMedio,
                estatisticas.NumeroDeAvaliacoes,
                estatisticas.AvaliacaoMedia,
                estatisticas.LeiloesCriados,
                estatisticas.LeiloesParticipados,
                estatisticas.LeiloesGanhos,
                estatisticas.GastoTotal,
                estatisticas.GastoMedio,
                estatisticas.UtilizadorId
            };

            await _db.SaveData(sql, parameters);
            return true;
        }


        //AQUI TEM DE SE ALTERAR PARA METER COMO IR BUSCAR OS DADOS PARA O UPDATE
        public async Task<bool> UpdateEstatisticas(Estatisticas estatisticas)
        {
            string sql = @"
                UPDATE Estatisticas
                SET LucroTotal = @LucroTotal,
                    LucroMedio = @LucroMedio,
                    NumeroDeAvaliacoes = @NumeroDeAvaliacoes,
                    AvaliacaoMedia = @AvaliacaoMedia,
                    LeiloesCriados = @LeiloesCriados,
                    LeiloesParticipados = @LeiloesParticipados,
                    LeiloesGanhos = @LeiloesGanhos,
                    GastoTotal = @GastoTotal,
                    GastoMedio = @GastoMedio
                WHERE UtilizadorId = @UtilizadorId;";

            var parameters = new
            {
                estatisticas.LucroTotal,
                estatisticas.LucroMedio,
                estatisticas.NumeroDeAvaliacoes,
                estatisticas.AvaliacaoMedia,
                estatisticas.LeiloesCriados,
                estatisticas.LeiloesParticipados,
                estatisticas.LeiloesGanhos,
                estatisticas.GastoTotal,
                estatisticas.GastoMedio,
                estatisticas.UtilizadorId
            };

            await _db.SaveData(sql, parameters);
            return true;
        }

    }
}
