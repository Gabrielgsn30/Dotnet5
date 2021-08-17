using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tarefa1.Services;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DiretorController : ControllerBase {
    private readonly IDiretorService _diretorService;
    public DiretorController(IDiretorService DiretorService){
        _diretorService = DiretorService;

    }

        /// <summary>
        /// O método Get retorna um registro do diretor de acordo com o parâmetro id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/diretor/id
        ///     {
        ///        "id": 1,
        ///        "nome": "Deltoro"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <returns>Registro do diretor informado como parâmetro</returns>
        /// <response code="200">Diretor localizado sucesso</response>

    //Dando get passando referência do ID para trazer um diretor em específico
    [HttpGet("{id}")]
    public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id) {
        var diretor = await _diretorService.GetById(id);

        var outputDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);
        return Ok(outputDto);
     }
        /// <summary>
        /// O método Get retorna uma lista de todos os diretores do banco.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/diretor
        ///     {
        ///        "id": 1,
        ///        "nome": "James Cameron"
        ///     },
        ///       
        /// </remarks>
        /// <returns>Todos os diretores já cadastrados no banco</returns>
        /// <response code="200">Diretores listados com sucesso</response>

    //Dando get para trazer todos os diretores
    [HttpGet]
    public async Task<ActionResult<DiretorListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1) {
        return await _diretorService.GetByPageAsync(limit, page, cancellationToken);
    }

    /// <summary>
    /// Cria um diretor
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /diretor
    ///     {
    ///        "nome": "Steven",
    ///     }
    ///
    /// </remarks>
    /// <param name="diretorInputPostDto">Nome do diretor</param>
    /// <returns>O diretor criado</returns>
    /// <response code="200">Diretor foi criado com sucesso</response>


    //Dando post para cadastrar um diretor
    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDto) {
        var diretor = await _diretorService.Add(new Diretor(diretorInputPostDto.Nome));

        var diretorOutputPostDto = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputPostDto);
    }
    
        /// <summary>
        /// O método Get retorna um registro do diretor de acordo com o parâmetro id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/diretor/id
        ///     {
        ///        "id": 2,
        ///        "nome": "Speilberg"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <param name="diretorInputPutDto">Id do diretor</param>
        /// <returns>Registro do diretor informado como parâmetro</returns>
        /// <response code="200">Diretor localizado sucesso</response>

    
    //Dando put passando um id de referencia para atualizar alguma informação do diretor
    [HttpPut("{id}")]
    public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDto) {
        var diretor = await _diretorService.Update(new Diretor(diretorInputPutDto.Nome), id);

        var diretorOutputDto = new DiretorOutputPutDTO(diretor.Id, diretor.Nome);
        return Ok(diretorOutputDto);
    }

        /// <summary>
        /// O método Delete remove um diretor no banco de acordo com o id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/diretor/id
        ///     {
        ///        "id": 1,
        ///        "nome": "Gabriel",
        ///        "filmes": []
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <returns>O diretor excluido</returns>
        /// <response code="200">Diretor removido com sucesso</response>



    //Dando delete passando um id em específico para deletar um diretor
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id) {
        await _diretorService.Delete(id);
        return Ok();
    }
}