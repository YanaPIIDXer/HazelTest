using System;
using HazelTestServer.Server;

namespace HazelTestServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Start HazelTestServer...");
			ServerCore Core = new ServerCore(1234);
			Core.Run();
		}
	}
}
