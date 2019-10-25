using Bisman.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bisman.Services
{
    public class UsuarioService
    {
        private readonly BismanContext _context;

        public UsuarioService(BismanContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Usuario obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ValidarLoginAsync(Usuario obj)
        {
            return await _context.Usuario.Where(x => x.Email == obj.Email && x.Senha == obj.Senha).SingleOrDefaultAsync();
        }

    }
}
