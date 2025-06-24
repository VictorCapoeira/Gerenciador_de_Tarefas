using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SenaiCrud.Data;
using SenaiCrud.Models;
using SenaiCrud.Models.Tables;

namespace SenaiCrud.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SenaiCrudDbContext _context; // Adicione esta linha

    public HomeController(ILogger<HomeController> logger, SenaiCrudDbContext context) // Modifique o construtor
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var tarefas = _context.Tarefas.OrderByDescending(t => t.DataCriacao).ToList();
        return View(tarefas);
    }

    [HttpPost]
    public IActionResult Create(string Titulo, string? Descricao)
    {
        if (string.IsNullOrWhiteSpace(Titulo))
        {
            ModelState.AddModelError("Titulo", "O título é obrigatório.");
            return RedirectToAction("Index");
        }

        var tarefa = new Tarefas
        {
            Titulo = Titulo,
            Descricao = Descricao,
            DataCriacao = DateTime.Now,
            Concluida = false
        };

        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
