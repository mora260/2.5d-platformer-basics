using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player") {
      Player player = GameObject.Find("Player").GetComponent<Player>();

      if ( player ) {
        player.SetYVelocity(1.0f);
      }
      
    }
  }
}
