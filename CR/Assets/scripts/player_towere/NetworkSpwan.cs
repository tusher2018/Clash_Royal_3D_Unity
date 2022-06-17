using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkSpwan : NetworkManager
{

    [SerializeField] Transform Pos1;
    [SerializeField] Transform Pos2;
    [SerializeField] public GameObject[] Players;
    [SerializeField] GameObject allcards;
    [SerializeField] GameObject bluecards;
    [SerializeField] GameObject redcards;
    bool Time1st = true;

    void Update()
    {
        if (Time1st)
        {
            for (int i = 0; i < allcards.transform.childCount; i++)
            {
                this.spawnPrefabs.Add(allcards.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < bluecards.transform.childCount; i++)
            {
                this.spawnPrefabs.Add(bluecards.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < redcards.transform.childCount; i++)
            {
                this.spawnPrefabs.Add(redcards.transform.GetChild(i).gameObject);
            }
            Time1st = false;
        }
    }


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        // add player at correct spawn position
        Transform start = numPlayers == 0 ? Pos1 : Pos2;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        Players[numPlayers - 1] = player;
        if (numPlayers == 2)
        {

        }



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
