﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public Animator playerAnimator;
    private Rigidbody2D playerRigidbody;
    private float horizontalSpeed;
    private float verticalSpeed;
    private Vector3 targetVelocity;
    private Vector3 velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    public Vector2 playerPos;
    public float runSpeed;
    public PLAYER Player;

    public Tilemap gameBoardTileMap;
    public GameObject bombPrefab;


    public enum PLAYER
    {
        PLAYER_1,
        PLAYER_2
    }

    public enum AXIS
    {
        HORIZONTAL,
        VERTICAL
    }

    private void Start()
    {
        if (player1 == null)
        {
            player1 = GameObject.Find("Player1");
        }
        else
        {
            return;
        }

        if (player2 == null)
        {
            player2 = GameObject.Find("Player2");

        }
        else
        {
            return;
        }

        switch (getCurrentPlayer())
        {
            case PLAYER.PLAYER_1:
                {
                    playerPos = player1.transform.position;
                    playerAnimator = player1.GetComponent<Animator>();
                    break;
                }
            case PLAYER.PLAYER_2:
                {
                    playerPos = player2.transform.position;
                    playerAnimator = player2.GetComponent<Animator>();  
                    break;
                }
        }           
        playerRigidbody = GetComponent<Rigidbody2D>();     
    }

    void FixedUpdate()
    {          
        switch (getCurrentPlayer())
        {
            case PLAYER.PLAYER_1:
                {    
                    player1Movement();  
                    break;
                }
            case PLAYER.PLAYER_2:
                {
                    player2Movement();
                    
                    break;
                }
        }
        spawnBombs(getCurrentPlayer());
    }

    public void spawnBombs(PLAYER player)
    {
        if (player == PLAYER.PLAYER_1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 worldPos = Camera.main.ScreenToViewportPoint(this.transform.localPosition);
                Vector3Int cell = gameBoardTileMap.LocalToCell(worldPos);
                Vector3 cellCentrePos = gameBoardTileMap.GetCellCenterLocal(cell);

                Instantiate(bombPrefab, cellCentrePos, Quaternion.identity);
            }
        }
        else if(player == PLAYER.PLAYER_2)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Vector3 worldPos = Camera.main.ScreenToViewportPoint(player2.transform.position);
                Vector3Int cell = gameBoardTileMap.LocalToCell(worldPos);
                Vector3 cellCentrePos = gameBoardTileMap.GetCellCenterLocal(cell);

                Instantiate(bombPrefab, cellCentrePos, Quaternion.identity);
            }
        }
        
    }

    public void player1Movement()
    {
        //Horizontal Movement
        horizontalSpeed = Input.GetAxis("HorizontalP1") * runSpeed;
        playerAnimator.SetFloat("HS", horizontalSpeed);

        if (horizontalSpeed > 0)
        {
            playerAnimator.SetTrigger("WR");
            playerMovement(horizontalSpeed, AXIS.HORIZONTAL);
        }
        else if (horizontalSpeed < 0)
        {
            playerAnimator.SetTrigger("WL");
            playerMovement(horizontalSpeed, AXIS.HORIZONTAL);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            horizontalSpeed = 0;
            ResetAnimationTriggers();
            playerMovement(horizontalSpeed, AXIS.HORIZONTAL);
        }

        // Vertical Movement
        verticalSpeed = Input.GetAxis("VerticalP1") * runSpeed;
        playerAnimator.SetFloat("VS", verticalSpeed);

        if (verticalSpeed > 0)
        {
            playerAnimator.SetTrigger("WU");
            playerMovement(verticalSpeed, AXIS.VERTICAL);
        }
        else if (verticalSpeed < 0)
        {
            playerAnimator.SetTrigger("WD");
            playerMovement(verticalSpeed, AXIS.VERTICAL);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            verticalSpeed = 0;
            ResetAnimationTriggers();
            playerMovement(verticalSpeed, AXIS.VERTICAL);
        }
    }

    public void player2Movement()
    {
        //Horizontal Movement
        horizontalSpeed = Input.GetAxis("HorizontalP2") * runSpeed;
        playerAnimator.SetFloat("HS", horizontalSpeed);

        if (horizontalSpeed > 0)
        {
            playerAnimator.SetTrigger("WR");
            playerMovement(horizontalSpeed, AXIS.HORIZONTAL);
        }
        else if (horizontalSpeed < 0)
        {
            playerAnimator.SetTrigger("WL");
            playerMovement(horizontalSpeed, AXIS.HORIZONTAL);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            horizontalSpeed = 0;
            ResetAnimationTriggers();
            playerMovement(horizontalSpeed, AXIS.HORIZONTAL);
        }

        // Vertical Movement
        verticalSpeed = Input.GetAxis("VerticalP2") * runSpeed;
        playerAnimator.SetFloat("VS", verticalSpeed);

        if (verticalSpeed > 0)
        {
            playerAnimator.SetTrigger("WU");
            playerMovement(verticalSpeed, AXIS.VERTICAL);
        }
        else if (verticalSpeed < 0)
        {
            playerAnimator.SetTrigger("WD");
            playerMovement(verticalSpeed, AXIS.VERTICAL);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            verticalSpeed = 0;
            ResetAnimationTriggers();
            playerMovement(verticalSpeed, AXIS.VERTICAL);
        }
    }

    public void playerMovement(float speed, AXIS axis)
    {
        if (axis == AXIS.HORIZONTAL)
        {
            // Move the character by finding the target velocity
            targetVelocity = new Vector2(speed * 2f, playerRigidbody.velocity.y);
        }
        else if (axis == AXIS.VERTICAL)
        {
            // Move the character by finding the target velocity
            targetVelocity = new Vector2(playerRigidbody.velocity.x, speed * 2f);
        }
   
        playerRigidbody.velocity = targetVelocity;
    }

    public PLAYER getCurrentPlayer()
    {
        return Player;
    }

    public void ResetAnimationTriggers()
    {
        playerAnimator.ResetTrigger("WU");
        playerAnimator.ResetTrigger("WD");
        playerAnimator.ResetTrigger("WR");
        playerAnimator.ResetTrigger("WL");
    }
}