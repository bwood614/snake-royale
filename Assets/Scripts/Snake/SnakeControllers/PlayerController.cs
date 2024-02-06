using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{

    public bool useController;
    public float turnSpeed;

    Vector3 mousePosition;
    Vector3 worldMousePosition;
    Quaternion travelAngle;
    bool controllerShot = false;

    public void Initialize()
    {
        
    }

    public void UpdateBoost(SnakeStateManager snake)
    {
        if (useController) {
            if (Input.GetAxis ("Boost") == 1 && !snake.currentState.Equals(snake.snakeBoostState)) {
                snake.SwitchState(snake.snakeBoostState);
            } 
        
            if (Input.GetAxis ("Boost") == 0 && !snake.currentState.Equals(snake.snakeNormalState)) {
                snake.SwitchState(snake.snakeNormalState);
            }
        } else {
            if (Input.GetMouseButton(0) && !snake.currentState.Equals(snake.snakeBoostState)) {
                snake.SwitchState(snake.snakeBoostState);
            } 
        
            if (!Input.GetMouseButton(0) && !snake.currentState.Equals(snake.snakeNormalState)) {
                snake.SwitchState(snake.snakeNormalState);
            }
        }
        
    }

    public void UpdateRotation(SnakeStateManager snake)
    {
        if (useController) {
            float horizonatalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            float targetTravelAngleZ = (Mathf.Atan2(verticalAxis, horizonatalAxis)* Mathf.Rad2Deg) - 90;
            Quaternion targetTravelAngle = Quaternion.Euler(0, 0, targetTravelAngleZ);
            travelAngle = Quaternion.RotateTowards(travelAngle, targetTravelAngle, turnSpeed * Time.deltaTime);
            snake.transform.rotation = travelAngle;
            
        } else {
            // get mouse position and convert to world point
            mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // calculate angle from player to mouse cursor
            float targetTravelAngleZ = Mathf.Atan2(worldMousePosition.y - snake.transform.position.y, worldMousePosition.x - snake.transform.position.x) * Mathf.Rad2Deg + 270;
            Quaternion targetTravelAngle = Quaternion.Euler(0, 0, targetTravelAngleZ);

            // lag actual travel angle behind mouse position. This prevents sharp turning
            travelAngle = Quaternion.RotateTowards(travelAngle, targetTravelAngle, turnSpeed * Time.deltaTime);
            
            // snake.transform.rotation = Quaternion.Euler(0, 0, 270 + travelAngle);
            snake.transform.rotation = travelAngle;
        }
    }

    public bool CheckForShoot() {
        if (!useController && Input.GetMouseButtonDown(1)) {
            return true;
        } else if (useController && !controllerShot && Input.GetAxis ("Fire") == 1) {
            controllerShot = true;
            return true;
        }

        if (useController && Input.GetAxis ("Fire") == 0) {
            controllerShot = false;
        }
        return false;
    }

    public void SetTravelAngle(Quaternion angle) {
        travelAngle = angle;
    }
}
