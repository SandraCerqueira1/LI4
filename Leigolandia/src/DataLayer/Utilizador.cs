using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string? Username { get; set; } = "";
        public string Password { get; set; } = "";
        public DateTime DataDeNascimento { get; set; }
        public int Telemovel { get; set; }
        public string Email { get; set; } = "";
        public int Nif { get; set; }
        public string Morada { get; set; } = "";
        public int IsAdministrador { get; set; }

        // Construtor parametrizado
        public Utilizador(string nome, string username, string password, DateTime dataDeNascimento, int telemovel,
                               string email, int nif, string morada, int isAdministrador)
        {
            Nome = nome;
            Username = username;
            Password = password;
            DataDeNascimento = dataDeNascimento;
            Telemovel = telemovel;
            Email = email;
            Nif = nif;
            Morada = morada;
            IsAdministrador = isAdministrador;
        }

        //Construtor sem parametros 
        public Utilizador()
        {
        }


    }

}
