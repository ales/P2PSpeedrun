using System;
using P2Pspeedrun.Models;

namespace P2Pspeedrun.Services
{
	public class AppService
	{
        public List<Message> Messages { get; set; }

        // HashSet is super cool! Nothing can be there twice, so if you Add() something that's already there, nothing happens.
        // Very handy data structure!
        public HashSet<string> Peers { get; set; }

        public AppService()
        {
            Messages = new(); // we don't have to specify the class name for new() because it's already given in the prop definition
            Peers = new(); // so – this shorhand!

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
            // adding only if port number is provided. Without port, no fun.
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
                // ❤️
                http.PostAsJsonAsync("http://" + peer + "/api/message/receive", new MessageProtocol()
                {
                    Message = m,
                    Client = new Client() { Id = user }
                });
            }

        }

        public void NewMessage(Message m, string user)
        {
            // back and forth, spacetime in one long. Unix time!
            m.Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            m.Username = user;
            m.Id = Guid.NewGuid();

            Messages.Add(m);

            // Broadcasting only my own messages to avoid infinite cycles.
            Broadcast(m, user);
        }
	}
}

