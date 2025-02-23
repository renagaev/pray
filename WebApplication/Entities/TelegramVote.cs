namespace WebApplication.Entities;

public class TelegramVote
{
    public int Id { get; init; }
    public long UserId { get; init; }
    public int TelegramPostId { get; init; }
    public TelegramPost TelegramPost { get; init; }
}