namespace dietsyncapi.DTOs.Evolucao
{
    public class ResponseEvolucaoDto
    {
        public ulong Id { get; set; }
        public DateTime Data { get; set; }

        public double Peso { get; set; }

        public double Altura { get; set; }

        public double Cintura { get; set; }
    }
}
