namespace Services.Identity.Models
{
    public class Password : IPassword
    {
        public byte[] Hash { get; }
        public byte[] Salt { get; }

        public Password(byte[] hash, byte[] salt)
        {
            Hash = hash;
            Salt = salt;
        }
    }
}
