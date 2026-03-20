using Data.Enums;
using Data.Models;
using Data.Stores;

namespace Data.Managers;

public class ChatMessageManager
{
    private readonly ChatMessageStore _store;

    public ChatMessageManager(ChatMessageStore store)
    {
        _store = store;
    }

    public async Task<List<ChatMessage>> GetMatchMessagesAsync(int matchId) =>
        await _store.GetByMatchAsync(matchId);

    public async Task SendMessageAsync(ChatMessage model)
    {
        model.SentAt = DateTime.UtcNow;
        model.CreatedAt = DateTime.UtcNow;
        model.MessageType = MessageTypeEnum.Text;
        await _store.AddAsync(model);
        await _store.SaveAsync();
    }
}
