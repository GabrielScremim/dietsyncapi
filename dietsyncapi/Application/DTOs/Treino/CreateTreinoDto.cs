namespace dietsync.DTOs
{
    public class CreateTreinoDTO
    {
        public DateOnly Data { get; set; }

        public string Tipo { get; set; }

        public string Exercicios { get; set; }

        public int Repeticoes { get; set; }

        public int Series { get; set; }

        public string Objetivo { get; set; }

        public string Duracao { get; set; }

        public string Frequencia { get; set; }

        public string NomeTreino { get; set; }
        public char DiaTreino { get; set; }
    }
}