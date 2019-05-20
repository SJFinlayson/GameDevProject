using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    [RequireComponent(typeof(Interactable))]
    public class ScoopCollide : MonoBehaviour{
    public GameObject vScoop;
    public GameObject vTub;
    public GameObject sScoop;
    public GameObject sTub;
    public GameObject cScoop;
    public GameObject cTub;
    public Transform movingPart;

    public Vector3 localMoveDistance = new Vector3(0, -0.1f, 0);

    [Range(0, 1)]
    public float engageAtPercent = 0.95f;

    [Range(0, 1)]
    public float disengageAtPercent = 0.9f;

    public HandEvent onButtonDown;
    public HandEvent onButtonUp;
    public HandEvent onButtonIsPressed;

    public bool engaged = false;
    public bool buttonDown = false;
    public bool buttonUp = false;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Vector3 handEnteredPosition;

    private bool hovering;

    private Hand lastHoveredHand;
    public void OnCollisionExit(Collision col) {
        if (col.gameObject == vTub)
        {
            vScoop.SetActive(true);
            sScoop.SetActive(false);
            cScoop.SetActive(false);
        } else if (col.gameObject == sTub) {
            sScoop.SetActive(true);
            vScoop.SetActive(false);
            cScoop.SetActive(false);
        } else if (col.gameObject == cTub) {
            cScoop.SetActive(true);
            vScoop.SetActive(false);
            sScoop.SetActive(false);
        }
    }
    private void Start()
    {
        if (movingPart == null && this.transform.childCount > 0)
            movingPart = this.transform.GetChild(0);

        startPosition = movingPart.localPosition;
        endPosition = startPosition + localMoveDistance;
        handEnteredPosition = endPosition;
    }

    private void HandHoverUpdate(Hand hand)
    {
        hovering = true;
        lastHoveredHand = hand;

        bool wasEngaged = engaged;

        float currentDistance = Vector3.Distance(movingPart.parent.InverseTransformPoint(hand.transform.position), endPosition);
        float enteredDistance = Vector3.Distance(handEnteredPosition, endPosition);

        if (currentDistance > enteredDistance)
        {
            enteredDistance = currentDistance;
            handEnteredPosition = movingPart.parent.InverseTransformPoint(hand.transform.position);
        }

        float distanceDifference = enteredDistance - currentDistance;

        float lerp = Mathf.InverseLerp(0, localMoveDistance.magnitude, distanceDifference);

        if (lerp > engageAtPercent)
            engaged = true;
        else if (lerp < disengageAtPercent)
            engaged = false;

        movingPart.localPosition = Vector3.Lerp(startPosition, endPosition, lerp);

        InvokeEvents(wasEngaged, engaged);
    }

    private void LateUpdate()
    {
        if (hovering == false)
        {
            movingPart.localPosition = startPosition;
            handEnteredPosition = endPosition;

            InvokeEvents(engaged, false);
            engaged = false;
        }

        hovering = false;
    }

    private void InvokeEvents(bool wasEngaged, bool isEngaged)
    {
        buttonDown = wasEngaged == false && isEngaged == true;
        buttonUp = wasEngaged == true && isEngaged == false;

        if (buttonDown && onButtonDown != null)
            onButtonDown.Invoke(lastHoveredHand);
        if (buttonUp && onButtonUp != null)
            onButtonUp.Invoke(lastHoveredHand);
        if (isEngaged && onButtonIsPressed != null)
            onButtonIsPressed.Invoke(lastHoveredHand);
    }
}
}

