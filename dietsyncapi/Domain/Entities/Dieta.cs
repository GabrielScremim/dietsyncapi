namespace dietsync.Domain.Entities;

public class Dieta
{
    public ulong IdDieta { get; set; }

    public string NomeDieta { get; set; }

    public string TipoDieta { get; set; }

    public double Calorias { get; set; }

    public double Proteinas { get; set; }

    public double Carboidratos { get; set; }

    public double Gorduras { get; set; }

    public DateTime DataDieta { get; set; }

    public string Refeicao { get; set; }

    public string Alimentos { get; set; }

    public int Quantidade { get; set; }

    public string Observacoes { get; set; }

    public ulong? FkIdUsuarioDieta { get; set; }

    public virtual User FkIdUsuarioDietaNavigation { get; set; }
}