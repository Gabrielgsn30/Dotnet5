using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Tarefa1;

namespace Tarefa1.Services
{
    public interface IFilmeService
    {
        Task<Filme> Add(Filme filme);
        Task<Filme> Delete(long id);
        Task<MovieListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
        Task<Filme> GetById(long id);
        Task<Diretor> GetDiretorId(long id);
        Task<Filme> Update(Filme filme);
    }
}