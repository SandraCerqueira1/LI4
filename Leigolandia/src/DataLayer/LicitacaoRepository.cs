namespace DataLayer;
public class LicitacaoRepository : ILicitacaoRepository
{
    private ISqlDataAccess _db;
    public LicitacaoRepository(ISqlDataAccess db)
    {

        _db = db;
    }

    public Task<List<Licitacao>> FindAll()
    {
        string sql = "select * from Licitacoes";
        return _db.LoadData<Licitacao, dynamic>(sql, new { });
    }

    public async Task<Licitacao> GetLicitacaoById(int licitacaoId)
    {
        string sql = "SELECT * FROM Licitacoes WHERE Id = @Id";
        var parameters = new { Id = licitacaoId };
        List<Licitacao> licitacoes = await _db.LoadData<Licitacao, dynamic>(sql, parameters);
        return licitacoes.FirstOrDefault();
    }


    //public async Task<bool> InsertLicitacao(Licitacao licitacao)
    //{
    //    string sql = @"
    //            INSERT INTO Licitacoes (IdLicitador, IdLeilao, Valor)
    //            VALUES (@IdLicitador, @IdLeilao, @Valor);";

    //    var parameters = new
    //    {
    //        licitacao.IdLicitador,
    //        licitacao.IdLeilao,
    //        licitacao.Valor
    //    };

    //    await _db.SaveData(sql, parameters);
    //    return true;
    //}
    public async Task<List<Licitacao>> GetLicitacoesByLeilao(int leilaoId)
    {
        string sql = "SELECT * FROM Licitacoes WHERE IdLeilao = @LeilaoId";
        var parameters = new { LeilaoId = leilaoId };
        List<Licitacao> licitacoes = await _db.LoadData<Licitacao, dynamic>(sql, parameters);
        return licitacoes;
    }


    /// <summary>
    /// Obtém a licitação vencedora para um leilão específico com base no valor mais alto.
    /// </summary>
    /// <param name="leilaoId">O identificador único do leilão para o qual se deseja obter a licitação vencedora.</param>
    /// <returns>A licitação vencedora com o valor mais alto para o leilão especificado, ou null se não houver licitações associadas.</returns>

    public async Task<Licitacao> GetLicitacaoVencedoraByLeilao(int leilaoId)
    {
        string sql = "SELECT TOP 1 * FROM Licitacoes WHERE IdLeilao = @LeilaoId ORDER BY Valor DESC";
        var parameters = new { LeilaoId = leilaoId };
        List<Licitacao> licitacoes = await _db.LoadData<Licitacao, dynamic>(sql, parameters);
        return licitacoes.FirstOrDefault();
    }

    /// <summary>
    /// Registra ou atualiza uma licitação para um leilão específico.
    /// Utiliza a cláusula MERGE para combinar as operações de INSERT e UPDATE em uma única instrução,
    /// garantindo que apenas a maior licitação para um leilão seja mantida na tabela de licitações.
    /// </summary>
    /// <param name="licitacao">A licitação a ser registrada ou atualizada.</param>
    /// <returns>True se a operação foi bem-sucedida; False em caso de erro.</returns>
    public async Task<bool> PutLicitacao(Licitacao licitacao)
    {
        string sql = @"
        MERGE INTO Licitacoes AS target
        USING (VALUES (@IdLeilao)) AS source (IdLeilao)
        ON target.IdLeilao = source.IdLeilao
        WHEN MATCHED AND target.Valor < @Valor THEN
            UPDATE SET
                Valor = @Valor,
                IdLicitador = @IdLicitador
        WHEN NOT MATCHED THEN
            INSERT (IdLeilao, Valor, IdLicitador)
            VALUES (@IdLeilao, @Valor, @IdLicitador);";

        var parameters = new
        {
            licitacao.IdLeilao,
            licitacao.Valor,
            licitacao.IdLicitador
        };

        await _db.SaveData(sql, parameters);
        return true;
    }
    public async Task<bool> UpsertLicitacao(float Valor, int IdLicitador, int IdLeilao)
    {
        // Verifica se já existe uma entrada para o par IdLicitador e IdLeilao
        string checkIfExistsSql = @"
        SELECT COUNT(*)
        FROM Licitacoes
        WHERE IdLicitador = @IdLicitador AND IdLeilao = @IdLeilao;";

        var checkIfExistsParameters = new
        {
            IdLicitador,
            IdLeilao
        };

        int existingCount = await _db.ExecuteScalar<int, dynamic>(checkIfExistsSql, checkIfExistsParameters);

        if (existingCount > 0)
        {
            // Se já existe uma entrada, atualiza o valor
            string updateSql = @"
            UPDATE Licitacoes
            SET Valor = @Valor
            WHERE IdLicitador = @IdLicitador AND IdLeilao = @IdLeilao;";

            var updateParameters = new
            {
                Valor,
                IdLicitador,
                IdLeilao
            };

            await _db.SaveData(updateSql, updateParameters);
        }
        else
        {
            // Se não existe uma entrada, insere uma nova
            string insertSql = @"
            INSERT INTO Licitacoes (Valor, IdLicitador, IdLeilao)
            VALUES (@Valor, @IdLicitador, @IdLeilao);";

            var insertParameters = new
            {
                Valor,
                IdLicitador,
                IdLeilao
            };

            await _db.SaveData(insertSql, insertParameters);
        }

        // Retorna true, pois foi feito com sucesso
        return true;
    }







    public async Task<int> CountLicitacoesByLicitadorId(int idLicitador)
    {
        string sql = "SELECT Id FROM Licitacoes WHERE IdLicitador = @IdLicitador";

        var parameters = new { IdLicitador = idLicitador };

        // Execute the query and return the count
        var licitacoes = await _db.LoadData<int, dynamic>(sql, parameters);

        return licitacoes.Count;
    }

    public async Task<int> CountLeiloesGanhosPorUtilizador(int utilizadorId)
    {
        // Consulta SQL para contar o número de leilões ganhos por um utilizador
        string sql = @"
            SELECT COUNT(*) 
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

        // Execute a consulta e retorne o número de leilões ganhos
        var count = await _db.LoadData<int, dynamic>(sql, parameters);

        return count.FirstOrDefault();
    }

    public async Task<bool> DeleteLicitacoesByLeilaoId(int leilaoId)
    {
        try
        {
            string sql = "DELETE FROM Licitacoes WHERE IdLeilao = @LeilaoId";

            var parameters = new { LeilaoId = leilaoId };

            await _db.SaveData(sql, parameters);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao remover o licitacaoooooooo.");
            return false;
        }
    }




}
