using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private List<Vector2> points = new List<Vector2>();
    private List<Vector2> localPoints = new List<Vector2>();

    [Header("Parameters")]
    [SerializeField] private float speed = 1f;
    [SerializeField] private float stopTime = 1f;
    private float stopCounter;

    private Vector2 currPoint;
    private Vector2 nextPoint;

    


    // Start is called before the first frame update
    void Start()
    {
        points.ForEach(p => localPoints.Add(new Vector2(transform.position.x + p.x, transform.position.y + p.y)));

        currPoint = transform.position;
        nextPoint = localPoints[0];


        stopCounter = stopTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position == nextPoint)
        {
            if(stopCounter > 0f)
            {
                stopCounter -= Time.deltaTime;
            }
            else
            {
                stopCounter = stopTime;

                currPoint = nextPoint;
                int nextIndex = localPoints.IndexOf(nextPoint) + 1;
                nextPoint = localPoints[nextIndex % localPoints.Count];
            }
   
        }
        else
        {
            currPoint = (Vector3)Vector2.MoveTowards(currPoint, nextPoint, speed * Time.deltaTime);
            transform.position = currPoint;
        }

        
    }
}
