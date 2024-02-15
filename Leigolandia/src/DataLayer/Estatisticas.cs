using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Estatisticas
    {
        public int Id { get; set; }
        public float LucroTotal { get; set; }
        public float LucroMedio { get; set; }
        public int NumeroDeAvaliacoes { get; set; }
        public float AvaliacaoMedia { get; set; }
        public int LeiloesCriados { get; set; }
        public int LeiloesParticipados { get; set; }
        public int LeiloesGanhos { get; set; }
        public float GastoTotal { get; set; }
        public float GastoMedio { get; set; }
        public int UtilizadorId { get; set; }
    }


}
