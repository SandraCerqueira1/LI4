using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace DataLayer
{
    public class LeilaoRepository : ILeilaoRepository
    {

        private readonly ISqlDataAccess _db;

        public LeilaoRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        // Encontrar todos os leiloes
        public Task<List<Leilao>> FindAll()
        {

            string sql = "SELECT * FROM Leiloes";
            return _db.LoadData<Leilao, dynamic>(sql, new { });
        }

        public async Task<Leilao> GetLeilaoById(int leilaoId)
        {
            string sql = "SELECT * FROM Leiloes WHERE Id = @LeilaoId";
            var parameters = new { LeilaoId = leilaoId };
            List<Leilao> leiloes = await _db.LoadData<Leilao, dynamic>(sql, parameters);

            // Retorna o primeiro leilão encontrado ou null se não houver correspondência.
            return leiloes.FirstOrDefault();
        }

        public async Task<int> GetLegoIdByLeilao(int leilaoId)
        {
            string sql = "SELECT LegoId FROM Leiloes WHERE Id = @LeilaoId";
            var parameters = new { LeilaoId = leilaoId };
            List<int> legoIds = await _db.LoadData<int, dynamic>(sql, parameters);

            // Retorna o primeiro LegoId encontrado ou null se não houver correspondência.
            return legoIds.FirstOrDefault();
        }





        public async Task<bool> PutLeilao(Leilao leilao)
        {
            string sql = @"
        INSERT INTO Leiloes (Titulo, DataInicio, DataFiM, PrecoBase, PrecoAtual, Descricao, LegoId, Estado, VendedorId)
        VALUES (@Titulo, @DataInicio, @DataFiM, @PrecoBase, @PrecoAtual, @Descricao, @LegoId, @Estado, @VendedorId);
    ";

            var parameters = new
            {
                leilao.Titulo,
                leilao.DataInicio,
                leilao.DataFiM,
                leilao.PrecoBase,
                leilao.PrecoAtual,
                leilao.Descricao,
                leilao.LegoId,
                leilao.Estado,
                leilao.VendedorId
            };

            await _db.SaveData(sql, parameters);
            return true;
        }



        public async Task<List<Leilao>> GetLeiloesADecorrer()
        {
            string sql = "SELECT * FROM Leiloes WHERE Estado = 'Decorrer'";
            return await _db.LoadData<Leilao, dynamic>(sql, new { });
        }

        //-----------------------------------

        public async Task<List<LeilaoViewModel>> GetLeiloesADecorrerComImagem()
        {
            string query = "SELECT Leiloes.*, Lego.Imagem " +
                           "FROM Leiloes " +
                           "INNER JOIN Lego ON Leiloes.LegoId = Lego.Id " +
                           "WHERE Leiloes.Estado = 'Decorrer'";

            var results = await _db.LoadData<LeilaoViewModel, dynamic>(query, new { });

            return results.ToList();
        }


        //-----------------------------------

        public async Task<List<LeilaoViewModel>> GetLeiloesADecorrerComImagemPorUtilizador(int idUtilizador)
        {
            string query = "SELECT Leiloes.*, Lego.Imagem " +
                           "FROM Leiloes " +
                           "INNER JOIN Lego ON Leiloes.LegoId = Lego.Id " +
                           "WHERE Leiloes.Estado = 'Decorrer' AND Leiloes.VendedorId = @IdUtilizador";

            var results = await _db.LoadData<LeilaoViewModel, dynamic>(query, new { IdUtilizador = idUtilizador });

            return results.ToList();
        }

        //-----------------------------------


        public async Task<List<LeilaoViewModel>> GetLeiloesParticipadosPorUtilizador(int idUtilizador)
        {
            try
            {
                // Consulta SQL para obter os IDs de leilão em que o usuário está a participar
                string subquery = "SELECT IdLeilao FROM Licitacoes WHERE IdLicitador = @IdUtilizador";

                // Consulta principal para obter os detalhes dos leilões
                string query = "SELECT Leiloes.*, Lego.Imagem " +
                               "FROM Leiloes " +
                               "INNER JOIN Lego ON Leiloes.LegoId = Lego.Id " +
                               $"WHERE Leiloes.Estado = 'Decorrer' AND Leiloes.Id IN ({subquery})";

                var results = await _db.LoadData<LeilaoViewModel, dynamic>(query, new { IdUtilizador = idUtilizador });

                return results.ToList();
            }
            catch (Exception ex)
            {
                // Adicione tratamento de exceção ou imprima informações de erro para diagnóstico
                Console.WriteLine($"Erro ao obter leilões participados: {ex.Message}");
                throw; // Re-throw para propagação do erro
            }
        }

        //-----------------------------------

        public async Task<List<LeilaoViewModel>> GetLeiloesTerminadosParticipadosPorUtilizador(int idUtilizador)
        {
            try
            {
                // Consulta SQL para obter os IDs de leilão em que o usuário está a participar
                string subquery = "SELECT IdLeilao FROM Licitacoes WHERE IdLicitador = @IdUtilizador";

                // Consulta principal para obter os detalhes dos leilões
                string query = "SELECT Leiloes.*, Lego.Imagem " +
                               "FROM Leiloes " +
                               "INNER JOIN Lego ON Leiloes.LegoId = Lego.Id " +
                               $"WHERE Leiloes.Estado = 'Terminado' AND Leiloes.Id IN ({subquery})";

                var results = await _db.LoadData<LeilaoViewModel, dynamic>(query, new { IdUtilizador = idUtilizador });

                return results.ToList();
            }
            catch (Exception ex)
            {
                // Adicione tratamento de exceção ou imprima informações de erro para diagnóstico
                Console.WriteLine($"Erro ao obter leilões participados: {ex.Message}");
                throw; // Re-throw para propagação do erro
            }
        }
        //-----------------------------------

        public async Task<List<LeilaoViewModel>> GetLeiloesATerminadosComImagemPorUtilizador(int idUtilizador)
        {
            string query = "SELECT Leiloes.*, Lego.Imagem " +
                           "FROM Leiloes " +
                           "INNER JOIN Lego ON Leiloes.LegoId = Lego.Id " +
                           "WHERE Leiloes.Estado = 'Terminado' AND Leiloes.VendedorId = @IdUtilizador";

            var results = await _db.LoadData<LeilaoViewModel, dynamic>(query, new { IdUtilizador = idUtilizador });

            return results.ToList();
        }

        public Task DeleteLeilao(int leilaoId)
        {
            string sql = "DELETE FROM Leiloes WHERE Id = @LeilaoId";

            var parameters = new { LeilaoId = leilaoId };

            return _db.SaveData(sql, parameters);
        }

        public Task<List<Leilao>> GetLeilaoByLegoId(int legoId)
        {
            string sql = "SELECT * FROM Leiloes WHERE LegoId = @LegoId";

            var parameters = new { LegoId = legoId };

            return _db.LoadData<Leilao, dynamic>(sql, parameters);
        }

        public async Task<int> CountLeiloes()
        {
            string sql = "SELECT Id FROM Leiloes";

            // Executa a consulta e obtém os IDs como uma lista
            List<int> leilaoIds = (await _db.LoadData<int, dynamic>(sql, new { })).ToList();

            // Conta os elementos na lista de IDs
            int count = leilaoIds.Count;

            return count;
        }

        public async Task<int> GetVendedorIdById(int leilaoId)
        {
            string sql = "SELECT VendedorId FROM Leiloes WHERE Id = @LeilaoId";
            var parameters = new { LeilaoId = leilaoId };

            // Aqui, usamos LoadData para carregar um campo específico (VendedorId).
            List<int> vendedorIds = await _db.LoadData<int, dynamic>(sql, parameters);

            // Retorna o primeiro VendedorId encontrado ou null se não houver correspondência.
            return vendedorIds.FirstOrDefault();
        }

        //Update PrecoAtual após uma licitação
        public async Task<bool> UpdatePrecoAtual(int leilaoId, float novoPrecoAtual)
        {
            try
            {
                string updateSql = @"
            UPDATE Leiloes
            SET PrecoAtual = @NovoPrecoAtual
            WHERE Id = @LeilaoId;
        ";

                var parameters = new
                {
                    NovoPrecoAtual = novoPrecoAtual,
                    LeilaoId = leilaoId
                };

                await _db.SaveData(updateSql, parameters);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task UpdateEstado(int leilaoId, string novoEstado)
        {

            string updateSql = @"UPDATE Leiloes
                                SET Estado = @NovoEstado
                                WHERE Id = @LeilaoId;
                                ";

            var parameters = new
            {
                NovoEstado = novoEstado,
                LeilaoId = leilaoId
            };

            await _db.SaveData(updateSql, parameters);
        }



        public async Task<int> CountLeiloesByVendedorId(int vendedorId)
        {
            string sql = "SELECT Id FROM Leiloes WHERE VendedorId = @VendedorId";

            var parameters = new { VendedorId = vendedorId };

            // Execute the query and return the count
            var leiloes = await _db.LoadData<int, dynamic>(sql, parameters);

            return leiloes.Count;
        }

        public async Task<int> CalcularLucroTotalPorUtilizador(int utilizadorId)
        {
            string sql = @"
SELECT COALESCE(SUM(CAST(L.PrecoAtual AS INT)), 0) AS LucroTotal
FROM Leiloes AS L
WHERE L.VendedorId = @UtilizadorId
    AND L.Estado = 'Terminado';";

            var parameters = new { UtilizadorId = utilizadorId };

            var lucroTotal = await _db.LoadData<int, dynamic>(sql, parameters);

            return lucroTotal.FirstOrDefault();
        }

        public async Task<decimal> CalcularLucroMedioPorUtilizador(int utilizadorId)
        {
            string sql = @"
SELECT COALESCE(
    CAST(SUM(CAST(L.PrecoAtual AS DECIMAL(18,2))) / NULLIF(COUNT(*), 0) AS DECIMAL(18,2)),
    0
) AS LucroMedio
FROM Leiloes AS L
WHERE L.VendedorId = @UtilizadorId
    AND L.Estado = 'Terminado';";

            var parameters = new { UtilizadorId = utilizadorId };

            var lucroMedio = await _db.LoadData<decimal, dynamic>(sql, parameters);

            return lucroMedio.FirstOrDefault();
        }

        public async Task<int> CalcularGastoTotalPorUtilizador(int utilizadorId)
        {
            string sql = @"
SELECT COALESCE(
    CAST(SUM(CAST(L.PrecoAtual AS DECIMAL(18,2))) AS INT),
    0
) AS GastoTotal
FROM Leiloes AS L
INNER JOIN Licitacoes AS Lic ON L.Id = Lic.IdLeilao
WHERE Lic.IdLicitador = @UtilizadorId
    AND L.Estado = 'Terminado'
    AND Lic.Valor = (
        SELECT MAX(Valor)
        FROM Licitacoes
        WHERE IdLeilao = L.Id
    );";

            var parameters = new { UtilizadorId = utilizadorId };

            var gastoTotal = await _db.LoadData<int, dynamic>(sql, parameters);

            return gastoTotal.FirstOrDefault();
        }

        public async Task<decimal> CalcularGastoMedioPorUtilizador(int utilizadorId)
        {
            string sql = @"
SELECT COALESCE(
    CAST(SUM(CAST(L.PrecoAtual AS DECIMAL(18,2))) / NULLIF(COUNT(*), 0) AS DECIMAL(18,2)),
    0
) AS GastoMedio
FROM Leiloes AS L
INNER JOIN Licitacoes AS Lic ON L.Id = Lic.IdLeilao
WHERE Lic.IdLicitador = @UtilizadorId
    AND L.Estado = 'Terminado'
    AND Lic.Valor = (
        SELECT MAX(Valor)
        FROM Licitacoes
        WHERE IdLeilao = L.Id
    );";

            var parameters = new { UtilizadorId = utilizadorId };

            var gastoMedio = await _db.LoadData<decimal, dynamic>(sql, parameters);

            return gastoMedio.FirstOrDefault();
        }

        public async Task<bool> DeleteLeilaoById(int leilaoId)
        {
            try
            {
                string sql = "DELETE FROM Leiloes WHERE Id = @LeilaoId";

                var parameters = new { LeilaoId = leilaoId };

                await _db.SaveData(sql, parameters);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async Task<List<LeilaoViewModel>> GetLeiloesGanhosPorUtilizador(int utilizadorId)
        {
            string sql = @"
        SELECT L.*, Lego.Imagem
        FROM Leiloes AS L
        INNER JOIN Licitacoes AS Lic ON L.Id = Lic.IdLeilao
        INNER JOIN Lego ON L.LegoId = Lego.Id
        WHERE Lic.IdLicitador = @UtilizadorId
            AND L.Estado = 'Terminado'
            AND Lic.Valor = (
                SELECT MAX(Valor)
                FROM Licitacoes
                WHERE IdLeilao = L.Id
            );";

            var parameters = new { UtilizadorId = utilizadorId };

            List<LeilaoViewModel> leiloesGanhos = await _db.LoadData<LeilaoViewModel, dynamic>(sql, parameters);

            return leiloesGanhos;
        }



        public async Task<List<LeilaoViewModel>> GetLeiloesADecorrerComImagemPorCategoria(int idCategoria)
        {
            string query = "SELECT Leiloes.*, Lego.Imagem " +
                            "FROM Leiloes " +
                            "INNER JOIN Lego ON Leiloes.LegoId = Lego.Id " +
                            "WHERE Leiloes.Estado = 'Decorrer' AND Lego.CategoriaId = @IdCategoria";

            //var results = await _db.LoadData<LeilaoViewModel, dynamic>(query, new { CategoriaId = idCategoria });
            var results = await _db.LoadData<LeilaoViewModel, dynamic>(query, new { IdCategoria = idCategoria });

            return results.ToList();
        }




    }






}
