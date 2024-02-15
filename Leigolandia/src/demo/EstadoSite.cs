using DataLayer;
namespace demo
{
    public class EstadoSite
    {
        //Atributos para a página com os diversos leilões
        public List<LeilaoViewModel> ListActiveAuctions { get; set; } = new List<LeilaoViewModel>();
        public List<Leilao> ListLeiloesPendentAuctions { get; set; } = new List<Leilao>();
        public List<LeilaoViewModel> ListLeiloesAgendados { get; set; } = new List<LeilaoViewModel>();




        //Atributos gerais ao site
        public string categoria_atual { get; set; } = "";
        public string loged_user { get; set; } = "";
        public int? atual_leilao { get; set; } = null;
    }
}
