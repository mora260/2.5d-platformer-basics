using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player") {
        Player player = other.GetComponent<Player>();
        if ( player != null) {
          player.IncreaseCoins();
        }
        Destroy(gameObject);
      }
    }
}
