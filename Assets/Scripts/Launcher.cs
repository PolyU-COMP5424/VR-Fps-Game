using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.GroupCharlie.FPS
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields

        // 绑定 Scene 内的 UI Object
        [SerializeField]
        private GameObject progressLabel;
        [SerializeField]
        private GameObject mainPanel;
        [SerializeField]
        private GameObject settingPanel;
        [SerializeField]
        private GameObject teamPanel;

        // 房间最大玩家数
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion

        #region Private Fields

        // 当前是否正在连接
        bool isConnecting;
        // 游戏版本号
        string gameVersion = "0.1";

        #endregion

        #region MonoBehaviour CallBacks

        void Awake()
        {
            // #Critical
            // 确保我们可以使用PhotonNetwork.LoadLevel()来同步加载场景
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Start is called before the first frame update
        void Start()
        {
            // 初始化 UI
            mainPanel.SetActive(true);
            progressLabel.SetActive(false);
            settingPanel.SetActive(false);
            teamPanel.SetActive(false);
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        /// <summary>
        /// 当玩家连接到 Photon Master Server 时回调
        /// </summary>
        public override void OnConnectedToMaster()
        {
            Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");

            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }

        /// <summary>
        /// 当玩家和服务器断开连接时回调
        /// </summary>
        /// <param name="cause"></param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            mainPanel.SetActive(true);

            Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }
        
        /// <summary>
        /// 当玩家成功加入房间时回调
        /// </summary>
        public override void OnJoinedRoom()
        {
            Debug.Log("Launcher: OnJoinedRoom() was called by PUN");

            // #Critical
            // 只有在我们是第一个玩家时才加载，否则依赖于`PhotonNetwork.AutomaticallySyncScene`来同步加载
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("Now loading 'Waiting Room'");

                PhotonNetwork.LoadLevel("Waiting Room");
            }
        }

        /// <summary>
        /// 当玩家加入房间失败时回调
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Launcher: OnJoinRandomFailed() was called by PUN");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 连接到 Photon 服务器，同时检查当前连接状态：
        /// 如果正在连接中：加入一个随机房间
        /// 如果没有连接：开始连接到 Photon 服务器
        /// </summary>
        public void Connect()
        {
            Debug.Log("Launcher: Connect() was called");

            progressLabel.SetActive(true);
            mainPanel.SetActive(false);

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }

        /// <summary>
        /// 前往设置界面
        /// </summary>
        public void Go_to_Setting()
        {
            Debug.Log("Launcher: Go_to_Setting() was called");

            mainPanel.SetActive(false);
            settingPanel.SetActive(true);
            teamPanel.SetActive(false);
        }

        /// <summary>
        /// 前往团队信息界面
        /// </summary>
        public void Go_to_Team()
        {
            Debug.Log("Launcher: Go_to_Team() was called");

            mainPanel.SetActive(false);
            settingPanel.SetActive(false);
            teamPanel.SetActive(true);
        }

        /// <summary>
        /// 回到主界面
        /// </summary>
        public void Back()
        {
            Debug.Log("Launcher: Back() was called");

            mainPanel.SetActive(true);
            settingPanel.SetActive(false);
            teamPanel.SetActive(false);
        }

        /// <summary>
        /// 退出游戏
        /// </summary>
        public void Quit()
        {
            Debug.Log("Launcher: Quit() was called");

            Application.Quit();
        }

        #endregion
    }
}
