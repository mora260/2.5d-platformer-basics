using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

  [SerializeField]
  private Transform _pointA, _pointB;

  [SerializeField]
  private float _speed;

  private bool _movingTowardsB;

  private void Start() {
    if (_pointA != null && _pointB != null)
    _movingTowardsB = true;
  }

  private void FixedUpdate() {
    if (_pointA != null && _pointB != null) {
      if (_movingTowardsB) {
        transform.position = Vector3.MoveTowards(transform.position, _pointB.transform.position, 0.01f * _speed);
      } else {
        transform.position = Vector3.MoveTowards(transform.position, _pointA.transform.position, 0.01f * _speed);
      }

      if(transform.position == _pointB.transform.position && _movingTowardsB) {
        _movingTowardsB = false;
      }

      if(transform.position == _pointA.transform.position && !_movingTowardsB) {
        _movingTowardsB = true;
      }
    }
  }

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
    if (other.tag == "Player") {
      Player player = GameObject.Find("Player").GetComponent<Player>();

      if ( player ) {
        player.SetYVelocity(1.0f);
      }
      
    }
  }
}
