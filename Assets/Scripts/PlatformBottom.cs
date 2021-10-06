using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBottom : MonoBehaviour
{
  private Platform _parent;

  void Start()
  {
    _parent = transform.parent.GetComponent<Platform>();
    if ( _parent == null) {
      Debug.LogError("This bottom has no parent");
    }
  }

  private void OnTriggerEnter(Collider other) {
    this._parent.HitTheBottom(other);
  }
}
