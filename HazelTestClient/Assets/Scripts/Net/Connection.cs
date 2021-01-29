using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Net
{
    public class Connection
    {
        /// <summary>
        /// 接続インスタンス
        /// </summary>
        private ConnectionInstance ConnInst = null;

        /// <summary>
        /// 切断イベント
        /// </summary>
        public Action<string> OnDisconnect
        {
            set
            {
                if (ConnInst == null)
                {
                    OnDisconnectPool = value;
                    return;
                }
                ConnInst.OnDisconnect = value;
            }
        }

        /// <summary>
        /// OnDisconnectイベント
        /// Connectより前に設定された時の為の領域
        /// </summary>
        private Action<string> OnDisconnectPool = null;

        /// <summary>
        /// サーバに接続
        /// </summary>
        public void Connect()
        {
            if (ConnInst != null) { return; }
            ConnInst = ConnectionInstance.Generate();
            if (OnDisconnectPool != null)
            {
                ConnInst.OnDisconnect = OnDisconnectPool;
            }
            ConnInst.ConnectToServer(() =>
            {
                Debug.Log("Connection Success!!");
            }, () =>
            {
                Debug.Log("Connection Failed...");
            });
        }
        
        /// <summary>
        /// ConnectionInstanceの破棄
        /// </summary>
        private void DestroyConnectionInstnace()
        {
            GameObject.Destroy(ConnInst.gameObject);
            ConnInst = null;    
        }

#region Singleton
        public static Connection Instance { get { return _Instance; } }
        private static Connection _Instance = new Connection();
        private Connection() {}
#endregion
    }
}
