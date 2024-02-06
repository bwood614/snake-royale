using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour, IController
{
    float randomTimeUntilNextRotation;
    float randomTimeUntilNextBoost;
    float randomBoostDuration;
    float rotationLerpValue = 0;
    float currentZRotation;
    float targetZRotation;

    public void Initialize() {
        randomTimeUntilNextRotation = Random.Range(.5f, 1f);
        randomTimeUntilNextBoost = Random.Range(1.0f, 20f);
        randomBoostDuration = Random.Range(2, 10);
    }

    public void UpdateBoost(SnakeStateManager snake)
    {
        //randomTimeUntilNextBoost -= Time.deltaTime;
        if (randomTimeUntilNextBoost <= 0) {
            randomTimeUntilNextBoost = Random.Range(1.0f, 20f);
            snake.SwitchState(snake.snakeBoostState);
        }
        if (snake.currentState.Equals(snake.snakeBoostState)) {
            randomBoostDuration -= Time.deltaTime;
            if (randomBoostDuration <= 0) {
                snake.SwitchState(snake.snakeNormalState);
                randomBoostDuration = Random.Range(2, 10);
            }
        }
    }

    public void UpdateRotation(SnakeStateManager snake) {
        // count down timer until next rotation
        randomTimeUntilNextRotation -= Time.deltaTime;

        if (randomTimeUntilNextRotation <= 0) {
            // reset timer to noew random value
            randomTimeUntilNextRotation = Random.Range(1f, 2f);
            // set our current rotation and target rotation for lerp function
            rotationLerpValue = 0;
            currentZRotation = snake.transform.rotation.eulerAngles.z;
            targetZRotation = Random.Range(0, 359);
        }

        // perform lerped rotation
        if (rotationLerpValue <= 1) {
            rotationLerpValue += Time.deltaTime;
            snake.transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(currentZRotation, targetZRotation, rotationLerpValue));
        }
    }

    public void SetTravelAngle(Quaternion angle)
    {
        
    }

    public bool CheckForShoot() {
        return false;
    }
}
