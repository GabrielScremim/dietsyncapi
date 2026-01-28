namespace dietsync.DTOs
{
    public class CreateReceitaDto
    {
        public string NomeReceita { get; set; }

        public string Ingredientes { get; set; }

        public string ModoPreparo { get; set; }

        public double Calorias { get; set; }

        public double Proteinas { get; set; }

        public double Carboidratos { get; set; }

        public double Gordura { get; set; }
    }
}