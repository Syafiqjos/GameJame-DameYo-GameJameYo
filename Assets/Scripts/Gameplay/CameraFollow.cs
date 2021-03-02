using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform trainHead;
    public float followRatio;

    private void Update()
    {
        FollowTrainHead();
    }
    
    private void FollowTrainHead()
    {
        Vector3 pos = Vector3.Lerp(transform.position, trainHead.position, followRatio);
        pos.z = -10;

        transform.position = pos;
    }
}
