using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserController : MonoBehaviour
{
  [SerializeField] private LayerMask targetLayer;
  [SerializeField] private PlayerMotor myMotor;
  [SerializeField] private PlayerCharacter myCharacter;

  [SerializeField] private float speedIncrease = 0.5f; 
  private Platform activePlatform;
  private Transform myTransform;

  private void Start()
  {
    myTransform = transform;
    myCharacter.Setup();
    myCharacter.SubscribeToOnTriggerEnter(HandleTriggerEnter);
    SwipeDetector.OnSwipe += HandleSwipe;
  }
  
  private void OnDestroy()
  {
    SwipeDetector.OnSwipe -= HandleSwipe;
  }

  private void Update()
  {
    if(!Physics.Raycast(myTransform.position,-myTransform.up,1.5f,targetLayer,QueryTriggerInteraction.Collide))
      LevelManager.instance.GameOver();
  }


  private void HandleSwipe(SwipeData passedSwipeData)
  {
    if (passedSwipeData.Direction == SwipeDirection.Left)
    {
      myMotor.RotateAround(90);
    }

    if (passedSwipeData.Direction == SwipeDirection.Right)
    {
      myMotor.RotateAround(-90);
    }
  }
  
  private void HandleTriggerEnter(Transform other)
  {

    if (activePlatform != null)
      activePlatform.TurnOff();

    activePlatform=other.transform.root.GetComponent<Platform>();

    int verticalDot =Mathf.RoundToInt(Vector3.SignedAngle(activePlatform.transform.forward, myTransform.up,myTransform.up));
        
    if (verticalDot is 0 or 180)
    {
      LevelManager.instance.GameOver();
      return;
    }
    
    LevelManager.instance.AddToScore(1);
        
    int horizontalCross =Mathf.RoundToInt( Vector3.SignedAngle(activePlatform.transform.forward, myTransform.forward,myTransform.up)); 
    myMotor.IncreaseSpeed(speedIncrease);
    myMotor.RotateTurn(-horizontalCross);
    myMotor.LatchToTarget(activePlatform.transform);
    

  }
}
