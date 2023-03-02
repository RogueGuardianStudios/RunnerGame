using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Transform modelAnchor;
    private GameObject playerModel;
   

    public void Setup()
    {
        playerModel= Instantiate(DataManager.instance.GetCharacter(), modelAnchor);
    }

    public void SubscribeToOnTriggerEnter(Action<Transform> passedAction)
    {
        playerModel.GetComponent<OnTriggerEnterCatcher>().SubscribeToOnTriggerCatch(passedAction);
    }
}
