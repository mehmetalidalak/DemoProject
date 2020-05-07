using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
	private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

	private Vector2 firstTouchPrevPos, secondTouchPrevPos;

	[SerializeField]
	float zoomModifierSpeed = .1f;

	private bool isZoom = false;


	private void FixedUpdate()
	{
		CameraMovement();
	}

	void CameraMovement()
	{
		SwipeCamera();
#if UNITY_EDITOR
		zoomModifierSpeed = 10f;
		zoomModifier = Input.GetAxis("Mouse ScrollWheel") * zoomModifierSpeed;
		Camera.main.orthographicSize -= zoomModifier;
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2f, 4f);

#else
		if (Input.touchCount == 2)
		{
		isZoom = true;
			Touch firstTouch = Input.GetTouch(0);
			Touch secondTouch = Input.GetTouch(1);

			firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
			secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

			touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
			touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

			zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

			if (touchesPrevPosDifference > touchesCurPosDifference)
				Camera.main.orthographicSize += zoomModifier;
			if (touchesPrevPosDifference < touchesCurPosDifference)
				Camera.main.orthographicSize -= zoomModifier;

		}
		else
		isZoom = false;

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2f, 5f);
#endif
	}
	public void SwipeCamera()
	{
		if (!isZoom)
		{
			if (ControllManager.instance.activeObject == null && transform.position.x < ValueManager.cameraRightBorder && (SwipeManager.direction == Swipes.Left))
			{

				transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0.25f, 0, 0), .7f);

			}
			else if (ControllManager.instance.activeObject == null && transform.position.x > ValueManager.cameraLeftBorder && SwipeManager.direction == Swipes.Right)
			{

				transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0.25f, 0, 0), .7f);

			}
			else if (ControllManager.instance.activeObject == null && transform.position.y < ValueManager.cameraTopBorder && SwipeManager.direction == Swipes.Down)
			{

				transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 0.25f, 0), .7f);

			}
			else if (ControllManager.instance.activeObject == null && transform.position.y > ValueManager.cameraBottomBorder && SwipeManager.direction == Swipes.Up)
			{

				transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0, 0.25f, 0), .7f);

			}
		}

	}
}
