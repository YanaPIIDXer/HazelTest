using System;
using System.Net;
using Hazel;
using Hazel.Udp;
using HazelCommon;
using HazelCommon.HazelExt;
using HazelCommon.Packet;

namespace HazelTestServer.Server
{
    /// <summary>
    /// サーバのコア部分
    /// </summary>
    public class ServerCore
    {
        /// <summary>
        /// ポート番号
        /// </summary>
        private int Port = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Port">ポート番号</param>
        public ServerCore(int Port)
        {
            this.Port = Port;
        }

        /// <summary>
        /// 起動
        /// </summary>
        public void Run()
        {
            using (var UdpServer = new UdpConnectionListener(new IPEndPoint(IPAddress.Any, Port)))
            {
                UdpServer.NewConnection += OnConnected;
                UdpServer.Start();
                while (true)
                {
                }
            }
        }

        /// <summary>
        /// コールバック：誰かが接続してきた
        /// </summary>
        /// <param name="e">イベント引数</param>
        private void OnConnected(NewConnectionEventArgs e)
        {
            if (e.HandshakeData.Length <= 0)
            {
                // ハンドシェイクコード以前にそもそもデータがないのでハンドシェイクコード不一致扱いで弾く
                DisconnectHandshakeError(e.Connection);
                return;
            }
            int Code = e.HandshakeData.ReadInt32();
            if (Code != CommonConsts.HandshakeCode)
            {
                // ハンドシェイクコードの不一致。弾く
                DisconnectHandshakeError(e.Connection);
                return;
            }
            Console.WriteLine("Connected.");
            
            // ↓newしたら駄目っぽい
            //MessageWriter Writer = new MessageWriter(4);
            // ↓インスタンス取得用staticメソッドがあるのでそれを使う
            MessageWriter Writer = MessageWriter.Get();
            Serializer Sr = new Serializer(Writer);
            PacketTest Packet = new PacketTest(30000, 200, 100.5f, "Hello World!!");
            Packet.Serialize(Sr);
            e.Connection.Send(Writer);
            Writer.Recycle();
        }

        /// <summary>
        /// ハンドシェイクコードの不一致による切断
        /// </summary>
        private void DisconnectHandshakeError(Connection Conn)
        {
            Conn.Disconnect("HandhskeCode Error");
            Console.WriteLine("Disconnect. Reason: HandshakeCode Error.");
        }
    }
}
