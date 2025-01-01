namespace Ganz.Domain.Enttiies.Identity
{
    public class User
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? PasswordSalt { get; set; }
        public DateTime Registerdate { get; set; }
        public DateTime LastLogindate { get; set; }

    }
}
