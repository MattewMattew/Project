using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerNetworkController : NetworkBehaviour
{
    private bool isSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (FindObjectOfType<GameManagerScript>())
            {
                transform.SetParent(GameObject.Find("Background").transform);
                if (!isSpawned)
                {
                    GameManagerScript GameManager = FindObjectOfType<GameManagerScript>(); ;
                    List<Vector2> pos = GameManager.spawnPoint;
                    if (pos != null)
                    {
                        Debug.Log(pos[0]);
                        transform.localPosition = pos[0];
                        transform.localScale = new Vector3(1, 1, 0);
                        GameManager.spawnPoint.RemoveAt(0);
                        
                        isSpawned = true;
                    }
                }
            }
        }

    }
}
