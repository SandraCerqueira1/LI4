using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ILicitacaoRepository
    {
        Task<List<Licitacao>> FindAll();
        Task<Licitacao> GetLicitacaoById(int licitacaoId);
        //Task<bool> InsertLicitacao(Licitacao licitacao);
        Task<List<Licitacao>> GetLicitacoesByLeilao(int leilaoId);
        Task<Licitacao> GetLicitacaoVencedoraByLeilao(int leilaoId);
        Task<bool> PutLicitacao(Licitacao licitacao);
        Task<bool> UpsertLicitacao(float Valor, int IdLicitador, int IdLeilao);




        Task<int> CountLicitacoesByLicitadorId(int idLicitador);
        Task<int> CountLeiloesGanhosPorUtilizador(int utilizadorId);
        Task<bool> DeleteLicitacoesByLeilaoId(int leilaoId);
    }
}
