using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterCatcher : MonoBehaviour
{
  [SerializeField] private LayerMask targetLayer; 
  private Action<Transform> OnTriggerCatch;

  private void OnTriggerEnter(Collider other)
  {
    if(((1<<other.gameObject.layer) & targetLayer) != 0)
    {
      OnTriggerCatch?.Invoke(other.transform);
    }
  }

  public void SubscribeToOnTriggerCatch(Action<Transform> passedAction)
  {
    OnTriggerCatch += passedAction;
  }
  
  private void OnDisable()
  {
    OnTriggerCatch = null;
  }
}
