using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CategoriasRepository : ICategoriasRepository
    {
        private readonly ISqlDataAccess _db;

        public CategoriasRepository(ISqlDataAccess db)
        {
            _db = db;
        }


        public async Task<List<string>> GetAllCategoriaNames()
        {
            string sql = "SELECT Nome FROM Categorias";
            List<string> listaCategorias = (await _db.LoadData<string, dynamic>(sql, new { })).ToList();

            foreach (var categoria in listaCategorias)
            {
                Console.WriteLine(categoria);
            }

            return listaCategorias;
        }

        public async Task<Categoria> GetCategoriaById(int categoriaId)
        {
            string sql = "SELECT Id, Nome FROM Categorias WHERE Id = @Id";
            var parameters = new { Id = categoriaId };
            return (await _db.LoadData<Categoria, dynamic>(sql, parameters)).FirstOrDefault();
        }

        //pega no id atraves do nome
        public async Task<Categoria> GetCategoriaByName(string nome)
        {
            string sql = "SELECT Id FROM Categorias WHERE Nome = @Nome";
            var parameters = new { Nome = nome };
            return (await _db.LoadData<Categoria, dynamic>(sql, parameters)).FirstOrDefault();
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            // Implemente a lógica para recuperar todas as categorias da base de dados
            string sql = "SELECT * FROM Categorias";
            return await _db.LoadData<Categoria, dynamic>(sql, new {});
        }

        public async Task<int> GetCategoryIdByName(string nome)
        {
            string sql = "SELECT Id FROM Categorias WHERE Nome = @Nome";
            var parameters = new { Nome = nome };

            var result = await _db.LoadData<int, dynamic>(sql, parameters);

            // Se houver um resultado, retorna o primeiro Id encontrado. Caso contrário, retorna null.
            return result.FirstOrDefault();
        }

        public async Task<bool> InsertCategoria(string nomeCategoria)
        {
            string sql = @"
        INSERT INTO Categorias (Nome)
        VALUES (@Nome);";

            var parameters = new
            {
                Nome = nomeCategoria
            };

            await _db.SaveData(sql, parameters);
            return true;
        }


    }
}
