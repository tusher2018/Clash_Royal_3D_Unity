using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class Archer : NetworkBehaviour
{

    [SerializeField] GameObject Arow;
    [SerializeField] Transform ArowPoint;
    NavMeshAgent navAgent;
    [SerializeField] GameObject Bow;
    [SerializeField] GameObject eye;
    private bool walking = false;
    [SerializeField] public Transform targetedEnemy;
    private bool isAttacking = false;
    [SerializeField] private float shootDistance = 10f;
    private float nextFire;
    private float timeBetweenShots = 2f;
    private Animator anim;
    float bulletLifeTime = 5f;
    [SerializeField] float bulletSpeed = 4f;




 
    void Start()
    {
        if (!hasAuthority) return;

        tag = "Player";
        name = "MyArcher";
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();


    }

 
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
                                transform.localRotation = Quaternion.Euler(PlayerRotationX, PlayerRotationY, PlayerRotationZ);

                                if (Time.time > nextFire)
                                {
                                    isAttacking = true;
                                    CmdAttackAnimation();
                                    // CmdBowCreat99();
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


    [SerializeField] float ArrowPointRotationX;
    [SerializeField] float ArrowPointRotationY;
    [SerializeField] float ArrowPointRotationZ;

    [SerializeField] float PlayerRotationX;
    [SerializeField] float PlayerRotationY;
    [SerializeField] float PlayerRotationZ;

    void CmdAttackAnimation()
    {

        if (transform.eulerAngles.x <= 180f) { ArrowPointRotationX = transform.eulerAngles.x; } else { ArrowPointRotationX = transform.eulerAngles.x - 360f; }

        ArrowPointRotationX = ArrowPointRotationX * (-1);

        if (transform.localRotation.eulerAngles.y <= 180f) { ArrowPointRotationY = ArowPoint.localRotation.eulerAngles.y; } else { ArrowPointRotationY = ArowPoint.localRotation.eulerAngles.y - 360f; }

        if (transform.localRotation.eulerAngles.z <= 180f) { ArrowPointRotationZ = ArowPoint.localRotation.eulerAngles.z; } else { ArrowPointRotationZ = ArowPoint.localRotation.eulerAngles.z - 360f; }

        ArowPoint.localRotation = Quaternion.Euler(ArrowPointRotationX, ArrowPointRotationY, ArrowPointRotationZ);


        anim.SetTrigger("Attack");
    }


    [Command]
    void CmdBowCreat99()
    {
        GameObject bullet = Instantiate(Arow, ArowPoint.position, ArowPoint.rotation);
        bullet.transform.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
        if (transform.GetComponent<HealthControler>().BlueTeam) { bullet.tag = "BlueFire"; }
        if (!transform.GetComponent<HealthControler>().BlueTeam) { bullet.tag = "RedFire"; }
        NetworkServer.Spawn(bullet);


    }






    public void CmdBowCreat()
    {
        if (hasAuthority) { CmdBowCreat99(); }
    }






    public void CmdropeAttack()
    {
        Bow.GetComponent<Animator>().PlayInFixedTime("rope Attack");
    }

    public void CallRotatiion()
    {
 
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
