using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Lego
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public int SerialNumber { get; set; }
        public string Estado { get; set; } = "";
        public int AnoDeLancamento { get; set; }
        public int NumeroDePecas { get; set; }
        public int IdadeRecomendada { get; set; }
        public string Imagem { get; set; } = "";
        public string Descricao { get; set; } = "";
        public int NumeroDeMiniFiguras { get; set; }
        public string? Certificacao { get; set; }
        public string? Dimensoes { get; set; } 
        public int? CategoriaId { get; set; }

        //Construtor padrao
        public Lego() { }




        // Construtor parametrizado
        public Lego(int id, string nome, int serialNumber, string estado, int anoDeLancamento,
                    int numeroDePecas, int idadeRecomendada, string imagem, string descricao,
                    int numeroDeMiniFiguras, string? certificacao, string? dimensoes, int? categoriaId)
        {
            Id = id;
            Nome = nome;
            SerialNumber = serialNumber;
            Estado = estado;
            AnoDeLancamento = anoDeLancamento;
            NumeroDePecas = numeroDePecas;
            IdadeRecomendada = idadeRecomendada;
            Imagem = imagem;
            Descricao = descricao;
            NumeroDeMiniFiguras = numeroDeMiniFiguras;
            Certificacao = certificacao;
            Dimensoes = dimensoes;
            CategoriaId = categoriaId;
        }
    }
}
