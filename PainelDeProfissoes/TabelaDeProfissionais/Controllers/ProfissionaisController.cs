using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabelaDeProfissionais.Context;
using TabelaDeProfissionais.Models;
using TabelaDeProfissionais.ViewModels;

namespace TabelaDeProfissionais.Controllers
{
    public class ProfissionaisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfissionaisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string especialidade, int page = 1)
        {
            var especialidades = _context.Profissionais
                                          .Select(p => p.Especialidade)
                                          .Distinct()
                                          .ToList();

            // Defina a especialidade selecionada (se houver) ou uma string vazia.
            var especialidadeSelecionada = string.IsNullOrEmpty(especialidade) ? "" : especialidade;

            var profissionaisQuery = _context.Profissionais.AsQueryable();

            // Filtrar por especialidade, se selecionada
            if (!string.IsNullOrEmpty(especialidadeSelecionada))
            {
                profissionaisQuery = profissionaisQuery.Where(p => p.Especialidade == especialidadeSelecionada);
            }

            // Calcular a quantidade total de páginas
            var totalProfissionais = profissionaisQuery.Count();
            var totalPagina = (int)Math.Ceiling(totalProfissionais / 10.0); // Supondo 10 por página

            // Obter os profissionais para a página atual
            var profissionais = profissionaisQuery
                                .Skip((page - 1) * 10)
                                .Take(10)
                                .ToList();

            var viewModel = new ProfissionaisViewModel
            {
                Profissionais = profissionais,
                Especialidades = especialidades,
                EspecialidadeSelecionada = especialidadeSelecionada, // Passa a especialidade selecionada
                PaginaAtual = page,
                TotalPagina = totalPagina
            };

            return View(viewModel);
        }




        // Exibir o formulário de cadastro
        public IActionResult Cadastrar()
        {

            var especialidades = _context.Profissionais
                                        .AsNoTracking()
                                        .Select(p => p.Especialidade)
                                        .Distinct()
                                        .ToList();

            ViewBag.Especialidades = especialidades;

            return View();
        }



        // Processar o cadastro
        [HttpPost]
        public async Task<IActionResult> Cadastrar(Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profissional);
                await _context.SaveChangesAsync();
                TempData["CadastroMensagem"] = "Profissional cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            var especialidades = _context.Profissionais
                                        .AsNoTracking()
                                        .Select(p => p.Especialidade)
                                        .Distinct()
                                        .ToList();
            ViewBag.Especialidades = especialidades;

            return View();
        }

        public JsonResult ObterTipoDocumento(string especialidade)
        {
            var tipoDocumento = _context.Profissionais
                                        .Where(p => p.Especialidade == especialidade)
                                        .Select(p => p.TipoDocumento)
                                        .FirstOrDefault(); 

            return Json(tipoDocumento ?? string.Empty);
        }



        public async Task<IActionResult> Editar(int id)
        {
            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null)
            {
                return NotFound("Profissional não encontrado!");
            }
            return View(profissional);
        }

        // Editar
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Profissional profissional)
        {
            if (id != profissional.Id)
            {
                return NotFound("Profissional não encontrado!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissional);
                    await _context.SaveChangesAsync();
                    TempData["EditarMensagem"] = "Profissional editado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalExiste(profissional.Id))
                    {
                        return NotFound("Profissional não encontrado!");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(profissional);
        }

        //Deletar
        public async Task<IActionResult> Deletar(int id)
        {
            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null)
            {
                return NotFound("Profissional não encontrado!");
            }
            return View(profissional);
        }

        //Deletar Confirmacao
        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> DeletarConfirmacao(int id)
        {
            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null)
            {
                return NotFound();
            }

            _context.Profissionais.Remove(profissional);
            await _context.SaveChangesAsync();
            TempData["DeleteMensagem"] = "Profissional deletado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        private bool ProfissionalExiste(int id)
        {
            return _context.Profissionais.Any(e => e.Id == id);
        }


    }
}
