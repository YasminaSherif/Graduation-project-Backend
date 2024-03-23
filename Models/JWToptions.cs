namespace Graduation_project.Models
{
    public class JWToptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }
        public string SigningKey { get; set; }

    }
}
