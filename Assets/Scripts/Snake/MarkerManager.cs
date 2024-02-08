using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{
   public class Marker {
        public Vector3 position;
        public Quaternion rotation;

        public Marker (Vector3 position, Quaternion rotation) {
            this.position = position;
            this.rotation = rotation;
        }
    }

    // public fields
    public bool showDebugTrail;
    public GameObject testMarker;

    // state
    List<Marker> markers = new List<Marker>();
    int maxMarkerCount = 0;
    
    List<GameObject> testMarkers = new List<GameObject>();

    public void AddMarker() {

            Marker newMarker = new Marker(transform.position, transform.rotation);
            markers.Add(newMarker);

            if (showDebugTrail) {
                testMarkers.Add(Instantiate(testMarker, newMarker.position, newMarker.rotation));
                //Debug.DrawLine(newMarker.position, newMarker.position);
            }


            // check if max marker count is exceeded. if yes, remove old ones
            if (markers.Count >= maxMarkerCount) {
                int numberToRemove = markers.Count - maxMarkerCount;
                markers.RemoveRange(0, numberToRemove);
                

                if (showDebugTrail) {
                    List<GameObject> testRange = testMarkers.GetRange(0, numberToRemove);
                    testRange.ForEach(testMarker => Destroy(testMarker));
                    testMarkers.RemoveRange(0, numberToRemove);
                }

            }
        //}
    }

    public Marker GetLastMarkerInLine() {
        return markers[0];
    }

    public int GetMarkerCount() {
        return markers.Count;
    }

    public void SetMaxMarkerCount(int max) {
        maxMarkerCount = max;
    }

    public Marker GetMarkerAt(int index) {
        return markers[index];
    }
}
