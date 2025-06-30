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
        // _logger = logger;
        _context = context;
    }

    public IActionResult Index(int? editId = null)
    {
        var tarefas = _context.Tarefas.OrderByDescending(t => t.DataCriacao).ToList();
        ViewBag.EditId = editId;
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

    [HttpPost]
    public IActionResult Concluir(int id)
    {
        var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa != null && !tarefa.Concluida)
        {
            tarefa.Concluida = true;
            tarefa.DataConclusao = DateTime.Now;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa == null)
        {
            return NotFound();
        }
        return View(tarefa);
    }

    [HttpPost]
    public IActionResult Edit(int id, string Titulo, string? Descricao, bool Concluida)
    {
        var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(Titulo))
        {
            ModelState.AddModelError("Titulo", "O título é obrigatório.");
            return RedirectToAction("Index", new { editId = id });
        }

        tarefa.Titulo = Titulo;
        tarefa.Descricao = Descricao;

        // Atualiza o status e a data de conclusão
        if (Concluida && !tarefa.Concluida)
        {
            tarefa.Concluida = true;
            tarefa.DataConclusao = DateTime.Now;
        }
        else if (!Concluida && tarefa.Concluida)
        {
            tarefa.Concluida = false;
            tarefa.DataConclusao = null;
        }

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);
        if (tarefa != null)
        {
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
