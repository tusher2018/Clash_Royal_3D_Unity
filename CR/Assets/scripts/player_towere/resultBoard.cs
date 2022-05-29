using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class resultBoard : NetworkBehaviour {

[SyncVar] public string p1s;
[SyncVar] public string p2s;
[SyncVar] public string p1n;
[SyncVar] public string p2n;


	void Start () {
	}
	

	void Update () {
		transform.GetChild(0).GetComponent<Text>().text=p1s;
		transform.GetChild(1).GetComponent<Text>().text=p2s;
		transform.GetChild(2).GetComponent<Text>().text=p1n;
		transform.GetChild(3).GetComponent<Text>().text=p2n;

	}
}
