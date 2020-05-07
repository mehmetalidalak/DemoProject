using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ProductType
{
	Soldier,
	Barrack,
	PowerPlant
}
[CreateAssetMenu]
public class ProductScriptable : ScriptableObject {	

	public GameObject product;
	public Sprite productSprite;
	public List<GameObject> productableList;
	public string productName;
	public string productDimension;
	public ProductType productType;
}
