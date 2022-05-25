using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class subTower : NetworkBehaviour
{

    [SerializeField] bool LeftTower;
    [SerializeField] bool RightTower;
    [SerializeField] bool MiddleTower;
    [SerializeField] string TagMe="Base";
   
   void Start()
   {
       if (!hasAuthority) return;
       transform.tag=TagMe;
   }
   
    void Update()
    {
        
    }


}
