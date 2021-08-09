using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tarefa1;

namespace Tarefa1.Services
{
    public interface IDiretorService
    {
        //Task<DiretorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
        Task<Diretor> Add(Diretor diretor);
        Task<Diretor> Delete(long id);
        //Task<List<Diretor>> GetAll();
        Task<Diretor> GetById(long id);
        Task <Diretor> Update(Diretor diretor, long id);
        Task<DiretorListOutputGetAllDTO> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
    }
} 