using System;
namespace P2Pspeedrun.Models
{
	public class Message
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Text { get; set; }
		public long Timestamp { get; set; }

	}
}

