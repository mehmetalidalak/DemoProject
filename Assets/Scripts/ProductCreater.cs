using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProductCreater : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

	[SerializeField] ProductScriptable productScriptable;

	private GameObject product;

	private SpriteRenderer productSpriteRenderer;

	private CheckAvailablity productController;

	private Vector3 mousePos;

	private new Rigidbody2D rigidbody;


	public void OnBeginDrag(PointerEventData eventData)
	{
		MainCanvasController.instance.CloseInfoPanel();
		product = Instantiate(productScriptable.product);
		ControllManager.instance.activeObject = product;
		productSpriteRenderer = product.GetComponent<SpriteRenderer>();
		productController = productController = product.GetComponent<CheckAvailablity>();
		rigidbody = product.GetComponent<Rigidbody2D>();
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);	// Getting world position of mouse in scene.
		product.transform.position = new Vector3(mousePos.x, mousePos.y, 0);	// set first position of instantiated object to button position
		productSpriteRenderer.sortingOrder = 4;		// to make it highest priority as order to see.
		MainCanvasController.instance.DeActivateScrollView(false);

	}

	public void OnDrag(PointerEventData eventData)
	{
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float roundedValue = ControllManager.instance.RoundTo(mousePos.x, ValueManager.cellSize);
		mousePos.x = roundedValue;
		roundedValue = ControllManager.instance.RoundTo(mousePos.y, ValueManager.cellSize);
		mousePos.y = roundedValue;
		product.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (ControllManager.instance.isAvailableToBuild)	// if the current area is safe then set product the that area.
		{
			MainCanvasController.instance.DeActivateScrollView(true);
			productController.canCheck = false;
			ControllManager.instance.isAvailableToBuild = true;
			productSpriteRenderer.sortingOrder = 2;
			product.tag = TagManager.PRODUCT_TAG;

		}
		else // if area not safe to put product and when user release the touch/mouse button the object will destroy!
		{
			Destroy(product);
		}
		ControllManager.instance.activeObject = null;
	}
	
}
