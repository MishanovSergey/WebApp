using System.Collections.Concurrent;

namespace WebApp.Services
{
    public class MessageStorageService
    {
        public ConcurrentQueue<string> Messages { get; } = new ConcurrentQueue<string>();
    }
}
