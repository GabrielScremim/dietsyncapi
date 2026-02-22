namespace dietsync.Domain.Entities;

public class Evolucao
{
    public ulong Id { get; set; }

    public DateTime Data { get; set; }

    public double Peso { get; set; }

    public double Altura { get; set; }

    public double Cintura { get; set; }

    public ulong? FkIdEvolucaos { get; set; }

    // public long FkIdUsuario { get; set; }

    public virtual User FkIdEvolucaosNavigation { get; set; }
}