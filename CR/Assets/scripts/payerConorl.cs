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
    [SerializeField] GameObject middleTowerPos;
    GameObject Lefttower;
    GameObject Righttower;
    GameObject middletower;
    [SyncVar(hook = "OnScoreChanged")] public int score = 0;
    [SerializeField] TextMesh ScoreText;

    [SerializeField] GameObject canvas;
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
    [SerializeField] float VSBoardRotationX;
    [SerializeField] float VSBoardRotationY;
    [SerializeField] float VSBoardRotationZ;
    // [SerializeField] float VSBoardRotationX_Score;
    // [SerializeField] float VSBoardRotationY_Score;
    // [SerializeField] float VSBoardRotationZ_Score;



    // [Header("End Game")]
    // [SerializeField] public GameObject endpoint;
    [SerializeField] public GameObject VSBoard;

    [SyncVar] public bool EndGame = false;

    public override void OnStartLocalPlayer()
    {

        base.OnStartLocalPlayer();
        name = "PlayerBase";

        if (board.transform.eulerAngles.x <= 180f) { BoardRotationX = board.transform.localEulerAngles.x; } else { BoardRotationX = board.transform.localEulerAngles.x - 360f; }
        if (board.transform.eulerAngles.y <= 180f) { BoardRotationY = board.transform.localEulerAngles.y; } else { BoardRotationY = board.transform.localEulerAngles.y - 360f; }
        if (board.transform.eulerAngles.z <= 180f) { BoardRotationZ = board.transform.localEulerAngles.z; } else { BoardRotationZ = board.transform.localEulerAngles.z - 360f; }
        BoardRotationX += 180;
        board.transform.localRotation = Quaternion.Euler(BoardRotationX, BoardRotationY, BoardRotationZ);

        // if (VSBoard.transform.eulerAngles.x <= 180f) { VSBoardRotationX = VSBoard.transform.localEulerAngles.x; } else { VSBoardRotationX = VSBoard.transform.localEulerAngles.x - 360f; }
        // if (VSBoard.transform.eulerAngles.y <= 180f) { VSBoardRotationY = VSBoard.transform.localEulerAngles.y; } else { VSBoardRotationY = VSBoard.transform.localEulerAngles.y - 360f; }
        // if (VSBoard.transform.eulerAngles.z <= 180f) { VSBoardRotationZ = VSBoard.transform.localEulerAngles.z; } else { VSBoardRotationZ = VSBoard.transform.localEulerAngles.z - 360f; }
        // VSBoardRotationX += 180;
        // VSBoard.transform.localRotation = Quaternion.Euler(VSBoardRotationX, VSBoardRotationY, VSBoardRotationZ);

        // for (int i = 0; i < 3; i++){
        //      if (VSBoard.transform.GetChild(i).eulerAngles.x <= 180f) { VSBoardRotationX_Score = VSBoard.transform.GetChild(i).localEulerAngles.x; } else { VSBoardRotationX_Score = VSBoard.transform.GetChild(i).localEulerAngles.x - 360f; }
        //         if (VSBoard.transform.GetChild(i).eulerAngles.y <= 180f) { VSBoardRotationY_Score = VSBoard.transform.GetChild(i).localEulerAngles.y; } else { VSBoardRotationY_Score = VSBoard.transform.GetChild(i).localEulerAngles.y - 360f; }
        //         if (VSBoard.transform.GetChild(i).eulerAngles.z <= 180f) { VSBoardRotationZ_Score = VSBoard.transform.GetChild(i).localEulerAngles.z; } else { VSBoardRotationZ_Score = VSBoard.transform.GetChild(i).localEulerAngles.z - 360f; }
        //         VSBoardRotationY_Score += 180;
        //         VSBoard.transform.GetChild(i).localRotation = Quaternion.Euler(VSBoardRotationX_Score, VSBoardRotationY_Score, VSBoardRotationZ_Score);
        // }



        canvas = GameObject.FindGameObjectWithTag("Canvas");

    }



    void Start()
    {
        // canvas.SetActive(false);
        // network = GameObject.Find("Network").GetComponent<NetworkSpwan>();
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
            BlueTeam = false; tag = "RedPlayerBase";
        }

  


        if (!isLocalPlayer) return;




        if (!calledtower)
        {
            calledtower = true;

            CmdTowerCreat(this.BlueTeam);
            if (Lefttower != null) Lefttower.tag = "Base";
            if (Righttower != null) Righttower.tag = "Base";
            if (middletower != null) middletower.tag = "Base";


        }

        if (score != GameObject.FindGameObjectsWithTag("Base").Length)
        {
            CmdScore(Mathf.Abs(GameObject.FindGameObjectsWithTag("Base").Length-3));
        }


        // call Game End
        // if (score == 0)
        // {
        //     CmdEndGame(true);
        // }

        // if (transform.tag == "BluePlayerBase")
        // {if(GameObject.FindGameObjectWithTag("RedPlayerBase")!=null)
        //     if (GameObject.FindGameObjectWithTag("RedPlayerBase").GetComponent<payerConorl>().EndGame == true) { this.EndGame = true; }
        // }
        //   if (transform.tag == "RedPlayerBase")
        // {if(GameObject.FindGameObjectWithTag("BluePlayerBase")!=null)
        //     if (GameObject.FindGameObjectWithTag("BluePlayerBase").GetComponent<payerConorl>().EndGame == true) { this.EndGame = true; }
        // }

        // if (EndGame)
        // {
        //     CmdResult();
        // }






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



    //     [Command]
    //     public void CmdGameWon()
    //     {
    //         RpcGameEnd(this.netId);
    //     }

    [ClientRpc]
    public void RpcGameEnd(NetworkInstanceId nid)
    {
        GameObject bluePlayer = GameObject.FindGameObjectWithTag("BluePlayerBase");
        GameObject redPlayer = GameObject.FindGameObjectWithTag("RedPlayerBase");
        if (isLocalPlayer)
        {

            if (this.netId == nid)
            {


            }
            else
            {
                //Process win here

            }
        }
    }



    [Command]
    void CmdScore(int scorePoint)
    {
        score = scorePoint;
        ScoreText.text = scorePoint.ToString();
    }

    void OnScoreChanged(int updatedHealth)
    {
        ScoreText.text = updatedHealth.ToString();
    }



    [Command]
    void CmdEndGame(bool end)
    {
        // RpcGameEnd(this.netId);
        this.EndGame = end;
    }
    [Command]
    void CmdResult()
    {
        // VSBoard.SetActive(true);
        // var foundCanvasObjects = FindObjectsOfType<HealthControler>();
        // foreach (var item in foundCanvasObjects)
        // {
        //     item.GetComponent<HealthControler>().health = 0;
        // }
        // VSBoard.transform.GetChild(0).GetComponent<Text>().text=score.ToString();

        // if (transform.tag == "BluePlayerBase")
        // {if(GameObject.FindGameObjectWithTag("RedPlayerBase")!=null)
        //    VSBoard.transform.GetChild(1).GetComponent<Text>().text=  GameObject.FindGameObjectWithTag("RedPlayerBase").GetComponent<payerConorl>().score.ToString();
        // }
        //   if (transform.tag == "RedPlayerBase")
        // {if(GameObject.FindGameObjectWithTag("BluePlayerBase")!=null)
        //    VSBoard.transform.GetChild(1).GetComponent<Text>().text=  GameObject.FindGameObjectWithTag("BluePlayerBase").GetComponent<payerConorl>().score.ToString();
        // }

    }



}




