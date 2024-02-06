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
    int maxMarkerCount;
    
    List<GameObject> testMarkers = new List<GameObject>();

    public void AddMarker() {

            Marker newMarker = new Marker(transform.position, transform.rotation);
            markers.Add(newMarker);

            #region debug
            // if (showDebugTrail) {
            //     testMarkers.Add(Instantiate(testMarker, newMarker.position, newMarker.rotation));
            // }
            #endregion

            // check if max marker count is exceeded. if yes, remove old ones
            if (markers.Count >= maxMarkerCount) {
                int numberToRemove = markers.Count - maxMarkerCount;
                markers.RemoveRange(0, numberToRemove);
                
                #region debug
                // if (showDebugTrail) {
                //     List<GameObject> testRange = testMarkers.GetRange(0, numberToRemove);
                //     testRange.ForEach(testMarker => Destroy(testMarker));
                //     testMarkers.RemoveRange(0, numberToRemove);
                // }
                #endregion
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
}
