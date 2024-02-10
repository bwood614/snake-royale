using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeGrowthManager : MonoBehaviour
{

    public GameObject bodySegmentPrefab;
    public int markersPerSegment = 5;
    public float markerDropDistance = 0.5f; // this affects how often path markers are dropped. lower vals make smoother movement

    // refs
    NuggetManager nuggetManager;
    //ScoreManager scoreManager;
    MarkerManager snakeMarkerManager;

    // state
    List<GameObject> bodySegments = new List<GameObject>();
    float damageCooldownTimer = 0;
    float damageCooldown = 2f;

    float distanceSinceLastMarker;


    float snakeMoveSpeed;

    void Start()
    {
        //scoreManager = GetComponent<ScoreManager>();

        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        nuggetManager = nuggetManagerObj.GetComponent<NuggetManager>();

        snakeMarkerManager = gameObject.GetComponent<MarkerManager>();
        snakeMarkerManager.SetMaxMarkerCount(markersPerSegment);

        for (int i = 1; i <= 5; i++) {
            IncreaseLength();
        }

        distanceSinceLastMarker = 0;
    }

    void Update() {
        if (distanceSinceLastMarker >= markerDropDistance) {
            snakeMarkerManager.AddMarker();
            distanceSinceLastMarker = 0;

        }

        damageCooldownTimer += Time.deltaTime;


        // update body segment positions and rotations
        for (int i = 0; i < bodySegments.Count; i++) {
            int markerCount = snakeMarkerManager.GetMarkerCount();
            if (markerCount <= 0) {
                break;
            }

            int nextMarkerIndex = markerCount - ((i + 1) * markersPerSegment);
            // if the length increases in rapid succession, it is possible to get negative index. Set index to 0 if negative
            nextMarkerIndex = nextMarkerIndex < 0 ? 0 : nextMarkerIndex;
            MarkerManager.Marker nextMarker = snakeMarkerManager.GetMarkerAt(nextMarkerIndex);

            Vector3 currentPostion = bodySegments[i].transform.position;
            Vector3 nextPosition = nextMarker.position;

            Quaternion currentRotation = bodySegments[i].transform.rotation;
            Quaternion nextRotation = nextMarker.rotation;


            // update transform position
            bodySegments[i].transform.position = Vector3.MoveTowards(currentPostion, nextPosition, snakeMoveSpeed * Time.deltaTime);

            //update transform rotation
            // distance between 2 angles = 180 - abs(abs(a1 - a2) - 180); 
            float angleDifference = 180 - Mathf.Abs(Mathf.Abs(nextRotation.eulerAngles.z - currentRotation.eulerAngles.z) - 180);
            float maxDegreesDelta = angleDifference * snakeMoveSpeed * Time.deltaTime / (markerDropDistance - distanceSinceLastMarker);
            bodySegments[i].transform.rotation = Quaternion.RotateTowards(currentRotation, nextRotation, maxDegreesDelta);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Nugget") {
            IncreaseLength();
            nuggetManager.DestroyNugget(other.gameObject);
        }
    }

    private void IncreaseLength()
    {
        Vector3 position;
        if (snakeMarkerManager.GetMarkerCount() > 0) {
            position = snakeMarkerManager.GetLastMarkerInLine().position;
        }
        else {
            position = transform.position;
        }
        GameObject newBodySegment = Instantiate(bodySegmentPrefab, position, Quaternion.identity, transform.parent);
        bodySegments.Add(newBodySegment);
        snakeMarkerManager.SetMaxMarkerCount((bodySegments.Count + 1) * markersPerSegment);


        newBodySegment.GetComponent<SpriteRenderer>().sortingOrder = -1 * (bodySegments.Count - 1);

        // increase score
        //scoreManager.IncreaseScore();
    }

    public void DescreaseLength() {
        if (damageCooldownTimer >= damageCooldown) {
            damageCooldownTimer = 0;

            GameObject segmentToDestroy = bodySegments[bodySegments.Count - 1];
            bodySegments.RemoveAt(bodySegments.Count - 1);
            Destroy(segmentToDestroy);

            // Add nugget at last position
            nuggetManager.AddNuggetAtPosition(GetLastBodySegmentPosition());

            // decreaseScore
            //scoreManager.DecreaseScore();
        }

    }


    public List<Vector3> DestroyAllBodySegments() {
        List<Vector3> positions = new List<Vector3>();
        for (int i = bodySegments.Count - 1; i >= 0; i--) {
            positions.Add(bodySegments[i].transform.position);
            Destroy(bodySegments[i]);
            bodySegments.RemoveAt(i);
        }
        return positions;
    }
    public Vector3 GetLastBodySegmentPosition() {
        return bodySegments[bodySegments.Count - 1].transform.position;
    }

    public void AddDistanceTraveled(float distance) {
        distanceSinceLastMarker += distance;
    }

    public void SetMoveSpeed(float moveSpeed) {
        snakeMoveSpeed = moveSpeed;
    }
}
