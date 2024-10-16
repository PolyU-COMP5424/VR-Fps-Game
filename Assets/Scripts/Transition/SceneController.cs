using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Data;

public class SceneController : Singleton<SceneController>
{
    public GameObject playerPrefab;
    GameObject player;
    private string sceneName;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void TransitionToDestination()
    {

        switch (GameManager.Instance.mapID)
        {
            case 0:
                sceneName = "Cartoon";
                break;
        }
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return Instantiate(playerPrefab, GetDestination().transform.position, GetDestination().transform.rotation);
        yield break;
    }

    private TransitionPoint GetDestination()
    {
        var entrances = FindObjectsOfType<TransitionPoint>();
        foreach (var entrance in entrances)
        {
            return entrance;
        }
        return null;
    }
}
