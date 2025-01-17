using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacle : ObstacleBase
{
    [Header("Ref Only")]
    Vector3 currentPos;
    public Vector3 previousPos;
    bool alreadyUpdatingGrid = false;

    public event Action<GameObject> OnMovedEvent;

    private void Start()
    {
        currentPos = transform.position;
    }
    
    private void Update()
    {
        //Restricting movement to grid based movement
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));

        if(transform.position != currentPos && !alreadyUpdatingGrid)
        {
            alreadyUpdatingGrid = true;
            StartCoroutine(UpdateGrid());
        }
    }

    IEnumerator UpdateGrid()
    {
        yield return new WaitForSeconds(2f);

        previousPos = currentPos;
        currentPos = transform.position;

        //OnMovedEvent?.Invoke(gameObject);
        //StaticEvents.ReScanEventTest(gameObject);
        //WorldScanner.instance.ReScan(gameObject);
        StoppedMoving(gameObject);

        alreadyUpdatingGrid = false;
    }
}
