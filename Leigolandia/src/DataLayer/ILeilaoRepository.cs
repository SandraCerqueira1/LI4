using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ILeilaoRepository
    {
        Task<List<Leilao>> FindAll();
        Task<Leilao> GetLeilaoById(int leilaoId);
        Task<int> GetLegoIdByLeilao(int leilaoId);
        Task<bool> PutLeilao(Leilao leilao);
        Task<List<Leilao>> GetLeiloesADecorrer();
        Task<List<LeilaoViewModel>> GetLeiloesADecorrerComImagem();
        Task DeleteLeilao(int leilaoId);
        Task<List<Leilao>> GetLeilaoByLegoId(int legoId);
        Task<int> CountLeiloes();

        Task<List<LeilaoViewModel>> GetLeiloesADecorrerComImagemPorUtilizador(int idUtilizador);
        Task<List<LeilaoViewModel>> GetLeiloesParticipadosPorUtilizador(int idUtilizador);
        Task<List<LeilaoViewModel>> GetLeiloesTerminadosParticipadosPorUtilizador(int idUtilizador);
        Task<List<LeilaoViewModel>> GetLeiloesATerminadosComImagemPorUtilizador(int idUtilizador);
        Task<int> GetVendedorIdById(int leilaoId);
        Task<bool> UpdatePrecoAtual(int leilaoId, float novoPrecoAtual);
        Task UpdateEstado(int leilaoId, string novoEstado);




        Task<int> CountLeiloesByVendedorId(int vendedorId);
        Task<int> CalcularLucroTotalPorUtilizador(int utilizadorId);
        Task<decimal> CalcularLucroMedioPorUtilizador(int utilizadorId);
        Task<int> CalcularGastoTotalPorUtilizador(int utilizadorId);
        Task<decimal> CalcularGastoMedioPorUtilizador(int utilizadorId);
        Task<bool> DeleteLeilaoById(int leilaoId);



        Task<List<LeilaoViewModel>> GetLeiloesGanhosPorUtilizador(int utilizadorId);

        Task<List<LeilaoViewModel>> GetLeiloesADecorrerComImagemPorCategoria(int idCategoria);

    }
}
