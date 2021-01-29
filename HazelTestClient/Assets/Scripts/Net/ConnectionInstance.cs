using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hazel;
using Hazel.Udp;
using System.Net;
using System;
using HazelCommon;

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
            Connection = new UdpClientConnection(new IPEndPoint(IPAddress.Loopback, 1234));
            Connection.Disconnected += (c, e) =>
            {
                if (OnDisconnect != null)
                {
                    OnDisconnect(e.Reason);
                }
            };
            MessageWriter Writer = new MessageWriter(4);
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
