using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LegoRepository : ILegoRepository
    {
        private readonly ISqlDataAccess _db;

        public LegoRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<Lego>> FindAll()
        {
            string sql = "SELECT * FROM Lego";
            return _db.LoadData<Lego, dynamic>(sql, new { });
        }

        public async Task<Lego> GetLegoById(int legoId)
        {
            string sql = "SELECT * FROM Lego WHERE Id = @Id";
            var parameters = new { Id = legoId };
            List<Lego> legos = await _db.LoadData<Lego, dynamic>(sql, parameters);
            return legos.FirstOrDefault();
        }


        public async Task<bool> DeleteLego(int legoId)
        {
            string sql = "DELETE FROM Lego WHERE Id = @Id;";
            var parameters = new { Id = legoId };

            await _db.SaveData(sql, parameters);
            return true;
        }

        public async Task<bool> InsertLego(Lego lego)
        {
            string sql = @"
                INSERT INTO Lego (Nome, SerialNumber, Estado, AnoDeLancamento, NumeroDePecas, 
                                  IdadeRecomendada, Imagem, Descricao, NumeroDeMiniFiguras,
                                  Certificacao, Dimensoes, CategoriaId)
                VALUES (@Nome, @SerialNumber, @Estado, @AnoDeLancamento, @NumeroDePecas,
                        @IdadeRecomendada, @Imagem, @Descricao, @NumeroDeMiniFiguras,
                        @Certificacao, @Dimensoes, @CategoriaId);";

            var parameters = new
            {
                lego.Nome,
                lego.SerialNumber,
                lego.Estado,
                lego.AnoDeLancamento,
                lego.NumeroDePecas,
                lego.IdadeRecomendada,
                lego.Imagem,
                lego.Descricao,
                lego.NumeroDeMiniFiguras,
                lego.Certificacao,
                lego.Dimensoes,
                lego.CategoriaId
            };

            await _db.SaveData(sql, parameters);
            return true;
        }

        public async Task<int> InsertLegoAndReturnId(Lego lego)
        {
            try
            {
                string sql = @"
        INSERT INTO Lego (Nome, SerialNumber, Estado, AnoDeLancamento, NumeroDePecas, 
                          IdadeRecomendada, Imagem, Descricao, NumeroDeMiniFiguras,
                          Certificacao, Dimensoes, CategoriaId)
        OUTPUT INSERTED.Id
        VALUES (@Nome, @SerialNumber, @Estado, @AnoDeLancamento, @NumeroDePecas,
                @IdadeRecomendada, @Imagem, @Descricao, @NumeroDeMiniFiguras,
                @Certificacao, @Dimensoes, @CategoriaId);";

                var parameters = new
                {
                    lego.Nome,
                    lego.SerialNumber,
                    lego.Estado,
                    lego.AnoDeLancamento,
                    lego.NumeroDePecas,
                    lego.IdadeRecomendada,
                    lego.Imagem,
                    lego.Descricao,
                    lego.NumeroDeMiniFiguras,
                    lego.Certificacao,
                    lego.Dimensoes,
                    lego.CategoriaId
                };

                // Executar a consulta e obter o ID
                int legoId = await _db.ExecuteScalar<int, dynamic>(sql, parameters);

                return legoId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar consulta SQL: {ex.Message}");
                throw; // Rejoga a exceção para que ela seja tratada no código chamador
            }

        }








        //pode vir a  dar jeito para as categorias
        public async Task<List<Lego>> GetLegosByCategoria(int categoriaId)
        {
            string sql = "SELECT * FROM Lego WHERE CategoriaId = @CategoriaId";
            var parameters = new { CategoriaId = categoriaId };
            List<Lego> legos = await _db.LoadData<Lego, dynamic>(sql, parameters);
            return legos;
        }


        // <summary>
        /// Obtém o identificador único (Id) de um Lego com base no nome.
        /// </summary>
        /// <param name="nome">O nome do Lego.</param>
        /// <returns>O identificador único (Id) do Lego com o nome fornecido, ou null se não encontrado.</returns>
        public async Task<int?> GetIdByNome(string nome)
        {
            string sql = "SELECT Id FROM Lego WHERE Nome = @Nome";
            var parameters = new { Nome = nome };
            var result = await _db.LoadData<int, dynamic>(sql, parameters);
            return result.FirstOrDefault();
        }

    }
}
