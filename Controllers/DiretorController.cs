using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

[ApiController]
[Route("[controller]")]
public class DiretorController : ControllerBase {
    private readonly ApplicationDbContext _context;
    public DiretorController(ApplicationDbContext context){
        _context = context;

    }
    //Dando get passando referência do ID para trazer um diretor em específico
    [HttpGet("{id}")]
    public async Task<ActionResult<Diretor>> Get(long id)
    {
        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);
        return Ok(diretor);

    }
    //Dando get para trazer todos os diretores
    [HttpGet]
    public async Task<List<Diretor>> Get(){
        return await _context.Diretores.ToListAsync();
       }

    //Dando post para cadastrar um diretor
    [HttpPost]
    public async Task<ActionResult<Diretor>> Post([FromBody] Diretor diretor) {
        //Validacao para ver se o campo nome está preenchido está preenchido
        if (diretor.Nome  == null || diretor.Nome =="")
        {
                return Conflict("Campo nome é obrigatório, digite o nome");
        }
        _context.Diretores.Add(diretor);
        await _context.SaveChangesAsync();

        return Ok(diretor);
        }
    
    //Dando put passando um id de referencia para atualizar alguma informação do diretor
    [HttpPut("{id}")]
    public async Task<ActionResult<Diretor>> Put(int id, [FromBody] Diretor diretor)
    {
        diretor.Id = id;
        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        return Ok(diretor); 
    } 

    //Dando delete passando um id em específico para deletar um diretor
    [HttpDelete("{id}")]
    public ActionResult Delete(long id)
    {
        var diretor = _context.Diretores.FirstOrDefault(diretor => diretor.Id == id);
        _context.Remove(diretor);
        _context.SaveChangesAsync();
        return Ok();
    }
}