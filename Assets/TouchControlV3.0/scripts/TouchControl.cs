//	Touch Control V3.0 (Unity & Unity Pro)
//	Developer: Ananda Gupta
//	CopyrightÂ©2015, Ananda GUpta
//	http://agmm.in
//	info@agmm.in
using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour
{	
	// public variables
	
	public float VSwipeZone = 50;
	public float HSwipeZone = 50;
	public float minSwipeDistance = 20;
	public float maxSwipeTime = 2;
	public float doubleTapTime = 0.2f;
	public float longTapTime = 0.5f;
	public bool OneFingerTap;
	public bool OneFingerSwipe;
	public bool TwoFingersTap;
	public bool TwoFingersSwipe;
	public bool MultiFingersTap;
	public bool PinchZoom;
	public Camera MainCamera;
	public float orthoCamZoomMin = 2.0f;
	public float orthoCamZoomMax = 8.0f;
	public float orthoZoomSpeed = 0.02f;
	public float persCamZoomMin = 40.0f;
	public float persCamZoomMax = 80.0f;
	public float persZoomSpeed = 0.2f;        
	
	//private variables

	private float startTime;
	private Vector2 touchPos;
	private bool tapping;
	private bool twoFingersTapping;
	private bool multiFingersTapping;
	private float lastTap;
	private bool multiTouch;
	private bool singleTouch;
	
	//public static variables

	public static bool singleFingerUpSwipe = false;
	public static bool singleFingerDownSwipe = false;
	public static bool singleFingerLeftSwipe = false;
	public static bool singleFingerRightSwipe = false;
	public static bool singleFingerTap = false;
	public static bool singleFingerLogTap = false;
	public static bool singleFingerDoubleTap = false;
	public static bool doubleFingersUpSwipe = false;
	public static bool doubleFingersDownSwipe = false;
	public static bool doubleFingersLeftSwipe = false;
	public static bool doubleFingersRightSwipe = false;
	public static bool doubleFingersTap = false;
	public static bool doubleFingersDoubleTap = false;
	public static bool doubleFingersLogTap = false;
	public static bool multiFingersTap = false;

	// Use this for initialization

	void Start ()
	{
		multiTouch = false;
		singleTouch = true;
	}
	
	// Update is called once per frame

	void Update ()
	{
		if (multiTouch) {
			if (Input.touchCount == 0) {
				singleTouch = true;
				multiTouch = false;
			} else {
				
			}
		} else if (singleTouch) {
#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
				Touch touch = new Touch();

				touch.position = Input.mousePosition;
				if (Input.GetMouseButtonDown(0) )
				{
					touch.phase = TouchPhase.Began;
				}
				else
				{
					touch.phase = TouchPhase.Ended;
				}

#else
			if (Input.touchCount == 1) {
				Touch touch = Input.touches [0];
#endif
				switch (touch.phase) {
				case TouchPhase.Began:
					touchPos = touch.position;
					startTime = Time.time;
					break;
				case TouchPhase.Ended:
					float swipeTime = Time.time - startTime;
					float swipeDist = (touch.position - touchPos).magnitude;

					if (((Mathf.Abs (touch.position.x - touchPos.x)) < VSwipeZone) && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && (Mathf.Sign (touch.position.y - touchPos.y) > 0) && (Mathf.Sign (touch.position.x - touchPos.x) < 2.5f) && (Mathf.Sign (touch.position.x - touchPos.x) > -2.5f && (OneFingerSwipe == true))) {
												
						print ("Single Finger Up Swipe");

						singleFingerUpSwipe = true;
						singleFingerDownSwipe = false;
						singleFingerLeftSwipe = false;
						singleFingerRightSwipe = false;
						singleFingerTap = false;
						singleFingerLogTap = false;
						singleFingerDoubleTap = false;
						
						doubleFingersUpSwipe = false;
						doubleFingersDownSwipe = false;
						doubleFingersLeftSwipe = false;
						doubleFingersRightSwipe = false;
						doubleFingersTap = false;
						doubleFingersDoubleTap = false;
						doubleFingersLogTap = false;
						
						multiFingersTap = false;
												
					} else if ((Mathf.Abs (touch.position.x - touchPos.x)) < VSwipeZone && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touch.position.y - touchPos.y) < 0 && Mathf.Sign (touch.position.x - touchPos.x) < 2.5f && Mathf.Sign (touch.position.x - touchPos.x) > -2.5f && (OneFingerSwipe == true)) {
												
						print ("Single Finger Down Swipe");

						singleFingerUpSwipe = false;
						singleFingerDownSwipe = true;
						singleFingerLeftSwipe = false;
						singleFingerRightSwipe = false;
						singleFingerTap = false;
						singleFingerLogTap = false;
						singleFingerDoubleTap = false;
						
						doubleFingersUpSwipe = false;
						doubleFingersDownSwipe = false;
						doubleFingersLeftSwipe = false;
						doubleFingersRightSwipe = false;
						doubleFingersTap = false;
						doubleFingersDoubleTap = false;
						doubleFingersLogTap = false;
						
						multiFingersTap = false;
												
					} else if ((Mathf.Abs (touch.position.y - touchPos.y)) < HSwipeZone && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touch.position.x - touchPos.x) < 0 && Mathf.Sign (touch.position.y - touchPos.y) < 2.5f && Mathf.Sign (touch.position.y - touchPos.y) > -2.5f && (OneFingerSwipe == true)) {
												
						print ("Single Finger Left Swipe");

						singleFingerUpSwipe = false;
						singleFingerDownSwipe = false;
						singleFingerLeftSwipe = true;
						singleFingerRightSwipe = false;
						singleFingerTap = false;
						singleFingerLogTap = false;
						singleFingerDoubleTap = false;
						
						doubleFingersUpSwipe = false;
						doubleFingersDownSwipe = false;
						doubleFingersLeftSwipe = false;
						doubleFingersRightSwipe = false;
						doubleFingersTap = false;
						doubleFingersDoubleTap = false;
						doubleFingersLogTap = false;
						
						multiFingersTap = false;
												
					} else if ((Mathf.Abs (touch.position.y - touchPos.y)) < HSwipeZone && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touch.position.x - touchPos.x) > 0 && Mathf.Sign (touch.position.y - touchPos.y) < 2.5f && Mathf.Sign (touch.position.y - touchPos.y) > -2.5f && (OneFingerSwipe == true)) {
												
						print ("Single Finger Right Swipe");

						singleFingerUpSwipe = false;
						singleFingerDownSwipe = false;
						singleFingerLeftSwipe = false;
						singleFingerRightSwipe = true;
						singleFingerTap = false;
						singleFingerLogTap = false;
						singleFingerDoubleTap = false;
						
						doubleFingersUpSwipe = false;
						doubleFingersDownSwipe = false;
						doubleFingersLeftSwipe = false;
						doubleFingersRightSwipe = false;
						doubleFingersTap = false;
						doubleFingersDoubleTap = false;
						doubleFingersLogTap = false;
						
						multiFingersTap = false;												

					} else if ((swipeTime > longTapTime) && (OneFingerTap == true)) {
												
						print ("Single Finger Long Tap");

						singleFingerUpSwipe = false;
						singleFingerDownSwipe = false;
						singleFingerLeftSwipe = false;
						singleFingerRightSwipe = false;
						singleFingerTap = false;
						singleFingerLogTap = true;
						singleFingerDoubleTap = false;
						
						doubleFingersUpSwipe = false;
						doubleFingersDownSwipe = false;
						doubleFingersLeftSwipe = false;
						doubleFingersRightSwipe = false;
						doubleFingersTap = false;
						doubleFingersDoubleTap = false;
						doubleFingersLogTap = false;
						
						multiFingersTap = false;

						tapping = false;

					} else if (!tapping) {
						tapping = true;
						StartCoroutine (SingleTap ());
					}

					if (((Time.time - lastTap) < doubleTapTime) && (OneFingerTap == true)) {

						print ("Single Finger Double Tap");

						singleFingerUpSwipe = false;
						singleFingerDownSwipe = false;
						singleFingerLeftSwipe = false;
						singleFingerRightSwipe = false;
						singleFingerTap = false;
						singleFingerLogTap = false;
						singleFingerDoubleTap = true;
						
						doubleFingersUpSwipe = false;
						doubleFingersDownSwipe = false;
						doubleFingersLeftSwipe = false;
						doubleFingersRightSwipe = false;
						doubleFingersTap = false;
						doubleFingersDoubleTap = false;
						doubleFingersLogTap = false;
						
						multiFingersTap = false;

						tapping = false;
					}
					     
					lastTap = Time.time;
					break;
				}
			}
		}

		if (Input.touchCount == 2) {
			multiTouch = true;
			singleTouch = false;

			if ((PinchZoom) && (!TwoFingersTap) && (!TwoFingersSwipe)) {

				Touch touchZero = Input.GetTouch (0);
				Touch touchOne = Input.GetTouch (1);

				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;				

				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				if (MainCamera.orthographic) {

					MainCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

					MainCamera.orthographicSize = Mathf.Clamp (MainCamera.orthographicSize, orthoCamZoomMin, orthoCamZoomMax);
				
				} else {

					MainCamera.fieldOfView += deltaMagnitudeDiff * persZoomSpeed;

					MainCamera.fieldOfView = Mathf.Clamp (MainCamera.fieldOfView, persCamZoomMin, persCamZoomMax);
				}
			}
		
			Touch touch = Input.touches [0];

			switch (touch.phase) {

			case TouchPhase.Began:
				touchPos = touch.position;
				startTime = Time.time;
				break;

			case TouchPhase.Ended:
				float swipeTime = Time.time - startTime;
				float swipeDist = (touch.position - touchPos).magnitude;								

				if ((Mathf.Abs (touch.position.x - touchPos.x)) < VSwipeZone && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touch.position.y - touchPos.y) > 0 && Mathf.Sign (touch.position.x - touchPos.x) < 2.5f && Mathf.Sign (touch.position.x - touchPos.x) > -2.5f && (TwoFingersSwipe == true)) {
										
					print ("Double Fingers Up Swipe");

					singleFingerUpSwipe = false;
					singleFingerDownSwipe = false;
					singleFingerLeftSwipe = false;
					singleFingerRightSwipe = false;
					singleFingerTap = false;
					singleFingerLogTap = false;
					singleFingerDoubleTap = false;
					
					doubleFingersUpSwipe = true;
					doubleFingersDownSwipe = false;
					doubleFingersLeftSwipe = false;
					doubleFingersRightSwipe = false;
					doubleFingersTap = false;
					doubleFingersDoubleTap = false;
					doubleFingersLogTap = false;
					
					multiFingersTap = false;
										
				} else if ((Mathf.Abs (touch.position.x - touchPos.x)) < VSwipeZone && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touch.position.y - touchPos.y) < 0 && Mathf.Sign (touch.position.x - touchPos.x) < 2.5f && Mathf.Sign (touch.position.x - touchPos.x) > -2.5f && (TwoFingersSwipe == true)) {
										
					print ("Double Fingers Down Swipe");

					singleFingerUpSwipe = false;
					singleFingerDownSwipe = false;
					singleFingerLeftSwipe = false;
					singleFingerRightSwipe = false;
					singleFingerTap = false;
					singleFingerLogTap = false;
					singleFingerDoubleTap = false;
					
					doubleFingersUpSwipe = false;
					doubleFingersDownSwipe = true;
					doubleFingersLeftSwipe = false;
					doubleFingersRightSwipe = false;
					doubleFingersTap = false;
					doubleFingersDoubleTap = false;
					doubleFingersLogTap = false;
					
					multiFingersTap = false;

				} else if ((Mathf.Abs (touch.position.y - touchPos.y)) < HSwipeZone && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touch.position.x - touchPos.x) < 0 && Mathf.Sign (touch.position.y - touchPos.y) < 2.5f && Mathf.Sign (touch.position.y - touchPos.y) > -2.5f && (TwoFingersSwipe == true)) {
										
					print ("Double Fingers Left Swipe");

					singleFingerUpSwipe = false;
					singleFingerDownSwipe = false;
					singleFingerLeftSwipe = false;
					singleFingerRightSwipe = false;
					singleFingerTap = false;
					singleFingerLogTap = false;
					singleFingerDoubleTap = false;
					
					doubleFingersUpSwipe = false;
					doubleFingersDownSwipe = false;
					doubleFingersLeftSwipe = true;
					doubleFingersRightSwipe = false;
					doubleFingersTap = false;
					doubleFingersDoubleTap = false;
					doubleFingersLogTap = false;
					
					multiFingersTap = false;

				} else if ((Mathf.Abs (touch.position.y - touchPos.y)) < HSwipeZone && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign (touch.position.x - touchPos.x) > 0 && Mathf.Sign (touch.position.y - touchPos.y) < 2.5f && Mathf.Sign (touch.position.y - touchPos.y) > -2.5f && (TwoFingersSwipe == true)) {
										
					print ("Double Fingers Right Swipe");

					singleFingerUpSwipe = false;
					singleFingerDownSwipe = false;
					singleFingerLeftSwipe = false;
					singleFingerRightSwipe = false;
					singleFingerTap = false;
					singleFingerLogTap = false;
					singleFingerDoubleTap = false;
					
					doubleFingersUpSwipe = false;
					doubleFingersDownSwipe = false;
					doubleFingersLeftSwipe = false;
					doubleFingersRightSwipe = true;
					doubleFingersTap = false;
					doubleFingersDoubleTap = false;
					doubleFingersLogTap = false;
					
					multiFingersTap = false;

				} else if ((swipeTime > longTapTime) && (TwoFingersTap == true)) {
					print ("Double Fingers Long Tap");
										
					singleFingerUpSwipe = false;
					singleFingerDownSwipe = false;
					singleFingerLeftSwipe = false;
					singleFingerRightSwipe = false;
					singleFingerTap = false;
					singleFingerLogTap = false;
					singleFingerDoubleTap = false;
					
					doubleFingersUpSwipe = false;
					doubleFingersDownSwipe = false;
					doubleFingersLeftSwipe = false;
					doubleFingersRightSwipe = false;
					doubleFingersTap = false;
					doubleFingersDoubleTap = false;
					doubleFingersLogTap = true;
					
					multiFingersTap = false;

					tapping = false;
					twoFingersTapping = false;
				
				} else if (!twoFingersTapping) {
					twoFingersTapping = true;
					StartCoroutine (twoFingersTap ());
				}

				if (((Time.time - lastTap) < doubleTapTime) && (OneFingerTap == true)) {

					print ("Double Fingers Double Tap");
					
					singleFingerUpSwipe = false;
					singleFingerDownSwipe = false;
					singleFingerLeftSwipe = false;
					singleFingerRightSwipe = false;
					singleFingerTap = false;
					singleFingerLogTap = false;
					singleFingerDoubleTap = false;
					
					doubleFingersUpSwipe = false;
					doubleFingersDownSwipe = false;
					doubleFingersLeftSwipe = false;
					doubleFingersRightSwipe = false;
					doubleFingersTap = false;
					doubleFingersDoubleTap = true;
					doubleFingersLogTap = false;
					
					multiFingersTap = false;

					tapping = false;
					twoFingersTapping = false;
				}

				lastTap = Time.time;
				break;
			}
		}
		if ((Input.touchCount > 2) && (MultiFingersTap == true)) {
			multiTouch = true;
			singleTouch = false;
			Touch touch = Input.touches [0];
			switch (touch.phase) {
			case TouchPhase.Ended:
								
				print ("Multi Fingers Tap");

				singleFingerUpSwipe = false;
				singleFingerDownSwipe = false;
				singleFingerLeftSwipe = false;
				singleFingerRightSwipe = false;
				singleFingerTap = false;
				singleFingerLogTap = false;
				singleFingerDoubleTap = false;
				
				doubleFingersUpSwipe = false;
				doubleFingersDownSwipe = false;
				doubleFingersLeftSwipe = false;
				doubleFingersRightSwipe = false;
				doubleFingersTap = false;
				doubleFingersDoubleTap = false;
				doubleFingersLogTap = false;
				
				multiFingersTap = true;

				break;
			}
		}
	}

	IEnumerator SingleTap ()
	{
		yield return new WaitForSeconds (doubleTapTime);
		if ((tapping) && (OneFingerTap == true)) {
						
			print ("Single Finger Tap");

			singleFingerUpSwipe = false;
			singleFingerDownSwipe = false;
			singleFingerLeftSwipe = false;
			singleFingerRightSwipe = false;
			singleFingerTap = true;
			singleFingerLogTap = false;
			singleFingerDoubleTap = false;
			
			doubleFingersUpSwipe = false;
			doubleFingersDownSwipe = false;
			doubleFingersLeftSwipe = false;
			doubleFingersRightSwipe = false;
			doubleFingersTap = false;
			doubleFingersDoubleTap = false;
			doubleFingersLogTap = false;
			
			multiFingersTap = false;

			tapping = false;
		}
	}

	IEnumerator twoFingersTap ()
	{
		yield return new WaitForSeconds (doubleTapTime);
		if ((twoFingersTapping) && (TwoFingersTap == true)) {
						
			print ("Double Fingers Tap");

			singleFingerUpSwipe = false;
			singleFingerDownSwipe = false;
			singleFingerLeftSwipe = false;
			singleFingerRightSwipe = false;
			singleFingerTap = false;
			singleFingerLogTap = false;
			singleFingerDoubleTap = false;
			
			doubleFingersUpSwipe = false;
			doubleFingersDownSwipe = false;
			doubleFingersLeftSwipe = false;
			doubleFingersRightSwipe = false;
			doubleFingersTap = true;
			doubleFingersDoubleTap = false;
			doubleFingersLogTap = false;
			
			multiFingersTap = false;

			twoFingersTapping = false;
		}
	}
}
