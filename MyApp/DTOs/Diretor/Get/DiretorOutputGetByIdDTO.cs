using System.Collections.Generic;
public class DiretorOutputGetByIdDTO {
    public long Id { get; set; }
    public string Nome { get; set; }

    public DiretorOutputGetByIdDTO(long id, string nome) {
        Id = id;
        Nome = nome;
    }
}
