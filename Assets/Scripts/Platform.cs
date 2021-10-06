using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      other.transform.parent = gameObject.transform;
    }
  }
  
  private void OnTriggerExit(Collider other) {
    if (other.tag == "Player") {
      other.transform.parent = null;
    }
  }

  public void HitTheBottom (Collider other) {
    if (other.gameObject.tag == "Player") {
      Player player = GameObject.Find("Player").GetComponent<Player>();

      if ( player ) {
        player.SetYVelocity(1.0f);
      }
      
    }
  }
}
