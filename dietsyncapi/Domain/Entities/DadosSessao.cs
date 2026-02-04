namespace dietsync.Domain.Entities;

public class DadosSessao
{
    public ulong IdLogin { get; set; }

    public ulong? FkIdUserLogin { get; set; }

    public DateTime? DataLogin { get; set; }

    public virtual User FkIdUserLoginNavigation { get; set; }
}