using System;
using System.Collections;
using System.Collections.Generic;
using CustomGameEvent;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerController _playerController;

    public void SetPlayerController(PlayerController playerController)
    {
        _playerController = playerController;
    }
    
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (_playerController != null)
        {
            var HorizontalValue = Input.GetAxis("Horizontal");
            var VerticalValue = Input.GetAxis("Vertical");
            if (!HorizontalValue.Equals(0.0f) || !VerticalValue.Equals(0.0f))
            {
                _playerController.MovePlayer(new Vector3(HorizontalValue, 0.0f, VerticalValue).normalized);
                _playerController.RotatePlayer(new Vector3(HorizontalValue, 0.0f, VerticalValue).normalized);
                GameEvent.SendMovePlayer(_playerController.gameObject.transform.position);
            }
            else
            {
                GameEvent.SendMovePlayer(_playerController.gameObject.transform.position);
            }
        }
    }
    
}
