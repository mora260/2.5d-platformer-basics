using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float _groundedTimer;

    private int _coins;
    private int _lives;
    private bool _death;

    private UIManager _uiManager;

    [SerializeField]
    Transform _spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _coins = 0;
        _lives = 3;
        _death = false;
        _characterController = GetComponent<CharacterController>();
        if (_characterController == null) {
          Debug.Log("Hey! no cc!!");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null) {
          Debug.Log("Hey! no uiManager!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
      if (_characterController.isGrounded) {
        _groundedTimer = 0.3f;
      }

      if (_groundedTimer > 0)
      {
        _groundedTimer -= Time.deltaTime;
      }

      if (Input.GetButtonDown("Jump") && _groundedTimer == 0 && _canDoubleJump) {
        _playerVelocityY = _jumpVelocity * 0.8f;
        _canDoubleJump = false;
      }
      
      // if a Jump is detected, modify speed in y axis 
      if (Input.GetButtonDown("Jump") && _groundedTimer > 0 ) {
        _groundedTimer = 0;
        _playerVelocityY = _jumpVelocity;
        _canDoubleJump = true;
      }

      // get speed in x axis
      float horizontalMovement = Input.GetAxis("Horizontal") * _playerMovementSpeed;
      // apply displacement based on speeds
      _characterController.Move(new Vector3(horizontalMovement, _playerVelocityY, 0) * Time.deltaTime ); // multiply for Time.deltaTime to convert velocity to distance... move that distance

      if (transform.position.y < -25.0f && !_death) {
        LoseALife();
        _death = true;
      }         
    }

    private void FixedUpdate() {
      // applying gravity every fixed update
      _playerVelocityY += _gravityValue * Time.deltaTime; // multiply for Time.deltaTime to convert acceleration to velocity
      
      // if player is grounded and velocity is negative, reset "y" velocity
      if (_characterController.isGrounded && _playerVelocityY < 0f)
      { 
        _playerVelocityY = 0f;
        _canDoubleJump = false;
      }
      
      if (_death) { 
        _playerVelocityY = 0f;
        // reset spawn point
        transform.position = _spawnPoint.position;
        _death = false;
      }
    }

    public void SetYVelocity(float v) {
      _playerVelocityY = v;
    }

    public void IncreaseCoins() {
      _coins++;
      if (_uiManager) {
        _uiManager.SetCoinsText(_coins);
      }
    }

    public void LoseALife() {
      _lives--;
      if (_uiManager) {
        _uiManager.SetLivesText(_lives);
      }

      if (_lives == 0) {
        SceneManager.LoadScene("Level1");
      }
    }
}
