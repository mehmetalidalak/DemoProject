using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducableObjectCreater : MonoBehaviour
{

	[SerializeField] private ProductScriptable producableProduct;

	/// <summary>
	/// Spawn button function. 
	/// It used to spawn soldiers now.
	/// Also for new utils can use that function which will be type of "producableUtils".
	/// </summary>
	public void SpawnProduct()
	{
		GameObject spawnPoint = ControllManager.instance.activeBarrack.transform.GetChild(0).gameObject;
		GameObject o = Instantiate(producableProduct.product);
		o.transform.position = spawnPoint.transform.position;
	}
}
