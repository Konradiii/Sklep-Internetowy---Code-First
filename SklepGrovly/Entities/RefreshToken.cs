namespace SklepGrovly.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string TokenHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public int Id_Osoba { get; set; }
    public Osoba Osoba { get; set; } = null!;

}