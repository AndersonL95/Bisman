using System.Collections.Generic;
using System.Threading.Tasks;
using Bisman.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bisman.Models;
using Bisman.Services.Exceptions;
using System.Diagnostics;
using Bisman.Models.ViewModels;

namespace Bisman.Controllers
{
    public class ServicosController : Controller
    {
        private readonly ServicoService _servicoService;
        IHttpContextAccessor HttpContextAccessor;

        public ServicosController(ServicoService servicoService, IHttpContextAccessor httpContextAccessor)
        {
            _servicoService = servicoService;
            HttpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IActionResult> Index()
        {
            var idUsuario = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var list = await _servicoService.FindAllAsync(int.Parse(idUsuario));
            return View(list);
        }
        
        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servico servico)
        {
            if (!ModelState.IsValid)
            {  
                return View(servico);
            }
            var idUsuario = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            servico.UsuarioId = (int.Parse(idUsuario));
            await _servicoService.InsertAsync(servico);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido"});
            }

            var obj = await _servicoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicoService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _servicoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(obj);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _servicoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            
            return View(obj);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servico servico)
        {
            if (!ModelState.IsValid)
            {
                return View(servico);
            }

            if (id != servico.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não compatível" });
            }

            try
            {
                await _servicoService.UpdateAsync(servico);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}