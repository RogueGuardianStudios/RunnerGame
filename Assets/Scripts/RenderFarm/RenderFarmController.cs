using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderFarmController : MonoBehaviour
{
    [SerializeField]private Transform modelAnchor;
    [SerializeField]private Vector3 anchorOffset;

    private GameObject activeTarget;
    
    private void Awake()
    {
        modelAnchor.SetParent(null);
        transform.LookAt(modelAnchor);
        modelAnchor.position = transform.position + anchorOffset;
       
    }

    public void SetActiveTarget(GameObject passedGameObject)
    {
        if (passedGameObject == null)
        {
            Debug.LogError("Null Object Passed!!");
            return;
        }
        
        if(activeTarget != null)
            Destroy(activeTarget);

        activeTarget = Instantiate(passedGameObject, modelAnchor);

    }
}
