using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLengthManager : MonoBehaviour
{
    TrailRenderer bodyRenderer;

    void Start()
    {
        bodyRenderer = transform.GetChild(0).gameObject.GetComponent<TrailRenderer>();
        bodyRenderer.time = .5f;
    }

    void Update() {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Nugget") {
            IncreaseLength();
            Destroy(other.gameObject);
        }
    }

    private void IncreaseLength()
    {
        bodyRenderer.time += 1;
    }

    public void SetBoostTime() {
        bodyRenderer.time /= 2;
    }

    public void SetNormalTime() {
        bodyRenderer.time *= 2;
    }
}
