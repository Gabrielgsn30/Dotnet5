using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefa1;

namespace Tarefa1.Services
{
    public interface IDiretorService
    {
        Task<Diretor> Add(Diretor diretor);
        Task<Diretor> Delete(long id);
        Task<List<Diretor>> GetAll();
        Task<Diretor> GetById(long id);
        Task <Diretor> Update(Diretor diretor);
    }
} 