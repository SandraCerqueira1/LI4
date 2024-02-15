using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DataLayer
{
    public class UtilizadorRepository : IUtilizadorRepository
    {

        private readonly ISqlDataAccess _db;

        public UtilizadorRepository(ISqlDataAccess db)
        {
            _db = db;
        }



        // Encontrar todos os utilizadores
        public Task<List<Utilizador>> FindAll()
        {

            string sql = "SELECT * FROM Utilizadores";
            return _db.LoadData<Utilizador, dynamic>(sql, new { });
        }

        // Devolve o username dado um id
        public async Task<string> GetUsernameById(int userId)
        {
            string sql = "SELECT Username FROM Utilizadores WHERE Id = @Id";
            var parameters = new { Id = userId };
            List<string> usernames = await _db.LoadData<string, dynamic>(sql, parameters);

            // Verifica se há algum resultado e retorna a primeira string (Username) se existir
            return usernames.FirstOrDefault();
        }

        /// <summary>
        /// Obtém um utilizador da base de dados com base no ID.
        /// </summary>
        /// <param name="userId">O ID do utilizador a ser obtido.</param>
        /// <returns>O UtilizadorModel correspondente ao ID fornecido.</returns>
        public async Task<Utilizador> GetUtilizadorById(int userId)
        {
            string sql = "SELECT * FROM Utilizadores WHERE Id = @Id";
            var parameters = new { Id = userId };
            List<Utilizador> utilizadores = await _db.LoadData<Utilizador, dynamic>(sql, parameters);

            // Retorna o primeiro utilizador encontrado ou null se não houver correspondência.
            return utilizadores.FirstOrDefault();
        }



        /// <summary>
        /// Insere um novo utilizador na base de dados.
        /// </summary>
        /// <param name="utilizador">O objeto UtilizadorModel contendo os dados do novo utilizador.</param>
        /// <returns>True se a operação foi bem-sucedida; False em caso de erro.</returns>
        public async Task<bool> InsertUtilizador(String Nome, String Username, String Password, String DataDeNascimento, String Telemovel, String Email, String Nif, String Morada, int IsAdministrador)
        {
            string sql = @"
        INSERT INTO Utilizadores (Nome, Username, Password, DataDeNascimento, Telemovel, Email, Nif, Morada, IsAdministrador)
        VALUES (@Nome, @Username, @Password, @DataDeNascimento, @Telemovel, @Email, @Nif, @Morada, @IsAdministrador);";

            var parameters = new
            {
                Nome,
                Username,
                Password,
                DataDeNascimento,
                Telemovel,
                Email,
                Nif,
                Morada,
                IsAdministrador
            };

            await _db.SaveData(sql, parameters);

            // Retorna true, pois foi feita com sucesso
            return true;
        }

        /// <summary>
        /// Atualiza os dados de um utilizador existente na base de dados.
        /// </summary>
        /// <param name="utilizador">O objeto UtilizadorModel contendo os dados a serem atualizados.</param>
        /// <returns>True se a operação foi bem-sucedida; False em caso de erro.</returns>
        public async Task<bool> UpdateUtilizador(Utilizador utilizador)
        {
            string sql = @"
        UPDATE Utilizadores
        SET Nome = @Nome,
            Username = @Username,
            Password = @Password,
            DataDeNascimento = @DataDeNascimento,
            Telemovel = @Telemovel,
            Email = @Email,
            Nif = @Nif,
            Morada = @Morada,
            IsAdministrador = @IsAdministrador
        WHERE Id = @Id;";

            var parameters = new
            {
                utilizador.Id,
                utilizador.Nome,
                utilizador.Username,
                utilizador.Password,
                utilizador.DataDeNascimento,
                utilizador.Telemovel,
                utilizador.Email,
                utilizador.Nif,
                utilizador.Morada,
                utilizador.IsAdministrador
            };

            await _db.SaveData(sql, parameters);

            // Retorna true, pois a operação foi bem-sucedida (não houve exceção)
            return true;
        }

        //PODE VIR A SER UTIL NO REGISTO

        // <summary>
        /// Verifica se um nome de utilizador já existe na base de dados.
        /// </summary>
        /// <param name="username">O nome de utilizador a ser verificado.</param>
        /// <returns>True se o nome de utilizador existir; False se não existir.</returns>
        public async Task<bool> CheckUsernameExists(string username)
        {
            string sql = "SELECT 1 FROM Utilizadores WHERE Username = @Username";
            var parameters = new { Username = username };
            List<int> result = await _db.LoadData<int, dynamic>(sql, parameters);

            // Retorna true se houver algum resultado (nome de utilizador existente)
            return result.Any();
        }

        //Valida Credenciais
        /// <summary>
        /// Valida as credenciais de um utilizador na base de dados.
        /// </summary>
        /// <param name="username">O nome de utilizador a ser verificado.</param>
        /// <param name="password">A palavra-passe a ser verificada.</param>
        /// <returns>True se as credenciais são válidas; False se não são válidas.</returns>
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            string sql = "SELECT 1 FROM Utilizadores WHERE Username = @Username AND Password = @Password";
            var parameters = new { Username = username, Password = password };
            List<int> result = await _db.LoadData<int, dynamic>(sql, parameters);

            // Retorna true se houver algum resultado (credenciais válidas)
            return result.Any();
        }

        //PODE VIR A SER UTIL NA BARRA DE PESQUISAS-> pega no nome e da-nos o user
        public async Task<List<Utilizador>> GetUtilizadoresByNome(string nome)
        {
            string sql = "SELECT * FROM Utilizadores WHERE Nome = @Nome";
            var parameters = new { Nome = nome };
            List<Utilizador> utilizadores = await _db.LoadData<Utilizador, dynamic>(sql, parameters);

            // Retorna a lista de utilizadores encontrados
            return utilizadores;
        }

        public async Task<Utilizador> GetUtilizadorByUsername(string username)
        {
            string sql = "SELECT * FROM Utilizadores WHERE Username = @Username";
            var parameters = new { Username = username };
            List<Utilizador> utilizadores = await _db.LoadData<Utilizador, dynamic>(sql, parameters);

            // Retorna o primeiro utilizador encontrado ou null se não houver correspondência.
            return utilizadores.FirstOrDefault();
        }

        public async Task<int> GetIdByUsername(string username)
        {
            string sql = "SELECT Id FROM Utilizadores WHERE Username = @Username";
            var parameters = new { Username = username };
            List<int> id = await _db.LoadData<int, dynamic>(sql, parameters);

           
            return id.FirstOrDefault();


        }
        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                // Lógica adicional, se necessário, antes de excluir o utilizador

                // Exclua as licitações associadas ao utilizador em leilões que ainda estão em andamento
                var deleteLicitacoesSql = "DELETE FROM Licitacoes WHERE IdLicitador = @UserId AND IdLeilao IN (SELECT Id FROM Leiloes WHERE Estado = 'Decorrer')";
                await _db.SaveData(deleteLicitacoesSql, new { UserId = userId });

                // Exclua o utilizador pelo ID
                var deleteUserSql = "DELETE FROM Utilizadores WHERE Id = @UserId";
                await _db.SaveData(deleteUserSql, new { UserId = userId });

                // Outras operações necessárias, se houver

                return true;  // A exclusão foi bem-sucedida
            }
            catch (Exception ex)
            {
                // Lidar com exceções ou erros, se necessário
                Console.WriteLine($"Erro ao excluir utilizador: {ex.Message}");
                return false;  // A exclusão falhou
            }
        }
       

    }
}
