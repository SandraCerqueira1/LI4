using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public interface ICategoriasRepository
    {
        Task<List<string>> GetAllCategoriaNames();
        Task<Categoria> GetCategoriaById(int categoriaId);
        Task<Categoria> GetCategoriaByName(string nome);
        Task<List<Categoria>> GetCategorias();
        Task<int> GetCategoryIdByName(string nome);
        Task<bool> InsertCategoria(string nomeCategoria);
    }
}
