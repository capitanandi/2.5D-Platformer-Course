using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 0.6f;
    [SerializeField]
    private float _jumpHeight = 20.0f;
    [SerializeField]
    private float _yVelocity;
    private int coins;
    private int lives = 3;

    private bool groundedPlayer;
    private bool canDoubleJump = false;

    private CharacterController _controller;
    private UIManager _uiManager;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL.");
        }
        _uiManager.UpdateLivesDisplay(lives);

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        groundedPlayer = _controller.isGrounded;

        if (groundedPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight * 1.7f;
                    canDoubleJump = false;
                }
            }

            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    public void AddCoins()
    {
        coins++;
        _uiManager.UpdateCoinDisplay(coins);
    }

    public void UpdateLives()
    {
        lives--;
        _uiManager.UpdateLivesDisplay(lives);
        Debug.Log("Player::UpdateLives() Called");

        if(lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
