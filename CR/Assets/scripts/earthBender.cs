using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Networking;


public class earthBender :  NetworkBehaviour {

    // [SerializeField] GameObject Fireball;
    [SerializeField] GameObject[] rock;

    [SerializeField] Material[] matarials;



    [SerializeField] Transform firePoint;
    NavMeshAgent navAgent;

    private bool walking = false;
    [SerializeField] public Transform targetedEnemy;
    private bool isAttacking = false;
    [SerializeField] private float shootDistance = 10f;
    private float nextFire;
    [SerializeField] private float timeBetweenShots = 2f;
    private Animator anim;
    float bulletLifeTime = 5f;
    [SerializeField] float bulletSpeed = 50f;
	[SerializeField] float corutineTime=1f;
	[SerializeField] float HipsY=0;




    // Use this for initialization
    void Start()
    {
        if (!hasAuthority) return;

        tag = "Player";
        name = "Myskeleton";
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasAuthority)
        {
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

                            if (navAgent.remainingDistance >= shootDistance)
                            {
                                navAgent.Resume();
                                walking = true;
                            }

                            if (navAgent.remainingDistance <= shootDistance)
                            {
                                transform.LookAt(targetedEnemy);
                                if (transform.eulerAngles.x <= 180f) { PlayerRotationX = transform.eulerAngles.x; } else { PlayerRotationX = transform.eulerAngles.x - 360f; }
                                if (transform.eulerAngles.y <= 180f) { PlayerRotationY = transform.eulerAngles.y; } else { PlayerRotationY = transform.eulerAngles.y - 360f; }
                                if (transform.eulerAngles.z <= 180f) { PlayerRotationZ = transform.eulerAngles.z; } else { PlayerRotationZ = transform.eulerAngles.z - 360f; }
                                PlayerRotationX = 0f;
								PlayerRotationY=PlayerRotationY+HipsY;
                                transform.localRotation = Quaternion.Euler(PlayerRotationX, PlayerRotationY, PlayerRotationZ);

                                if (Time.time > nextFire)
                                {
                                
                                    isAttacking = true;
                                    CmdAttackAnimation();
                                    
                                    nextFire = Time.time + timeBetweenShots;
                                    targetedEnemy = null;
                                }
                                navAgent.Stop();
                                walking = false;
                            }



                        }
                    }
                }
                if (targetedEnemy == null)
                {
                    walking = false; isAttacking = false;
                    navAgent.destination = transform.position;
                    navAgent.Resume();
                }

            }
            if (anim != null) { anim.SetBool("IsWalking", walking); }
        }
    }


    float ArrowPointRotationX;
    float ArrowPointRotationY;
    float ArrowPointRotationZ;

    float PlayerRotationX;
    float PlayerRotationY;
    float PlayerRotationZ;

    void CmdAttackAnimation()
    {

        if (transform.eulerAngles.x <= 180f) { ArrowPointRotationX = transform.eulerAngles.x; } else { ArrowPointRotationX = transform.eulerAngles.x - 360f; }

        ArrowPointRotationX = ArrowPointRotationX * (-1);

        if (transform.localRotation.eulerAngles.y <= 180f) { ArrowPointRotationY = firePoint.localRotation.eulerAngles.y; } else { ArrowPointRotationY = firePoint.localRotation.eulerAngles.y - 360f; }

        if (transform.localRotation.eulerAngles.z <= 180f) { ArrowPointRotationZ = firePoint.localRotation.eulerAngles.z; } else { ArrowPointRotationZ = firePoint.localRotation.eulerAngles.z - 360f; }

        firePoint.localRotation = Quaternion.Euler(ArrowPointRotationX, ArrowPointRotationY, ArrowPointRotationZ);


        anim.SetTrigger("Attack");
    }


    [Command]
    void CmdBowCreat99()
    {

        GameObject bullet = Instantiate(rock[Random.Range(0, rock.Length)], firePoint.position, firePoint.rotation);
        bullet.transform.GetChild(0).GetComponent<Renderer>().material=matarials[Random.Range(0,matarials.Length)];
        StartCoroutine(ExampleCoroutine(bullet));
		if (transform.GetComponent<HealthControler>().BlueTeam) { bullet.tag = "BlueFire"; }
        if (!transform.GetComponent<HealthControler>().BlueTeam) { bullet.tag = "RedFire"; }
        NetworkServer.SpawnWithClientAuthority(bullet, GameObject.FindGameObjectWithTag("PlayerBase"));


    }






    public void CmdBowCreat()
    {
        if(hasAuthority){CmdBowCreat99();}
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

	IEnumerator ExampleCoroutine(GameObject bullet){
		yield return bullet.transform.GetComponent<Rigidbody>().velocity = bullet.transform.up * bulletSpeed;
		yield return new WaitForSeconds(corutineTime);
		yield return bullet.transform.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed*3;
	}

}
