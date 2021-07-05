using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

[ApiController]
[Route("[controller]")] //movie
public class MovieController : ControllerBase
{
   private readonly ApplicationDbContext _context;
    public MovieController(ApplicationDbContext context){
        _context = context;

    }
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieOutputGetByIdDTO>> GetById(long id)
    {
        try
        {
         var filme = await _context.Filmes.Include(filme => filme.Diretor).FirstOrDefaultAsync(filme => filme.Id == id);

        if(filme == null){
            return NotFound("Filme nao encontrado!!!");
            
        }
         var movieOutputGetByIdDTO = new MovieOutputGetByIdDTO(filme.Id,filme.Titulo,filme.Genero,filme.Ano,filme.DiretorId,filme.Diretor.Nome);
         return Ok(movieOutputGetByIdDTO);
         } catch (Exception ex)
        { 
            return Conflict(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<List<MovieOutputGetAllDTO>>> Get()
    {
        try
        {
        //return await _context.Filmes.ToListAsync();
         var movieOutputGetAllDTO = new List<MovieOutputGetAllDTO>();
         var filme = await _context.Filmes.ToListAsync();

         movieOutputGetAllDTO.AddRange(filme.Select(dir => new MovieOutputGetAllDTO(){
                Id=dir.Id,
                Titulo=dir.Titulo,
                Ano=dir.Ano,
                Genero=dir.Genero,
                DiretorId=dir.DiretorId
                
         }).ToList());

        if(movieOutputGetAllDTO.Any()){

             return movieOutputGetAllDTO;

         }
         return NotFound("Não existem diretores cadastrados!!!");
         } catch (Exception ex)
        { 
            return Conflict(ex.Message);
        }

    } 

    [HttpPost]
    public async Task <ActionResult<MovieOutputPostDTO>> Post ([FromBody] MovieInputPostDTO movieInputPostDTO){  
        try
        {

        var filme = new Filme(movieInputPostDTO.Titulo,movieInputPostDTO.Genero,movieInputPostDTO.Ano,movieInputPostDTO.DiretorId);  
        //validacao ruim se está nullo
        if (filme.Titulo == null || filme.Titulo =="")
        {
            return Conflict("Campo título é obrigatório, digite o título do filme");
        } 
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == movieInputPostDTO.DiretorId); 
        if (diretor == null) {
            return NotFound("Diretor informado não existe");
        }
        _context.Filmes.Add(filme);
        await _context.SaveChangesAsync();

        var movieOutputPostDTO = new MovieOutputPostDTO(filme.Id,filme.Titulo,filme.Genero,filme.Ano,filme.DiretorId);
        return Ok (movieOutputPostDTO);
        } catch (Exception ex)
        { 
            return Conflict(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieOutputPutDTO>> Put(int id, [FromBody] MovieInputPutDTO movieInputPutDTO)
    {
        try
        {

        var filme = new Filme(movieInputPutDTO.Titulo,movieInputPutDTO.Genero,movieInputPutDTO.Ano,movieInputPutDTO.DiretorId);
        filme.Id = id;

        _context.Filmes.Update(filme);
        await _context.SaveChangesAsync();

        var MovieOutputPutDTO = new MovieOutputPutDTO(filme.Id,filme.Titulo,filme.Genero,filme.Ano,filme.DiretorId);
        return Ok(MovieOutputPutDTO);
        } catch (Exception ex)
        { 
            return Conflict(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
    try
    {
      var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);
      _context.Remove(filme);
      await _context.SaveChangesAsync();

      return Ok(filme);
      } catch (Exception ex)
        { 
            return Conflict(ex.Message);
        }
    }
}