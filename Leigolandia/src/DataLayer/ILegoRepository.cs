using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ILegoRepository
    {
        Task<List<Lego>> FindAll();
        Task<Lego> GetLegoById(int legoId);
        Task<bool> DeleteLego(int legoId);
        Task<bool> InsertLego(Lego lego);
        Task<int> InsertLegoAndReturnId(Lego lego);
        Task<List<Lego>> GetLegosByCategoria(int categoriaId);
        Task<int?> GetIdByNome(string nome);
    }
}
