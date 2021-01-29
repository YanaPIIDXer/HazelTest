using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hazel;
using Hazel.Udp;
using System.Net;
using System;
using HazelCommon;
using HazelCommon.HazelExt;
using HazelCommon.Packet;

namespace Net
{
    /// <summary>
    /// 接続インスタンス
    /// </summary>
    public class ConnectionInstance : MonoBehaviour
    {
        /// <summary>
        /// Prefabのパス
        /// </summary>
        private static readonly string PrefabPath = "Prefabs/ConnectionInstance";

        /// <summary>
        /// 接続
        /// </summary>
        private UdpClientConnection Connection = null;

        /// <summary>
        /// 接続されているか？
        /// </summary>
        public bool IsConnected { get { return (Connection != null && Connection.State != ConnectionState.Connected); } }

        /// <summary>
        /// 接続が閉じられているか？
        /// </summary>
        public bool IsClosing { get { return (Connection != null && Connection.State != ConnectionState.NotConnected); } }

        /// <summary>
        /// 切断された
        /// </summary>
        public Action<string> OnDisconnect { set; private get; }

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns>生成されたインスタンス</returns>
        public static ConnectionInstance Generate()
        {
            var Prefab = Resources.Load(PrefabPath);
            Debug.Assert(Prefab != null);
            var Instance = Instantiate(Prefab) as GameObject;
            Debug.Assert(Instance != null);
            var ConnInst = Instance.GetComponent<ConnectionInstance>();
            return ConnInst;
        }

        /// <summary>
        /// サーバに接続
        /// </summary>
        public void ConnectToServer(Action OnSuccess, Action OnFail)
        {
            StartCoroutine(Connect(OnSuccess, OnFail));
        }

        /// <summary>
        /// 接続用コルーチン
        /// </summary>
        private IEnumerator Connect(Action OnSuccess, Action OnFail)
        {
            if (Connection != null) { yield break; }

            Connection = new UdpClientConnection(new IPEndPoint(IPAddress.Loopback, CommonConsts.Port));
            Connection.DataReceived += (e) =>
            {
                Serializer Sr = new Serializer(e.Message);
                ushort PacketID = 0;
                Sr.Serialize(ref PacketID);
                Debug.Log("PacketID:" + PacketID);
                switch (PacketID)
                {
                    case 1:

                        PacketTest Packet = new PacketTest();
                        Packet.Serialize(Sr);
                        Debug.Log("int:" + Packet.IntData);
                        Debug.Log("short:" + Packet.ShortData);
                        Debug.Log("float:" + Packet.FloatData);
                        Debug.Log("string:" + Packet.StringData);
                        break;
                }
            };
            Connection.Disconnected += (c, e) =>
            {
                if (OnDisconnect != null)
                {
                    OnDisconnect(e.Reason);
                }
            };
            
            MessageWriter Writer = new MessageWriter(8);
            Writer.Write(CommonConsts.HandshakeCode);
            Connection.ConnectAsync(Writer.Buffer);

            while (!IsConnected && !IsClosing)
            {
                yield return null;
            }
            
            if (IsConnected)
            {
                if (OnSuccess != null)
                {
                    OnSuccess();
                }
            }
            else
            {
                if (OnFail != null)
                {
                    OnFail();
                }
            }
        }

        void Awake()
        {
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
