using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUtilizadorRepository
    {
        Task<List<Utilizador>> FindAll();
        Task<string> GetUsernameById(int userId);
        Task<Utilizador> GetUtilizadorById(int userId);
        Task<bool> InsertUtilizador(String Nome, String Username, String Password, String DataDeNascimento, String Telemovel, String Email, String Nif, String Morada, int IsAdministrador);
        Task<bool> UpdateUtilizador(Utilizador utilizador);
        Task<bool> CheckUsernameExists(string username);
        Task<bool> ValidateCredentials(string username, string password);
        Task<List<Utilizador>> GetUtilizadoresByNome(string nome);
        Task<Utilizador> GetUtilizadorByUsername(string username);
        Task<int> GetIdByUsername(string username);
        Task<bool> DeleteUser(int userId);
    }
}
