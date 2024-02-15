using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class HistoricoVenda 
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public float PrecoFinal { get; set; }
        public int LegoId { get; set; }
        public int UtilizadorId { get; set; }
    }
}
