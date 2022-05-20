using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthControler : NetworkBehaviour
{

    //[SyncVar(hook = "OnHealthChanged")]
    [SyncVar] public float health = 100f;
    [SerializeField] GameObject effect;
    [SerializeField] Transform effectPos;
    [SyncVar] public bool BlueTeam = false;

    float dam;

    void Start()
    {

    }


    void Update()
    {
        if (hasAuthority)
        {

        }
    }

    [Command]
    void CmdDestroy()
    {
        NetworkServer.Spawn(Instantiate(effect, effectPos.position, effectPos.rotation));
        Destroy(transform.GetComponent<NetworkTransform>());
        NetworkServer.Destroy(transform.gameObject);

    }


    [Command]
    void CmdArrowDestroy(GameObject arrow)
    {
        if (arrow.GetComponent<Arow_Bullet>().blustEffect != null)
        {
            GameObject blust = Instantiate(arrow.transform.GetComponent<Arow_Bullet>().blustEffect, arrow.transform.position, Quaternion.identity);
            NetworkServer.Spawn(blust);
            Destroy(blust, arrow.GetComponent<Arow_Bullet>().time);
        }

        NetworkServer.Destroy(arrow);

    }



    [Command]
    void CmdBlust(GameObject blustEffect, Vector3 pos, float time)
    {
        GameObject blust = Instantiate(blustEffect, pos, Quaternion.identity);
        NetworkServer.Spawn(blust);
        Destroy(blust, time);
    }


    void OnTriggerEnter(Collider other)
    {
        if (BlueTeam)
        {
            if (other.gameObject.CompareTag("RedFire"))
            {
                dam = other.gameObject.GetComponent<Arow_Bullet>().damage;
                takeDamage(other.gameObject);

            }
            if (other.gameObject.CompareTag("Wephone"))
            {
                if (!other.transform.root.GetComponent<HealthControler>().BlueTeam){
                    dam= other.gameObject.GetComponent<WephoneDamage>().damage;
                    takeDamage();
                }
            }

        }
        if (!BlueTeam)
        {
            if (other.gameObject.CompareTag("BlueFire"))
            {
                dam = other.gameObject.GetComponent<Arow_Bullet>().damage;
                takeDamage(other.gameObject);

            }
            if (other.gameObject.CompareTag("Wephone"))
            {
                if (other.transform.root.GetComponent<HealthControler>().BlueTeam){
                    dam= other.gameObject.GetComponent<WephoneDamage>().damage;
                    takeDamage();
                }
            }
        }

    }





    void takeDamage(GameObject ARROW)
    {
        if (isServer)
        {

            CmdArrowDestroy(ARROW);

            health -= dam;

            if (health <= 0)
            {
                CmdDestroy();
            }

        }
    }

    void takeDamage()
    {
        if (isServer)
        {

            health -= dam;

            if (health <= 0)
            {
                CmdDestroy();
            }

        }
    }



    void OnHealthChanged(float updatedHealth)
    {




    }



}
