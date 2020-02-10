namespace Services.Identity.Models
{
    public interface IPassword
    {
        byte[] Hash { get; }
        byte[] Salt { get; }
    }
}
