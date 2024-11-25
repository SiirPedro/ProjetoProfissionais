using TabelaDeProfissionais.Models;

namespace TabelaDeProfissionais.ViewModels
{
    public class ProfissionaisViewModel
    {
        public List<Profissional> Profissionais { get; set; }
        public List<string> Especialidades { get; set; }
        public string EspecialidadeSelecionada { get; set; }
        public int PaginaAtual { get; set; }

        public int TotalPagina { get; set; }

    }
}
