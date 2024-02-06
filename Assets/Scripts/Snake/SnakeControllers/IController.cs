using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    void Initialize();
    void UpdateRotation(SnakeStateManager snake);
    void UpdateBoost(SnakeStateManager snake);

    void SetTravelAngle(Quaternion angle);
    bool CheckForShoot();
}
