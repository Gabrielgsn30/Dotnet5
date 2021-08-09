using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tarefa1;

namespace Tarefa1.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly ApplicationDbContext _context;
        public FilmeService(ApplicationDbContext context)
        {
            _context = context;
        }

   public async Task<MovieListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken) {
        var pagedModel = await _context.Filmes
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .PaginateAsync(page, limit, cancellationToken);

        if (!pagedModel.Items.Any()) {
            throw new Exception("Não existem filmes cadastrados!");
        }

        return new MovieListOutputGetAllDTO {
            CurrentPage = pagedModel.CurrentPage,
            TotalPages = pagedModel.TotalPages,
            TotalItems = pagedModel.TotalItems,
            Items = pagedModel.Items.Select(filme => new MovieOutputGetAllDTO(filme.Id, filme.Titulo,filme.Ano,filme.Genero,filme.DiretorId)).ToList()
        };
    }

        public async Task<Filme> GetById(long id)
        {
            var filme = await _context.Filmes.Include(filme => filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);
            return filme;
        }

        public async Task<Diretor> GetDiretorId(long id)
        {
            var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
            return diretor;
        }

        public async Task<Filme> Add(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

        public async Task<Filme> Update(Filme filme)
        {
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
            return filme;
        }

         public async Task<Filme> Delete(long id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
            _context.Remove(filme);
            await _context.SaveChangesAsync();
            return filme;
        }
    }
}