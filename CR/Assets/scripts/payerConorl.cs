using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class payerConorl : NetworkBehaviour
{
    [SerializeField] GameObject[] AllPrefeb;

    [SerializeField] GameObject TowerPrefebs;
    [SerializeField] GameObject LeftTowerPos;
    [SerializeField] GameObject RightTowerPos;
    GameObject Lefttower;
    GameObject Righttower;



    Ray ray;
    GameObject myTroop;
    bool exitroad = true;
    GameObject road = null;
    [SerializeField] GameObject Troop;
    RaycastHit hit;
    [SyncVar] public bool BlueTeam = false;
    bool calledtower = false;


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        tag = "PlayerBase";
        name = "PlayerBase";
        // transform.GetChild(0).tag = "PlayerBase";
        // transform.GetChild(1).tag = "PlayerBase";
        // transform.GetChild(2).tag = "PlayerBase";
        // if(transform.position==GameObject.Find("Pos 1").transform.position){BlueTeam=true;};



    }



    void Start()
    {

    }

    void Update()
    {
        
        if (!isLocalPlayer) return;
        
        if (transform.position == GameObject.Find("Pos 1").transform.position) { BlueTeam = true; };


        if (!calledtower)
        {
            transform.GetComponent<HealthControler>().BlueTeam=this.BlueTeam;
            CmdTowerCreat(this.BlueTeam); calledtower = true;
            if (Lefttower != null) Lefttower.tag = "Base";
            if (Righttower != null) Righttower.tag = "Base";
        }

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                CmdcreatTroop(hit.point);

                if (myTroop != null)
                {
                    myTroop.tag = "Player";
                    myTroop.name = "MyArcher";

                }

            }

        }

    }



    // public void troopImageLarge()
    // {
    //     transform.GetComponent<RawImage>().transform.localScale = new Vector3(1.1f, 1.25f, 1f);
    // }
    // public void troopImageNormal()
    // {
    //     transform.GetComponent<RawImage>().transform.localScale = new Vector3(1f, 1f, 1f);
    // }


    [Command]
    void CmdcreatTroop(Vector3 pos)
    {
        myTroop = Instantiate(Troop, pos, Quaternion.identity) as GameObject;
        myTroop.GetComponent<HealthControler>().BlueTeam = this.BlueTeam;
        if(myTroop.GetComponent<subtroopCreat>()!=null){
           foreach (Transform item in  myTroop.GetComponent<subtroopCreat>().subtroopPos)
           {
GameObject submytroop=Instantiate(myTroop.GetComponent<subtroopCreat>().subTroopPrefebs,item.position,item.rotation) as GameObject;
               submytroop.GetComponent<HealthControler>().BlueTeam = this.BlueTeam;
               NetworkServer.SpawnWithClientAuthority(submytroop, connectionToClient);
           }
           
        }
        NetworkServer.SpawnWithClientAuthority(myTroop, connectionToClient);

    }


    [Command]
    void CmdTowerCreat(bool tellIsBlueTeam)
    {

        Lefttower = Instantiate(TowerPrefebs, LeftTowerPos.transform.position, LeftTowerPos.transform.rotation);
        Righttower = Instantiate(TowerPrefebs, RightTowerPos.transform.position, RightTowerPos.transform.rotation);
        Lefttower.GetComponent<HealthControler>().BlueTeam = tellIsBlueTeam;
        Righttower.GetComponent<HealthControler>().BlueTeam = tellIsBlueTeam;
        NetworkServer.SpawnWithClientAuthority(Lefttower, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(Righttower, connectionToClient);
    }


}




