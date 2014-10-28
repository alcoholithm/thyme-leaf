using UnityEngine;
using System.Collections;

public class GameStartListener : MonoBehaviour {

    void OnClick()
    {
        transform.parent.GetComponent<LobbyView>().GameStart();
    }
}
