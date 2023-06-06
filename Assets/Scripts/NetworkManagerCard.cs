using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;




public enum PlayerSpawnMethod { Random, RoundRobin }
public enum NetworkManagerMode { Offline, ServerOnly, ClientOnly, Host }

[DisallowMultipleComponent]
[AddComponentMenu("Network/Network Manager")]
[HelpURL("https://mirror-networking.gitbook.io/docs/components/network-manager")]

public class NetworkManagerCard : NetworkManager
{
    public GameObject PrefabCanvas;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // add player at correct spawn position
        /*Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;*/
        conn.isReady = true;
        GameObject player = Instantiate(playerPrefab, new Vector2(0,0), Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 6)
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
