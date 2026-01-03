namespace FIAP.FCG.Transaction.Service.Dto.Game;

public class GameResponseDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public long Code { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public int PurchaseCount { get; set; }
    public float AverageRating { get; set; }
    public string Genre { get; set; } = null!;
}
