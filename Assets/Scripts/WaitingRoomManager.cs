using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Com.GroupCharlie.FPS
{
    public class WaitingRoomManager : MonoBehaviour
    {
        #region Public Fields

        public GameObject playerPrefab;

        #endregion

        #region MonoBehaviour CallBacks

        // Start is called before the first frame update
        void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManager.GetActiveScene().name);
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                // 我们在一个房间里。为本地玩家生成一个角色。通过使用 PhotonNetwork.Instantiate 进行同步
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}
