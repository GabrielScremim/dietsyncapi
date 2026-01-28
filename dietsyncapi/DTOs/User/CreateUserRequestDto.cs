namespace dietsync.DTOs
{
    public class CreateUserRequestDTO
    {
        public string Name { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Sexo { get; set; }
        public DateOnly DataNasc { get; set; }

        public double Peso { get; set; }
        public double Altura { get; set; }

        public string Meta { get; set; }
    }
}