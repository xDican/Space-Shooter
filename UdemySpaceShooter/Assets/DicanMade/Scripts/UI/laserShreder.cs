using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserShreder : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) 
   {
        if(other.gameObject.tag == "EnemyShot" || other.gameObject.tag == "PlayerShot")
        {
            Destroy(other.gameObject);
        }    
   }
}
