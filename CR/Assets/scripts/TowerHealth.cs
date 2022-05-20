using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TowerHealth : NetworkBehaviour
{


    [SyncVar(hook = "OnHealthChangedLeftTower")] public float LeftTowerHealth = 200f;
    [SyncVar(hook = "OnHealthChangedRightTower")] public float RightTowerHealth = 200f;
    [SyncVar(hook = "OnHealthChangedMiddleTower")] public float MiddleTowerHealth = 200f;
    [SerializeField] GameObject effect;
    [SerializeField] TextMesh leftTowerTextEnemy;
    [SerializeField] TextMesh rightTowerTextEnemy;
    [SerializeField] TextMesh middleTowerTextEnemy;
    [SerializeField] TextMesh leftTowerTextPlayer;
    [SerializeField] TextMesh rightTowerTextPlayer;
    [SerializeField] TextMesh middleTowerTextPlayer;
    [SerializeField] Transform LeftEffectPos;
    [SerializeField] Transform RightEffectPos;
    [SerializeField] Transform MiddleEffectPos;



 

    void Start()
    {

    }

    void Update()
    {
       
        
        
    }

    // [Command]
    // void CmdDestroyLeftTower()
    // {
    //     NetworkServer.Spawn(Instantiate(effect, LeftEffectPos.position, LeftEffectPos.rotation));
    //     Destroy(transform.GetChild(0).gameObject);

    // }
    // [Command]
    // void CmdDestroyRightTower()
    // {
    //     NetworkServer.Spawn(Instantiate(effect, RightEffectPos.position, RightEffectPos.rotation));
    //     Destroy(transform.GetChild(1).gameObject);

    // }
    // [Command]
    // void CmdDestroyMiddleTower()
    // {
    //     NetworkServer.Spawn(Instantiate(effect, MiddleEffectPos.position, MiddleEffectPos.rotation));
    //     Destroy(transform.GetChild(2).gameObject);

    // }


    // [Command]
    // void CmdArrowDestroy(GameObject arrow)
    // {
    //     NetworkServer.Destroy(arrow);

    // }




    // public void takeDamageLeftTower(GameObject ARROW,float dam)
    // {
    //     if (isServer)
    //     {
    //         CmdArrowDestroy(ARROW);
    //         LeftTowerHealth -= dam;

    //         if (LeftTowerHealth <= 0)
    //         {
    //             CmdDestroyLeftTower();
    //         }

    //     }
    // }
    // public void takeDamageRightTower(GameObject ARROW,float dam)
    // {
    //     if (isServer)
    //     {
    //         CmdArrowDestroy(ARROW);
    //         RightTowerHealth -= dam;

    //         if ( RightTowerHealth <= 0)
    //         {
    //             CmdDestroyRightTower();
    //         }

    //     }
    // }
    // public void takeDamageMiddleTower(GameObject ARROW,float dam)
    // {
    //     if (isServer)
    //     {
    //         CmdArrowDestroy(ARROW);
    //         MiddleTowerHealth -= dam;

    //         if (MiddleTowerHealth <= 0)
    //         {
    //             CmdDestroyMiddleTower();
    //         }

    //     }
    // }


    void OnHealthChangedLeftTower(float updatedHealth)
    {
      
            leftTowerTextEnemy.text = LeftTowerHealth.ToString();
            leftTowerTextPlayer.text = LeftTowerHealth.ToString();
    

        // if (LeftTowerHealth <= 0)
        // {
        //     CmdDestroyLeftTower();
        // }
    }

    void OnHealthChangedRightTower(float updatedHealth)
    {
       
            rightTowerTextEnemy.text = RightTowerHealth.ToString();
            rightTowerTextPlayer.text = RightTowerHealth.ToString();
    

        // if (RightTowerHealth <= 0)
        // {
        //     CmdDestroyRightTower();
        // }
    }
    void OnHealthChangedMiddleTower(float updatedHealth)
    {
       
            middleTowerTextEnemy.text =  MiddleTowerHealth.ToString();
            middleTowerTextPlayer.text =  MiddleTowerHealth.ToString();
    

        // if (MiddleTowerHealth <= 0)
        // {
        //     CmdDestroyMiddleTower();
        // }
    }




    [Command]
    void CmdDestroy()
    {
        NetworkServer.Spawn(Instantiate(effect,  LeftEffectPos.position,  LeftEffectPos.rotation));
        transform.GetChild(0).gameObject.SetActive(false);
    }


    [Command]
    void CmdArrowDestroy(GameObject arrow)
    {
        NetworkServer.Destroy(arrow);

    }







    public void takeDamage(GameObject ARROW,float dam)
    {
        if (isServer)
        {
            CmdArrowDestroy(ARROW);
           LeftTowerHealth -= dam;

            if (LeftTowerHealth <= 5)
            {
                CmdDestroy();
                RpcChildDestroy();
            }

        }
    }

[ClientRpc]
void RpcChildDestroy(){
if(isLocalPlayer){
    transform.GetChild(0).gameObject.SetActive(false);
}
}





}
