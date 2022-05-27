using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class payerConorl : NetworkBehaviour
{
    [SerializeField] Vector3 CameraPos1;
    [SerializeField] Vector3 CameraPos2;

    [SerializeField] GameObject TowerPrefebs;
    [SerializeField] GameObject LeftTowerPos;
    [SerializeField] GameObject RightTowerPos;
    [SerializeField] GameObject middleTowerPos;
    GameObject Lefttower;
    GameObject Righttower;
    GameObject middletower;
    [SyncVar] public int score = 3;
    [SerializeField] TextMesh ScoreText;
    [SerializeField] public GameObject board;
    Ray ray;
    GameObject myTroop;
    bool exitroad = true;
    GameObject road = null;
    [SerializeField] GameObject Troop;
    RaycastHit hit;
    [SyncVar] public bool BlueTeam = false;
    bool calledtower = false;
    [SerializeField] float BoardRotationX;
    [SerializeField] float BoardRotationY;
    [SerializeField] float BoardRotationZ;

    [SerializeField] GameObject resultBoard;
    bool GameEnd = false;



    public override void OnStartLocalPlayer()
    {

        base.OnStartLocalPlayer();
        name = "PlayerBase";

        if (board.transform.eulerAngles.x <= 180f) { BoardRotationX = board.transform.localEulerAngles.x; } else { BoardRotationX = board.transform.localEulerAngles.x - 360f; }
        if (board.transform.eulerAngles.y <= 180f) { BoardRotationY = board.transform.localEulerAngles.y; } else { BoardRotationY = board.transform.localEulerAngles.y - 360f; }
        if (board.transform.eulerAngles.z <= 180f) { BoardRotationZ = board.transform.localEulerAngles.z; } else { BoardRotationZ = board.transform.localEulerAngles.z - 360f; }
        BoardRotationX += 180;
        board.transform.localRotation = Quaternion.Euler(BoardRotationX, BoardRotationY, BoardRotationZ);
    }



    void Start()
    {

    }

    void Update()
    {
        if (transform.position == GameObject.Find("Pos 1").transform.position)
        {
            BlueTeam = true;
            tag = "BluePlayerBase";
        }
        else
        {
            BlueTeam = false;
            tag = "RedPlayerBase";
        }




        if (!isLocalPlayer) return;

        if (BlueTeam)
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = CameraPos1;
            GameObject.FindGameObjectWithTag("MainCamera").transform.localRotation = Quaternion.Euler(37, 180, 0);

        }
        else
        {
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = CameraPos2;
            GameObject.FindGameObjectWithTag("MainCamera").transform.localRotation = Quaternion.Euler(37, 0, 0); ;

        }


        if (!calledtower)
        {
            calledtower = true;

            CmdTowerCreat(this.BlueTeam);
            if (Lefttower != null) Lefttower.tag = "Base";
            if (Righttower != null) Righttower.tag = "Base";
            if (middletower != null) middletower.tag = "Base";


        }



        if (score == 0 && GameEnd == false)
        {
            CmdResult();
            GameEnd = true;
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





    [Command]
    void CmdcreatTroop(Vector3 pos)
    {
        myTroop = Instantiate(Troop, pos, Quaternion.identity) as GameObject;
        myTroop.GetComponent<HealthControler>().BlueTeam = this.BlueTeam;
        if (myTroop.GetComponent<subtroopCreat>() != null)
        {
            foreach (Transform item in myTroop.GetComponent<subtroopCreat>().subtroopPos)
            {
                GameObject submytroop = Instantiate(myTroop.GetComponent<subtroopCreat>().subTroopPrefebs, item.position, item.rotation) as GameObject;
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
        middletower = Instantiate(TowerPrefebs, middleTowerPos.transform.position, middleTowerPos.transform.rotation);
        Lefttower.GetComponent<HealthControler>().BlueTeam = tellIsBlueTeam;
        Righttower.GetComponent<HealthControler>().BlueTeam = tellIsBlueTeam;
        middletower.GetComponent<HealthControler>().BlueTeam = tellIsBlueTeam;
        NetworkServer.SpawnWithClientAuthority(Lefttower, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(Righttower, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(middletower, connectionToClient);
    }


    // void OnScoreChanged(int updatedHealth)
    // {
    //     ScoreText.text = updatedHealth.ToString();
    // }


    [Command]
    public void CmdResult()
    {
        GameObject[] allPlayer = GameObject.Find("Network").GetComponent<NetworkSpwan>().Players;
        GameObject resultboard = Instantiate(resultBoard, Vector3.zero, Quaternion.identity);
        resultboard.transform.GetComponent<resultBoard>().p1s = allPlayer[0].GetComponent<payerConorl>().score.ToString();
        resultboard.transform.GetComponent<resultBoard>().p2s = allPlayer[1].GetComponent<payerConorl>().score.ToString();
        resultboard.transform.GetComponent<resultBoard>().p1n = allPlayer[0].GetComponent<payerConorl>().netId.ToString();
        resultboard.transform.GetComponent<resultBoard>().p2n = allPlayer[1].GetComponent<payerConorl>().netId.ToString();
        NetworkServer.SpawnWithClientAuthority(resultboard, connectionToClient);
        foreach (NetworkTransform item in GameObject.FindObjectsOfType<NetworkTransform>())
        {
            if (item.gameObject.GetComponent<HealthControler>() != null)
            {
                item.gameObject.GetComponent<HealthControler>().health -= 100;
               
            }
        }

    }




}




