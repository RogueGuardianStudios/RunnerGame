using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] private Transform socket;

    public Vector3 GetSocketPosition()
    {
        return socket.position;
    }
    
    public void TurnOff()
    {
        LevelManager.instance.SpawnPlatform(1);
       Destroy(gameObject,0.5f);
    }
}
