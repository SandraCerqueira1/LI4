using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Licitacao
    {
        public int Id { get; set; }
        public int IdLicitador { get; set; }
        public int IdLeilao { get; set; }
        public float Valor { get; set; }


        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Licitacao"/>.
        /// </summary>
        /// <param name="id">O identificador único da licitação.</param>
        /// <param name="valor">O valor da licitação.</param>
        /// <param name="idLicitador">O identificador do licitador associado à licitação.</param>
        /// <param name="idLeilao">O identificador do leilão associado à licitação.</param>
        public Licitacao(int id, int idLicitador, int idLeilao, float valor)
        {
            Id = id;
            IdLicitador = idLicitador;
            IdLeilao = idLeilao;
            Valor = valor;
        }

    }
}
