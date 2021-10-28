using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UtilizandoADOProjetoMVC.Models;
using UtilizandoADOProjetoMVC.Repository;

namespace UtilizandoADOProjetoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        private readonly IRepository<Employee> repository;

        public HomeController(IConfiguration config, IRepository<Employee> repository)
        {
            this.config = config;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var employees = repository.RetornarTodos();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Employee employee)
        {
            if (ModelState.IsValid)
            {
                repository.Inserir(employee);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detalhe(int? id)
        {
            
            var employee = repository.RetornarPorId(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Editar(int? Id)
        {
            var employee = repository.RetornarPorId(Id);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Employee employee)
        {
            repository.Update(employee);
            return RedirectToAction("Detalhe",employee.Id);
        }

        [HttpGet]
        public IActionResult Deletar(int? Id)
        {
            var employee = repository.RetornarPorId(Id);
            return View(employee);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmaExclusao(Employee employee)
        {
            repository.Delete(employee);
            return RedirectToAction("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
