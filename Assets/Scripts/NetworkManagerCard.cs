using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;




public enum PlayerSpawnMethod { Random, RoundRobin }
public enum NetworkManagerMode { Offline, ServerOnly, ClientOnly, Host }


[DisallowMultipleComponent]
[AddComponentMenu("Network/Network Manager")]
[HelpURL("https://mirror-networking.gitbook.io/docs/components/network-manager")]

public class NetworkManagerCard : NetworkManager
{
    public GameObject PrefabCanvas;
    public TMP_Dropdown DPlayer;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position
        /*Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;*/
        conn.isReady = true;
        GameObject player = Instantiate(playerPrefab, new Vector2(0,0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);

        if(DPlayer.value == 0 && numPlayers ==2)
        {
            
            /*GiveHandCards(CurrentGame.Pack, EnemyHand);*/
            GameObject CanvasPref = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Canvas"));
            NetworkServer.Spawn(CanvasPref);
        }
        else if(DPlayer.value == 1 && numPlayers ==4)
        {
            
            /*GiveHandCards(CurrentGame.Pack, EnemyHand);*/
            GameObject CanvasPref = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Canvas"));
            NetworkServer.Spawn(CanvasPref);
        }
        else if(DPlayer.value == 2 && numPlayers ==5)
        {
            
            /*GiveHandCards(CurrentGame.Pack, EnemyHand);*/
            GameObject CanvasPref = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Canvas"));
            NetworkServer.Spawn(CanvasPref);
        }
        else if(DPlayer.value == 3 && numPlayers ==6)
        {
            
            /*GiveHandCards(CurrentGame.Pack, EnemyHand);*/
            GameObject CanvasPref = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Canvas"));
            NetworkServer.Spawn(CanvasPref);
        }
        else if(DPlayer.value == 4 && numPlayers ==7)
        {
            
            /*GiveHandCards(CurrentGame.Pack, EnemyHand);*/
            GameObject CanvasPref = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Canvas"));
            NetworkServer.Spawn(CanvasPref);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        // destroy ball
/*        if (ball != null)
            NetworkServer.Destroy(ball);*/

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);
    }
}
