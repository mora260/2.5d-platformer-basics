using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _gravityValue = -9.8f;
    
    [SerializeField]
    private float _playerMovementSpeed = 2.0f;

    [SerializeField]
    private float _jumpHeight = 3.0f;

    private CharacterController _characterController;
    private float _playerVelocityY;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null) {
          Debug.Log("Hey! no cc!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
      // get speed in x axis
      float horizontalMovement = Input.GetAxis("Horizontal") * _playerMovementSpeed;
      // if a Jump is detected, modify speed in y axis 
      if (Input.GetButtonDown("Jump") && _characterController.isGrounded) {
        _playerVelocityY += _jumpHeight * 2.0f;
      }
      // apply displacement based on speeds
      _characterController.Move(new Vector3(horizontalMovement, _playerVelocityY, 0) * Time.deltaTime ); // multiply for Time.deltaTime to convert velocity to distance... move that distance
        
    }

    private void FixedUpdate() {
      // if player is grounded and velocity is negative, reset "y" velocity
      if (_characterController.isGrounded && _playerVelocityY < 0)
      { 
          _playerVelocityY = 0f;
      }
      // applying gravity every fixed update
      _playerVelocityY += _gravityValue * Time.deltaTime; // multiply for Time.deltaTime to convert acceleration to velocity
    }

    // private void OnCollisionEnter(Collision other) {
    //   Debug.Log(other.gameObject.tag);
    //   if (other.gameObject.tag == "Terrain") {
    //     Debug.Log("Colliding");
    //   }
    // }
}
