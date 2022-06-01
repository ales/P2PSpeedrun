using System;
using P2Pspeedrun.Models;

namespace P2Pspeedrun.Services
{
	public class AppService
	{
        public List<Message> Messages { get; set; }
        public HashSet<string> Peers { get; set; }

        public AppService()
		{
            Messages = new();
            Peers = new();

            Peers.Add("192.168.3.147:5000");
		}

		public List<Message> GetMessages()
        {
            return Messages;
        }

        public void ReceiveMessage(Message m)
        {
            Messages.Add(m);
        }

        public void HavePeer(string ip, Client c)
        {
            if (c.Port > 0)
            {
                Peers.Add($"{ip}:{c.Port}");
            }
        }

        public void Broadcast(Message m, string user)
        {
            var http = new HttpClient();

            foreach(var peer in Peers)
            {
                http.PostAsJsonAsync("http://" + peer + "/api/message/receive", new MessageProtocol()
                {
                    Message = m,
                    Client = new Client() { Id = user }
                });
            }

        }

        public void NewMessage(Message m, string user)
        {
            m.Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            m.Username = user;
            m.Id = Guid.NewGuid();

            Messages.Add(m);

            Broadcast(m, user);
        }
	}
}

