namespace dietsync.Domain.Entities;

public class Treino
{
    public ulong Id { get; set; }

    public DateOnly Data { get; set; }

    public string Tipo { get; set; }

    public string Exercicios { get; set; }

    public int Repeticoes { get; set; }

    public int Series { get; set; }

    public string Objetivo { get; set; }

    public string Duracao { get; set; }

    public string Frequencia { get; set; }

    public string NomeTreino { get; set; }

    public ulong? FkIdUsuarioTreino { get; set; }

    public char DiaTreino { get; set; }

    public virtual User FkIdUsuarioTreinoNavigation { get; set; }
}