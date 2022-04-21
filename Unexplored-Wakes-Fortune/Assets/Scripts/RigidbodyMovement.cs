using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    Rigidbody body;

    Vector3 PreviousPosition;
    Vector3 PositionDifference;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PreviousPosition = transform.localPosition;
        PositionDifference = transform.localPosition - PreviousPosition;
    }
}
