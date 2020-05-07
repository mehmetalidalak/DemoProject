using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Pathfinding;
public class SoldierMovement : MonoBehaviour
{
	private Seeker seeker;
	private AIPath aiPath;
	private SpriteRenderer spriteRenderer;
	private Vector2 ray;
	private RaycastHit2D hit;
	private Vector3 firstPos;
	private int mouseCount = 0;

	private void Start()
	{
		seeker = GetComponent<Seeker>();
		aiPath = GetComponent<AIPath>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
#if UNITY_EDITOR
		//// This function moves producableProduct to an safe area.
		if (Input.GetMouseButtonDown(1) && ControllManager.instance.moveProducableObject == gameObject)
		{
			ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			hit = Physics2D.Raycast(ray, Vector2.zero);

			if (hit.collider != null && !hit.collider.CompareTag(TagManager.PRODUCT_TAG))
			{
				Vector3 pos = hit.collider.gameObject.transform.position;
				pos.z = 0;
				seeker.StartPath(ControllManager.instance.moveProducableObject.transform.position, pos);

			}

			else //Unaccepted(Unsafe) Area Clicked!
			{
				ControllManager.instance.moveProducableObject = null;
			}

			if (aiPath.desiredVelocity.x < .001f)
			{
				spriteRenderer.color = Color.white;
				ControllManager.instance.moveProducableObject = null;
			}

		}
#else
		if (mouseCount > 0 && Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			hit = Physics2D.Raycast(ray, Vector2.zero);
			if (hit.collider != null && !(hit.collider.CompareTag(TagManager.PRODUCT_TAG) || hit.collider.CompareTag(TagManager.SOLDIER_TAG)))
			{
				Vector3 pos = hit.collider.gameObject.transform.position;
		seeker.StartPath(ControllManager.instance.moveProducableObject.transform.position, pos);

			}
			else
			{
				ControllManager.instance.moveProducableObject.transform.position = firstPos;
			}
			mouseCount = 0;
			spriteRenderer.color = Color.white;
		}
#endif
	}


	private void OnMouseUp()
	{
#if UNITY_EDITOR

		seeker.CancelCurrentPathRequest();
		ControllManager.instance.moveProducableObject = gameObject;
		MainCanvasController.instance.CloseInfoPanel();
		spriteRenderer.color = Color.red;
		AstarPath.active.Scan();    // update path area
#else
		MouseDownPhone();
#endif
	}
	void MouseDownPhone()
	{
		if (mouseCount == 0)
		{
			ControllManager.instance.moveProducableObject = gameObject;
			firstPos = ControllManager.instance.moveProducableObject.transform.position;
			mouseCount++;
			seeker.CancelCurrentPathRequest();
			MainCanvasController.instance.CloseInfoPanel();
			spriteRenderer.color = Color.red;
			AstarPath.active.Scan();    // update path area

		}
	}
}
