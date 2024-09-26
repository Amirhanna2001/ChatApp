using RealTimeChatApp.Models;
using System.Collections.Concurrent;

namespace RealTimeChatApp.DataServices;

public class SharedDb
{
    public readonly ConcurrentDictionary<string, UserConnection> _connections = new();

    public SharedDb(ConcurrentDictionary<string, UserConnection> connections) => _connections = connections;

    public UserConnection this[string connectionId]
    {
        get => _connections.ContainsKey(connectionId) ? _connections[connectionId] : null;
        set => _connections[connectionId] = value;
    }
}
