using TEALINK.Models.Utilities;

namespace TEALINK.Repositories.Utilities
{
    public interface IMessageRepository
    {
        public Message GetMessage(string code);
    }
}
