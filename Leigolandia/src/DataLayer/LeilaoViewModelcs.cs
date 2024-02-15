using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LeilaoViewModel
    {
        //public Leilao Leilao { get; set; }
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public DateTime DataInicio { get; set; }
        public DateTime DataFiM { get; set; }
        public float PrecoBase { get; set; }
        public float PrecoAtual { get; set; }
        public string Descricao { get; set; } = "";
        public int LegoId { get; set; }
        public string Estado { get; set; } = "";
        public int VendedorId { get; set; }
        public string Imagem { get; set; }

        public LeilaoViewModel() { }

        public LeilaoViewModel(int id, string titulo, DateTime dataInicio, DateTime dataFim, float precoBase, float precoAtual, string descricao, int legoId, string estado, int vendedorId, string imagem)
        {
            //Leilao = leilao ?? throw new ArgumentNullException(nameof(leilao));
            Id = id;
            Titulo = titulo;
            DataInicio = dataInicio;
            DataFiM = dataFim;
            PrecoBase = precoBase;
            PrecoAtual = precoAtual;
            Descricao = descricao;
            LegoId = legoId;
            Estado = estado;
            VendedorId = vendedorId;
            Imagem = imagem;
        }
    }

}
