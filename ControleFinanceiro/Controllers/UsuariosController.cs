using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bisman.Models;
using Bisman.Services;
using Microsoft.AspNetCore.Http;
using System;

namespace Bisman.Controllers
{

    public class UsuariosController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly ProdutoService _produtoService;
        private readonly ServicoService _servicoService;

        public UsuariosController(UsuarioService usuarioService, ProdutoService produtoService, ServicoService servicoService)
        {
            _usuarioService = usuarioService;
            _produtoService = produtoService;
            _servicoService = servicoService;
        }

        [HttpGet]
        public IActionResult Login(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    HttpContext.Session.SetString("NomeUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("EmailUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("IdUsuarioLogado", string.Empty);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidarLogin(Usuario usuario)
        {
            var obj = await _usuarioService.ValidarLoginAsync(usuario);
            if (obj != null)
            {
                HttpContext.Session.SetString("NomeUsuarioLogado", obj.Nome);
                HttpContext.Session.SetString("EmailUsuarioLogado", obj.Email);
                HttpContext.Session.SetString("IdUsuarioLogado", obj.Id.ToString());
                return RedirectToAction("Menu", "Home");
            }
            else
            {
                TempData["MensagemLoginInvalido"] = "Dados de Login Inválidos!";
                return RedirectToAction("Login");
            }
        }             

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar([Bind("Id,Nome,Email,Senha")] Usuario usuario)
        {
            await _usuarioService.InsertAsync(usuario);
            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Relatorios()
        {
            return View();
        }

        public async Task<IActionResult> BuscarProdutos(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _produtoService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> BuscarServicos(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _servicoService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
    }
}
