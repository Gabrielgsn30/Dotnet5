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

    public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
    {

        var diretor = await _context.Diretores.FirstOrDefaultAsync(diretor => diretor.Id == id);

        if(diretor == null){
            return NotFound("Diretor nao encontrado!!!");
            
        }
        var diretorOutputGetByIdDTO = new DiretorOutputGetByIdDTO(diretor.Id,diretor.Nome);
        // poode passar por objetos entre diretorOutputGetByIdDTO recebe o diretor . id ou nome em vez de passar por parametro no new no construtor
        return Ok(diretorOutputGetByIdDTO);
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
    public async Task<ActionResult<List<DiretorOutputGetAllDTO>>> Get(){

        //https://forums.asp.net/t/2161265.aspx?How+can+I+implement+GetAll+Delete+Update+repository+from+Generic+Repository+into+repository
        //jeito de implementar getall com DTO usando lista e selecionado os campos

         var diretorOutputGetAllDTO = new List<DiretorOutputGetAllDTO>();
         var diretor = await _context.Diretores.ToListAsync();

         diretorOutputGetAllDTO.AddRange(diretor.Select(dir => new DiretorOutputGetAllDTO(){
                Id=dir.Id,
                Nome=dir.Nome
         }).ToList());

         if(diretorOutputGetAllDTO.Any()){

             return diretorOutputGetAllDTO;

         }
         return NotFound("Não existem diretores cadastrados!!!");


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
    /// <param name="diretorInputPostDTO">Nome do diretor</param>
    /// <returns>O diretor criado</returns>
    /// <response code="200">Diretor foi criado com sucesso</response>


    //Dando post para cadastrar um diretor
    [HttpPost]
    public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDTO) {
        //DTO faz a tranasferencia de dados entre o DTO para o objeto diretor(nesse caso o nome)
        //Vai pedir somente o nome para cadastro que realmente é o que deve ser digitado e nao os outros campos
        //variavel diretor vai instanciar um novo objetoInput
        var diretor = new Diretor(diretorInputPostDTO.Nome);
        //Validacao para ver se o campo nome está preenchido está preenchido
        /*
        if (diretor.Nome  == null || diretor.Nome =="")
        {
                return Conflict("Campo nome é obrigatório, digite o nome");
        }
        */
        _context.Diretores.Add(diretor);
        await _context.SaveChangesAsync();

        var diretorOutputPostDTO = new DiretorOutputPostDTO(diretor.Id,diretor.Nome);
        return Ok(diretorOutputPostDTO);

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
        /// <param name="diretorInputPutDTO">Id do diretor</param>
        /// <returns>Registro do diretor informado como parâmetro</returns>
        /// <response code="200">Diretor localizado sucesso</response>

    
    //Dando put passando um id de referencia para atualizar alguma informação do diretor
    [HttpPut("{id}")]
    public async Task<ActionResult<DiretorOutputPutDTO>> Put(int id, [FromBody] DiretorInputPutDTO diretorInputPutDTO)
    {
        //variavel diretor vai instanciar um novo objetoInput
        var diretor = new Diretor(diretorInputPutDTO.Nome);

        diretor.Id = id;

        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        var DiretorOutPutPutDTO = new DiretorOutputPutDTO(diretor.Id,diretor.Nome);
        return Ok(DiretorOutPutPutDTO); 

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
    public ActionResult Delete(long id)
    {

        var diretor = _context.Diretores.FirstOrDefault(diretor => diretor.Id == id);
        _context.Remove(diretor);
        _context.SaveChangesAsync();
        return Ok();

    }
}