using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class brute : NetworkBehaviour
{



    NavMeshAgent navAgent;
    public GameObject wephone;
    public GameObject EffectORAnyObject;


    private bool walking = false;
    [SerializeField] public Transform targetedEnemy;
    public bool isAttacking = false;
    [SerializeField] private float AttackDistance = 10f;
    private float nextAttack;
    [SerializeField] private float timeBetweenAttack = 2f;
    private Animator anim;
    float PlayerRotationX;
    float PlayerRotationY;
    float PlayerRotationZ;



    // Use this for initialization
    void Start()
    {
        if (!hasAuthority) return;

        tag = "Player";
        name = "MyArcher";
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
        
        tag = "Player";
        name = "MyArcher";


        if(targetedEnemy!=null){if(targetedEnemy.GetComponent<HealthControler>().BlueTeam==transform.GetComponent<HealthControler>().BlueTeam){targetedEnemy=null;}}

            if (navAgent == null)
            {
                navAgent = GetComponent<NavMeshAgent>();
            }

            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }



            if (GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                targetedEnemy = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy")).transform;

                if (targetedEnemy != null)
                {
                    if (navAgent == null)
                    {
                        navAgent = GetComponent<NavMeshAgent>();
                    }

                    if (anim == null)
                    {
                        anim = GetComponent<Animator>();
                    }

                    if (navAgent != null)
                    {
                        if (anim != null)
                        {
                            navAgent.destination = targetedEnemy.position;

                            if (navAgent.remainingDistance >= AttackDistance)
                            {
                                walking = true;
                                anim.SetBool("IsWalking", walking);
                                // navAgent.Resume();

                            }

                            if (navAgent.remainingDistance <= AttackDistance)
                            {
                                transform.LookAt(targetedEnemy);
                                if (transform.eulerAngles.x <= 180f) { PlayerRotationX = transform.eulerAngles.x; } else { PlayerRotationX = transform.eulerAngles.x - 360f; }
                                if (transform.eulerAngles.y <= 180f) { PlayerRotationY = transform.eulerAngles.y; } else { PlayerRotationY = transform.eulerAngles.y - 360f; }
                                if (transform.eulerAngles.z <= 180f) { PlayerRotationZ = transform.eulerAngles.z; } else { PlayerRotationZ = transform.eulerAngles.z - 360f; }
                                PlayerRotationX = 0f;
                                transform.localRotation = Quaternion.Euler(PlayerRotationX, PlayerRotationY, PlayerRotationZ);

                                if (Time.time > nextAttack)
                                {
                                    isAttacking = true;
                                    CmdAttackAnimation();

                                    nextAttack = Time.time + timeBetweenAttack;
                                    targetedEnemy = null;
                                }

                                navAgent.Stop();
                                walking = false;
                                anim.SetBool("IsWalking", walking);
                            }



                        }
                    }
                }
                if (targetedEnemy == null)
                {
                    walking = false; isAttacking = false;
                    navAgent.destination = transform.position;
                    anim.SetBool("IsWalking", walking);
                    navAgent.Resume();
                }

            }
            if (anim != null) { anim.SetBool("IsWalking", walking); }
        }
    }

    void CmdAttackAnimation()
    {
        anim.SetTrigger("Attack");
    }

    public void callNavAgent() { if(navAgent!=null) navAgent.Resume(); }

    public void colliderOpen()
    {
      if(wephone!=null)  wephone.GetComponent<Collider>().enabled = true;
    }
    public void colliderClose()
    {
      if(wephone!=null)  wephone.GetComponent<Collider>().enabled = false;
    }
        
    public void ObjectOpen()
    {
      if(EffectORAnyObject!=null)  EffectORAnyObject.SetActive(true);
    }
    public void ObjectClose()
    {
      if(EffectORAnyObject!=null)  EffectORAnyObject.SetActive(false);
    }




    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }



}
