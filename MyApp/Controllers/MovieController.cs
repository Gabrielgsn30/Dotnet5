using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Tarefa1.Services;
using System.Threading;

[ApiController]
[Route("[controller]")] //movie
public class MovieController : ControllerBase
{
   private readonly IFilmeService _filmeService;
    public MovieController(IFilmeService filmeService){
        _filmeService = filmeService;

    }

        /// <summary>
        /// O método Get retorna um registro do filme de acordo com o parâmetro id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/filme/id
        ///     {
        ///        "id": 2,
        ///        "titulo": "Et O Extraterrestre",
        ///        "nomeDoDiretor": "Steven Spielberg"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do filme</param>
        /// <returns>Registro do filme informado como parâmetro</returns>
        /// <response code="200">Filme localizado sucesso</response>


    [HttpGet("{id}")]
    public async Task<ActionResult<MovieOutputGetByIdDTO>> GetById(long id)
    {
         var filme = await _filmeService.GetById(id);

         var movieOutputGetByIdDTO = new MovieOutputGetByIdDTO(filme.Id,filme.Titulo,filme.Genero,filme.Ano,filme.DiretorId,filme.Diretor.Nome);
         return Ok(movieOutputGetByIdDTO);
    }


        /// <summary>
        /// O método Get retorna uma lista de todos os filmes do banco.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/filme
        ///     {
        ///        "id": 1,
        ///        "titulo": "A Era do Gelo",
        ///        "ano": null
        ///     },
        ///     {
        ///        "id": 3,
        ///        "titulo": "Eu sou a Lenda",
        ///        "ano": null
        ///     } 
        ///       
        /// </remarks>
        /// <returns>Todos os filmes já cadastrados no banco</returns>
        /// <response code="200">Filmes listados com sucesso</response>
    
    [HttpGet]
    public async Task<ActionResult<MovieListOutputGetAllDTO>> Get(CancellationToken cancellationToken, int limit = 5, int page = 1) {
        return await _filmeService.GetByPageAsync(limit, page, cancellationToken);        
    }


        /// <summary>
        /// O método Post registra um filme no banco de acordo com o nome informado e id do diretor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/filme
        ///     {
        ///        "titulo": "It",
        ///        "diretorId": 3
        ///     } 
        ///       
        /// </remarks>
        /// <param name="movieInputPostDTO">Titulo do filme e id do diretor</param>
        /// <returns>O filme cadastrado no banco</returns>
        /// <response code="200">Filme criado com sucesso</response>


    [HttpPost]
    public async Task <ActionResult<MovieOutputPostDTO>> Post ([FromBody] MovieInputPostDTO movieInputPostDTO){  

        var filme = await _filmeService.Add(new Filme(movieInputPostDTO.Titulo,movieInputPostDTO.Genero,movieInputPostDTO.Ano,movieInputPostDTO.DiretorId));  
        var movieOutputPostDTO = new MovieOutputPostDTO(filme.Id,filme.Titulo,filme.Genero,filme.Ano,filme.DiretorId);
        return Ok (movieOutputPostDTO);
    }


        /// <summary>
        /// O método Put atualiza o id do filme, titulo e id do diretor no banco de acordo com o id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/filme/id
        ///     {
        ///        "id": 2,
        ///        "titulo": "Titanic"
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do filme</param>
        /// <param name="movieInputPutDTO">Titulo do filme</param>
        /// <returns>O filme atualizado no banco</returns>
        /// <response code="200">Filme atualizado com sucesso</response>


    [HttpPut("{id}")]
    public async Task<ActionResult<MovieOutputPutDTO>> Put(int id, [FromBody] MovieInputPutDTO movieInputPutDTO)
    {
        var filme = new Filme(movieInputPutDTO.Titulo,movieInputPutDTO.Genero,movieInputPutDTO.Ano,movieInputPutDTO.DiretorId);
        filme.Id = id;

        await _filmeService.Update(filme);

        var MovieOutputPutDTO = new MovieOutputPutDTO(filme.Id,filme.Titulo,filme.Genero,filme.Ano,filme.DiretorId);
        return Ok(MovieOutputPutDTO);
    }


        /// <summary>
        /// O método Delete remove um filme no banco de acordo com o id informado.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/filme/id
        ///     {
        ///        "id": 2,
        ///        "titulo": "Eu sou a Lenda",
        ///        "ano": null,
        ///        "genero": null,
        ///        "diretorId": 1,
        ///        "diretor": null
        ///     } 
        ///       
        /// </remarks>
        /// <param name="id">Id do filme</param>
        /// <returns>O filme excluido</returns>
        /// <response code="200">Filme removido com sucesso</response>

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
    var filme = await _filmeService.Delete(id);

      return Ok(filme);
    }
}