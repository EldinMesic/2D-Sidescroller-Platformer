using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera currCamera;
    public Transform followTarget;


    Vector2 startingPosition;
    float startingZ;


    Vector2 cameraMoveFromStart => (Vector2)currCamera.transform.position - startingPosition;

    float distanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (currCamera.transform.position.z + (distanceFromTarget > 0 ? currCamera.nearClipPlane : currCamera.farClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + cameraMoveFromStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
