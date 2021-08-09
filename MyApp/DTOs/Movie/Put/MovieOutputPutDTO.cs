using System.Collections.Generic;
public class MovieOutputPutDTO {

    public int Id { get; set; }
    public string Titulo { get; set; }

    public string Ano { get; set; }

    public string Genero { get; set; }

    public long DiretorId { get; set; }

    public MovieOutputPutDTO(int id, string titulo, string ano, string genero, long diretorId) {
        Id = id;
        Titulo = titulo;
        Ano = ano;
        Genero =  genero;
        DiretorId = diretorId;
    }
}
