using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class PlayerNetworkController : NetworkBehaviour
{
    
private bool isSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void setPlayerPosition(Vector2 pos)
    {
        transform.SetParent(GameObject.Find("Background").transform);
        transform.localScale = new Vector3(1,1,1);
        transform.localPosition = pos;
    }
}
