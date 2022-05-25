using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class score : NetworkBehaviour {

	public NetworkSpwan network;
	void Start () {





	}
	

	void Update () {
		if(!isServer){return;}	
		
			      if (isServer)
        {
            network=GameObject.Find("Network").GetComponent<NetworkSpwan>();

            if (network.Players[0] != null && network.Players[1] != null)
            {
                foreach (GameObject item in network.Players)
                {

                    if (item.GetComponent<payerConorl>().score == 3)
                    {
                        // network=GameObject.Find("Network").GetComponent<NetworkSpwan>();
                        // foreach (GameObject subitem in network.Players)
                        // {
                        //     subitem.GetComponent<payerConorl>().VSBoard.SetActive(true);
                        //     subitem.GetComponent<payerConorl>().VSBoard.transform.GetChild(0).GetComponent<Text>().text =
                        //    network.Players[0].GetComponent<payerConorl>().score.ToString();
                        //     subitem.GetComponent<payerConorl>().VSBoard.transform.GetChild(1).GetComponent<Text>().text =
                        //    network.Players[1].GetComponent<payerConorl>().score.ToString();

                        //     if (subitem.GetComponent<payerConorl>().netId == network.Players[0].GetComponent<payerConorl>().netId)
                        //     {

                        //     }
                        //     if (subitem.GetComponent<payerConorl>().netId == network.Players[1].GetComponent<payerConorl>().netId)
                        //     {

                        //     }

                        // }
 network.Players[0].GetComponent<payerConorl>().VSBoard.SetActive(true);
 network.Players[0].GetComponent<payerConorl>().VSBoard.transform.GetChild(0).GetComponent<Text>().text= network.Players[0].GetComponent<payerConorl>().score.ToString();
 network.Players[0].GetComponent<payerConorl>().VSBoard.transform.GetChild(1).GetComponent<Text>().text= network.Players[1].GetComponent<payerConorl>().score.ToString();

 network.Players[1].GetComponent<payerConorl>().VSBoard.SetActive(true);
 network.Players[1].GetComponent<payerConorl>().VSBoard.transform.GetChild(0).GetComponent<Text>().text= network.Players[0].GetComponent<payerConorl>().score.ToString();
 network.Players[1].GetComponent<payerConorl>().VSBoard.transform.GetChild(1).GetComponent<Text>().text= network.Players[1].GetComponent<payerConorl>().score.ToString();

                    }

                }
            }
        }
	}
}
