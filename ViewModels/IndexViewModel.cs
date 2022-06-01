using System;
using P2Pspeedrun.Models;

namespace P2Pspeedrun.ViewModels
{
	public class IndexViewModel
	{
		public List<Message> Messages { get; set; }
		public HashSet<string> Peers { get; set; }


	}
}

