using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public interface IEstatisticasRepository
    {
        Task<List<Estatisticas>> FindAll();
        Task<Estatisticas> GetEstatisticasByUserId(int userId);
        Task<bool> InsertEstatisticas(Estatisticas estatisticas);
        Task<bool> UpdateEstatisticas(Estatisticas estatisticas);
       
    }


}
