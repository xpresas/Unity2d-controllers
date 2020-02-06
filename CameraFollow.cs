using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float dampTime = 0.15f;
    public Transform target;
    public Vector3 minPosition;
    public Vector3 maxPosition;


    // Update is called once per frame
    void Update()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPos = target.position;

            targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
            targetPos.z = minPosition.z;
            transform.position = Vector3.Lerp(transform.position, targetPos, dampTime);
        }

    }
}