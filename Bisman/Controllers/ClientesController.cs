using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bisman.Models;
using Bisman.Services;
using Microsoft.AspNetCore.Http;
using System;

namespace Bisman.Controllers
{

    public class ClientesController : Controller
    {

          private readonly UsuarioService _usuarioService;
        private readonly ProdutoService _produtoService;
        private readonly ServicoService _servicoService;

        public ClientesController(ClienteService ClienteService, ProdutoService produtoService, ServicoService servicoService)
        {
            _ClienteService = ClienteService;
        }


        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Relatorios()
        {
            return View();
        }


      
    }
}
