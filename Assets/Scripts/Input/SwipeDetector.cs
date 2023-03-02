using System;
using UnityEngine;


public struct SwipeData
{
   public Vector2 StartPosition;
   public Vector2 EndPosition;
   public SwipeDirection Direction;
}

public enum SwipeDirection
{
   Up,
   Down,
   Left,
   Right
}

public class SwipeDetector : MonoBehaviour
{
   private Vector2 fingerDownPosition;
   private Vector2 fingerUpPostion;
   [SerializeField] private bool detectSwipeOnlyAfterRelease = false;

   [SerializeField] private float minDistanceForSwipe = 20f;
   
   public static event  Action<SwipeData> OnSwipe = delegate {  };

   private void Awake()
   {
      DontDestroyOnLoad(gameObject);
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         fingerUpPostion = Input.mousePosition;
         fingerDownPosition = Input.mousePosition;
      }

      if (!detectSwipeOnlyAfterRelease && Input.GetMouseButton(0))
      {
         fingerDownPosition = Input.mousePosition;
         DetectSwipe();
      }

      if (Input.GetMouseButtonUp(0))
      {
         fingerDownPosition = Input.mousePosition;
         DetectSwipe();
      }

      foreach (Touch touch in Input.touches)
      {
         if (touch.phase == TouchPhase.Began)
         {
            fingerUpPostion = touch.position;
            fingerDownPosition = touch.position;
         }

         if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
         {
            fingerDownPosition = touch.position;
            DetectSwipe();
         }

         if (touch.phase == TouchPhase.Ended)
         {
            fingerDownPosition = touch.position;
            DetectSwipe();
         }

      }

   }

   private void DetectSwipe()
      {
         if (SwipeDistanceCheckMet())
         {
            if (IsVerticalSwipe())
            {
               var direction = fingerDownPosition.y - fingerUpPostion.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
               SendSwipe(direction);
            }
            else
            {
               var direction = fingerDownPosition.x - fingerUpPostion.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
               SendSwipe(direction);
            }

            fingerUpPostion = fingerDownPosition;


         }
      }

      private bool SwipeDistanceCheckMet()
      {
         return VerticalMovementDistance() > minDistanceForSwipe ||  HorizontalMovementDistance() > minDistanceForSwipe;
      }

      private float HorizontalMovementDistance()
      {
         return Mathf.Abs(fingerDownPosition.x - fingerUpPostion.x);
      }

      private float VerticalMovementDistance()
      {
         return Mathf.Abs(fingerDownPosition.y - fingerUpPostion.y);
      }

      private bool IsVerticalSwipe()
      {
         return VerticalMovementDistance() > HorizontalMovementDistance();
      }

      private void SendSwipe(SwipeDirection direction)
      {
         SwipeData swipeData = new SwipeData()
         {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPostion
         };
      
         Debug.Log("Swipe Sent : "+swipeData.Direction);
      
         OnSwipe(swipeData);
      }
   
   
}
