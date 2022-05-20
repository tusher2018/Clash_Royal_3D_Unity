using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Arow_Bullet : NetworkBehaviour
{


    [SerializeField] public float damage = 10f;

    [SerializeField] public GameObject blustEffect;
	[SerializeField] public Transform blustEffectPoint;
    [SerializeField] public float time=5f;




}
