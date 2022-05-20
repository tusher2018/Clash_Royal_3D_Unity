using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class subTower : NetworkBehaviour
{

    [SerializeField] bool LeftTower;
    [SerializeField] bool RightTower;
    [SerializeField] bool MiddleTower;
   
   void Start()
   {
       if (!hasAuthority) return;
       transform.tag="Base";
   }
   
    void Update()
    {
        
    }


}
