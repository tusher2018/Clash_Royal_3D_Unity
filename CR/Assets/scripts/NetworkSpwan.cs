using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkSpwan : NetworkManager 
{

    [SerializeField] Transform Pos1;
    [SerializeField] Transform Pos2;
    GameObject ball;




    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? Pos1 : Pos2;

        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        
        // if (numPlayers == 2)
        // {
            
        // }
    }
    /*  
       public override void OnServerDisconnect(NetworkConnection conn)
       {
           // destroy ball
           if (ball != null)
               NetworkServer.Destroy(ball);

           // call base functionality (actually destroys the player)
           base.OnServerDisconnect(conn);
       }
   */

}
