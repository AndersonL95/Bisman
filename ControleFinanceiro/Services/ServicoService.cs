using Bisman.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bisman.Services.Exceptions;

namespace Bisman.Services
{
    public class ServicoService
    {
        private readonly BismanContext _context;
        IHttpContextAccessor HttpContextAccessor;

        public ServicoService(BismanContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            HttpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Servico>> FindAllAsync(int id)
        {
            return await _context.Servico.Where(x => x.UsuarioId == id).OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task InsertAsync(Servico obj)
        {
            _context.Add(obj);
           await _context.SaveChangesAsync();
        }

        public async Task<Servico> FindByIdAsync(int id)
        {
            return await _context.Servico.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Servico.FindAsync(id);
            _context.Servico.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Servico obj)
        {
            bool hasAny = await _context.Servico.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task<List<Servico>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var idUsuario = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            var result = from obj in _context.Servico select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value && x.UsuarioId == int.Parse(idUsuario));
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value && x.UsuarioId == int.Parse(idUsuario));
            }
            return await result
            .OrderByDescending(x => x.Data)
            .ToListAsync();
        }
        
       
    }
}
