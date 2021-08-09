public class Filme {
    public Filme(string titulo,string ano, string genero,long diretorId) {
        Titulo = titulo;
        DiretorId = diretorId;
        Ano = ano;
        Genero = genero;

    }
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Ano { get; set; }
    public string Genero { get; set; }
    public long DiretorId { get; set; }
    public Diretor Diretor { get; set; }
}