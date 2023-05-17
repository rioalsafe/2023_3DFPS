using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject sparkEffect;

   void OnCollisionEnter(Collision coll)
   {
        if(coll.collider.tag == "BULLET")
        {
            ContactPoint cp = coll.contacts[0];

            Quaternion rot = Quaternion.LookRotation(-cp.normal);

            GameObject spark = Instantiate(sparkEffect, cp.point, rot);

            Destroy(spark, 0.5f);
            
            Destroy(coll.gameObject);
        }
   }
}
