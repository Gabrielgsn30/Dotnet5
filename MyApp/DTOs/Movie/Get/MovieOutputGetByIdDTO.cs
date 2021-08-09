using System.Collections.Generic;
public class MovieOutputGetByIdDTO {
    public long Id { get; set; }
    public string Titulo { get; set; }

    public string Ano { get; set; }

    public string Genero { get; set; }

    public long DiretorId { get; set; }

    public string NomeDiretor{get;set;}

    public MovieOutputGetByIdDTO(long id,string titulo, string ano, string genero, long diretorid,string nomediretor) {
        Id = id;
        Titulo = titulo;
        Ano = ano;
        Genero = genero;
        DiretorId = diretorid;
        NomeDiretor = nomediretor;

    }
}
