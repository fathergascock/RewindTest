using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    public bool isRewinding = false;
    public float maxRewindTime = 5f;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime >();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.E))
            StopRewind();
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    private void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }

        else
        {
            StopRewind();
        }
        
    }

    private void Record()
    {
        if(pointsInTime.Count > Mathf.Round(maxRewindTime * (1f / Time.fixedDeltaTime)))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StopRewind()
    { 
        isRewinding = false;
        rb.isKinematic = false;
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }
}
