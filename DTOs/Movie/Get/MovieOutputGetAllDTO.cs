using System.Collections.Generic;

public class MovieListOutputGetAllDTO {
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public List<MovieOutputGetAllDTO> Items { get; init; }
}
public class MovieOutputGetAllDTO {
    public int Id { get; set; }
    public string Titulo { get; set; }

    public string Ano { get; set; }

    public string Genero { get; set; }

    public long DiretorId { get; set; }

    public MovieOutputGetAllDTO(int id, string titulo, string ano, string genero, long diretorId) {
        Id = id;
        Titulo = titulo;
        Ano = ano;
        Genero =  genero;
        DiretorId = diretorId;
    }
    public MovieOutputGetAllDTO()
    {
    }

}
