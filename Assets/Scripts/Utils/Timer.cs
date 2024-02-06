using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float frequency;
    float timer;


    public Timer(float frequency) {
        this.frequency = frequency;
        timer = 0;
    }

    // returns true if timer has reach frequency
    public void UpdateTimer(float deltaTime) {
        timer += deltaTime;
        if (timer >= frequency) {
            timer = 0;
        }
    }
}
