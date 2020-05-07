using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasController : Singleton<MainCanvasController>
{

	#region MenuPanels
	[SerializeField] private GameObject informationPanel;
	[SerializeField] private GameObject productMenuPanel;
	#endregion MenuPanels

	#region Fields
	[SerializeField] private ScrollRect productScrollRect;
	[SerializeField] private ScrollRect informationScrollRect;
	[SerializeField] private GameObject informationContent;
	[SerializeField] private Text productNameText;
	[SerializeField] private Text productDimensionText;
	[SerializeField] private Image productImage;
	#endregion Fields



	public void DeActivateScrollView(bool isActive)
	{
		productScrollRect.enabled = isActive;
	}

	public void SetInformationPanel(ProductScriptable product)
	{
		CloseInfoPanel();
		informationPanel.SetActive(true);
		productNameText.text = product.productName;
		productDimensionText.text = product.productDimension + " cells.";
		productImage.sprite = product.productSprite;

		switch (product.productType)	// product type is the type of builds, soldiers etc.
		{
			case ProductType.Soldier:
				//If we want to upgrade or product new skills for soldiers we can set items which it will product to content.
				break;
			case ProductType.Barrack:
				for (int i = 0; i < product.productableList.Count; i++)
				{
					Instantiate(product.productableList[i], informationContent.transform);
				}
				informationScrollRect.gameObject.SetActive(true);
				break;
			case ProductType.PowerPlant:
				//for the future if we will produce new units from PP we can set items to content.
				break;
			default:
				break;
		}

		
	}
	public void CloseInfoPanel()
	{
		informationPanel.SetActive(false);

		for (int i = 0; i < informationContent.transform.childCount; i++)
		{
			GameObject o = informationContent.transform.GetChild(i).gameObject;
			Destroy(o);
		}
	}
}
