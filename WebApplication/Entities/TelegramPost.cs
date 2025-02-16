namespace WebApplication.Entities;

public class TelegramPost
{
    public int Id { get; init; }
    public int MessageId { get; init; }
    public long ChatId { get; init; }
    public Post Post { get; init; }
    public int PostId { get; set; }
}