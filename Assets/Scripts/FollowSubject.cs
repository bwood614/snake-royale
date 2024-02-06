using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSubject : MonoBehaviour
{
    public GameObject subject;
    void LateUpdate()
    {
        transform.position = new Vector3(subject.transform.position.x, subject.transform.position.y, transform.position.z);
    }
}
