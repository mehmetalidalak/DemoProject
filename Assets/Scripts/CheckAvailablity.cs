using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// this script check whether user can put build or not.
/// </summary>
public class CheckAvailablity : MonoBehaviour
{
	[SerializeField] private ProductScriptable product;

	public bool canCheck = true;

	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}


	private void OnTriggerStay2D(Collider2D other)
	{
		if (canCheck)
		{
			if (other.CompareTag(TagManager.CELL_TAG))
			{
				return;
			}

			if (other.CompareTag(TagManager.PRODUCT_TAG) || other.CompareTag(TagManager.SOLDIER_TAG))
			{
				spriteRenderer.color = Color.red;
				ControllManager.instance.isAvailableToBuild = false;
				return;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag(TagManager.PRODUCT_TAG) || other.CompareTag(TagManager.SOLDIER_TAG))
		{
			spriteRenderer.color = Color.white;
			ControllManager.instance.isAvailableToBuild = true;
			return;
		}
	}

	/// <summary>
	/// When we click on any build object it set the information panel
	/// </summary>
	private void OnMouseDown()
	{
		if (product.productType == ProductType.Barrack)
			ControllManager.instance.activeBarrack = gameObject;

		MainCanvasController.instance.SetInformationPanel(product);
	}
}
