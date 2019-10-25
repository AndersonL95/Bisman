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
    public class ProdutosController : Controller
    {
        private readonly ProdutoService _produtoService;
        IHttpContextAccessor HttpContextAccessor;

        public ProdutosController(ProdutoService produtoService, IHttpContextAccessor httpContextAccessor)
        {
            _produtoService = produtoService;
            HttpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IActionResult> Index()
        {
            var idUsuario = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var list = await _produtoService.FindAllAsync(int.Parse(idUsuario));
            return View(list);
        }
        
        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (!ModelState.IsValid)
            {  
                return View(produto);
            }
            var idUsuario = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            produto.UsuarioId = (int.Parse(idUsuario));
            await _produtoService.InsertAsync(produto);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido"});
            }

            var obj = await _produtoService.FindByIdAsync(id.Value);
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
            await _produtoService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = await _produtoService.FindByIdAsync(id.Value);
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

            var obj = await _produtoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            
            return View(obj);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return View(produto);
            }

            if (id != produto.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não compatível" });
            }

            try
            {
                await _produtoService.UpdateAsync(produto);
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