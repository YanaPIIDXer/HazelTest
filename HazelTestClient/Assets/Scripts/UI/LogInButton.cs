using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Net;

namespace UI
{
    /// <summary>
    /// ログインボタン
    /// </summary>
    public class LogInButton : MonoBehaviour
    {
        /// <summary>
        /// 押された
        /// </summary>
        public void OnPressed()
        {
            Connection.Instance.OnDisconnect = (Reason) => Debug.Log("Disconnected. Reason:" + Reason);
            Connection.Instance.Connect();
        }
    }
}
