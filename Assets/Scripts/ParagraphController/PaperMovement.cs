using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PaperMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 activePosition;
    [SerializeField]
    private Vector2 inactivePosition;
    [SerializeField]
    private float transitionTime = 0.3f;

    private bool moving = false;
    private Vector2 velocity = Vector2.zero;
    private Vector2 actualTarget = Vector2.zero;


    public void setActivation(bool active)
    {
        if (moving) throw new Exception("The object is already in movement.");

        Vector2 targetPosition = active ? activePosition : inactivePosition;
        if (transform.position.Equals(targetPosition)) return;

        StartCoroutine(moveObject(targetPosition));
    }

    public bool isMoving()
    {
        return moving;
    }

    public float distanceFromTarget()
    {
        return Vector2.Distance(transform.position, actualTarget);
    }


    private IEnumerator moveObject(Vector2 targetPosition)
    {
        moving = true;
        actualTarget = targetPosition;

        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            Vector2 nextPosition = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, transitionTime);
            transform.position = nextPosition;
            yield return null;
        }

        transform.position = targetPosition;
        velocity = Vector2.zero;
        moving = false;
    }
}
