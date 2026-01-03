namespace FIAP.FCG.Transaction.Service.Dto.User;

public class UserResponseDto
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string AccessLevel { get; set; } = null!;
}
