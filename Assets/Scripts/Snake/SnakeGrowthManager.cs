using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeGrowthManager : MonoBehaviour
{

    public GameObject bodySegmentPrefab;
    public int maxMarkersPerSegment;

    // refs
    NuggetManager nuggetManager;
    ScoreManager scoreManager;


    // state
    List<GameObject> bodySegments = new List<GameObject>();
    float damageCooldownTimer = 0;
    float damageCooldown = 2f;

    float markerDropDistance = .5f;
    float distanceSinceLastMarker;

    float snakeMoveSpeed;

    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();

        gameObject.GetComponent<MarkerManager>().SetMaxMarkerCount(maxMarkersPerSegment);
        bodySegments.Add(gameObject);
        for (int i = 1; i <=5; i++) {
            IncreaseLength();
        }

        GameObject nuggetManagerObj = GameObject.FindGameObjectWithTag("Nugget Manager");
        nuggetManager =  nuggetManagerObj.GetComponent<NuggetManager>();

        distanceSinceLastMarker = 0;
    }

    void Update() {
        if (distanceSinceLastMarker >= markerDropDistance) {
            distanceSinceLastMarker = 0;
        }

        damageCooldownTimer += Time.deltaTime;
        

        // update body segment positions and rotations and Add new markers
        for (int i = 0; i < bodySegments.Count; i++) {
            if (i != 0) {
                MarkerManager nextMarkerManagerInLine = bodySegments[i-1].GetComponent<MarkerManager>();
                if (nextMarkerManagerInLine.GetMarkerCount() > 0) {
                    bodySegments[i].transform.position = Vector3.MoveTowards(bodySegments[i].transform.position, nextMarkerManagerInLine.GetLastMarkerInLine().position, snakeMoveSpeed * Time.deltaTime); ;
                    bodySegments[i].transform.rotation = nextMarkerManagerInLine.GetLastMarkerInLine().rotation;
                }
            }
            if (distanceSinceLastMarker == 0) {
                MarkerManager currentMarkerManager = bodySegments[i].GetComponent<MarkerManager>();
                currentMarkerManager.AddMarker();
            }
            
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
        Vector3 position = bodySegments[bodySegments.Count - 1].transform.position;
        GameObject newBodySegment = Instantiate(bodySegmentPrefab, position, Quaternion.identity, transform.parent);
        newBodySegment.GetComponent<MarkerManager>().SetMaxMarkerCount(maxMarkersPerSegment);
        bodySegments.Add(newBodySegment);

        // increase score
        scoreManager.IncreaseScore();
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
            scoreManager.DecreaseScore();
        }
        
    }


    public List<Vector3> DestroyAllBodySegments() {
        List<Vector3> positions = new List<Vector3>();;
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
