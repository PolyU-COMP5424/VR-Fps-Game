using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace Com.GroupCharlie.FPS
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        public static GameManager Instance;

        #endregion

        #region MonoBehaviour CallBacks

        void Start()
        {
            Instance = this;
        }

        #endregion

        #region Photon Callbacks

        /// <summary>
        /// 当本机玩家离开房间时调用，加载 Launcher 场景
        /// </summary>
        public override void OnLeftRoom()
        {
            Debug.Log("GameManager: OnLeftRoom() was called");

            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// 当其他玩家进入房间时调用。加载游戏场景以适应游戏人数。
        /// </summary>
        public override void OnPlayerEnteredRoom(Player other)
        {
            // Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                // LoadArena();
            }
        }

        /// <summary>
        /// 当其他玩家离开房间时调用。加载游戏场景以适应游戏人数。
        /// </summary>
        public override void OnPlayerLeftRoom(Player other)
        {
            // Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                // LoadArena();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 离开房间
        /// </summary>
        public void LeaveRoom()
        {
            Debug.Log("GameManager: LeaveRoom() was called");

            PhotonNetwork.LeaveRoom();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 只有主机可以加载游戏场景
        /// </summary>
        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
                return;
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Waiting Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }

        #endregion

    }
}