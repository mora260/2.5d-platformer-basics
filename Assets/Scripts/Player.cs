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
    private float _jumpVelocity = 6.0f;

    private CharacterController _characterController;
    private float _playerVelocityY;

    private bool _canDoubleJump;

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
        _playerVelocityY = _jumpVelocity;
        _canDoubleJump = true;
      }

      if (Input.GetButtonDown("Jump") && !_characterController.isGrounded && _canDoubleJump) {
        _playerVelocityY = _jumpVelocity * 0.8f;
        _canDoubleJump = false;
      }

      // apply displacement based on speeds
      _characterController.Move(new Vector3(horizontalMovement, _playerVelocityY, 0) * Time.deltaTime ); // multiply for Time.deltaTime to convert velocity to distance... move that distance
        
    }

    private void FixedUpdate() {
      // if player is grounded and velocity is negative, reset "y" velocity
      if (_characterController.isGrounded && _playerVelocityY < 0)
      { 
        _playerVelocityY = 0f;
        _canDoubleJump = false;
      }
      // applying gravity every fixed update
      _playerVelocityY += _gravityValue * Time.deltaTime; // multiply for Time.deltaTime to convert acceleration to velocity
    }

    public void SetYVelocity(float v) {
      _playerVelocityY = v;
    }
}
