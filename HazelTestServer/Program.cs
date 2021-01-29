using System;
using HazelTestServer.Server;
using HazelCommon;

namespace HazelTestServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Start HazelTestServer...");
			ServerCore Core = new ServerCore(CommonConsts.Port);
			Core.Run();
		}
	}
}
