using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkController : NetworkBehaviour
{
    private List<Vector2> spawnPoint = new List<Vector2>() { new Vector2(0, -237), new Vector2(-696, -77), new Vector2(0, 421) };
    private bool isSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void setPlayerPosition()
    {
        transform.SetParent(GameObject.Find("Background").transform);
        transform.localScale = new Vector3(1,1,1);
        if (isLocalPlayer)
        {
            transform.localPosition = spawnPoint[0];
        }
        else
        {
            transform.localPosition = spawnPoint[1];
            spawnPoint.RemoveAt(1);
        }
    }
}
