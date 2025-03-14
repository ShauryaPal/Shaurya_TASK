using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Prop_Lever : MonoBehaviour
{
    [SerializeField] private UnityEvent OnLeverToggled;
    [SerializeField] private Animator leverAnimator;
    
    private const string PullLeftKey = "PullLeft";
    private const string PullRightKey = "PullRight";
    
    private float timeToWaitBeforeChangingLeverAgain;
    private bool isLeft = true;

    private void Update()
    {
        timeToWaitBeforeChangingLeverAgain -= Time.deltaTime;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Throwable") && timeToWaitBeforeChangingLeverAgain <= 0)
        {
            leverAnimator.Play(isLeft ? PullRightKey : PullLeftKey);
            isLeft = !isLeft;
            OnLeverToggled.Invoke();
            timeToWaitBeforeChangingLeverAgain = 1f;
        }
    }

}
