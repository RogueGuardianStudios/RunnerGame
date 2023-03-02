using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
   [SerializeField] private bool Fire = false;
   
   
   [SerializeField] private bool canMove = false;
   [SerializeField] private float rotationOffset = 1;
   private float speed = 2.5f;
   private Transform myTransform;
   private Vector3 targetPOS;
   private Quaternion targetRotaion;
   private void Awake()
   {
      myTransform = transform;
      targetPOS = myTransform.position;
      targetRotaion = transform.rotation;
   }

   private void Update()
   {
      
      if(!canMove)
         return;
      
      Vector3 movementDIR = myTransform.forward * speed * Time.deltaTime;
      if (targetPOS != Vector3.zero)
      {
         Vector3 offset  = Vector3.zero;
         float x = Mathf.Lerp(0, targetPOS.x,   10*Time.deltaTime);
         targetPOS.x -= x;
         offset.x = x;
         myTransform.position += myTransform.rotation * offset;
         
      }
      myTransform.position += movementDIR;
   }

   public void SetCanMove(bool passedCanMove)
   {
      canMove = passedCanMove;
   }

   public void IncreaseSpeed(float amount)
   {
      speed += amount;
   }

   public void RotateAround(float angle)
   {
      Quaternion rot = Quaternion.AngleAxis (angle, myTransform.forward);
      myTransform.position -= myTransform.up;
      myTransform.rotation = rot *  myTransform.rotation;
      myTransform.position += myTransform.up;
     
   }

   public void RotateTurn(float angle)
   {
      Quaternion rot = Quaternion.AngleAxis (angle, myTransform.up);
      myTransform.rotation  = rot * myTransform.rotation;
   }

   public void LatchToTarget(Transform other)
   {
      targetPOS = myTransform.InverseTransformPoint(other.position);
      targetPOS.y = 0;
      targetPOS.z = 0;
      
   }
}
