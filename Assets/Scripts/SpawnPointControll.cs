using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this script inside the spawn point and it will find a safe place to locate the spawn point.
/// </summary>
public class SpawnPointControll : MonoBehaviour
{
	
	private void OnTriggerStay2D(Collider2D other)
	{

		if (other.CompareTag(TagManager.CELL_TAG))
		{
			return;
		}

		if (other.CompareTag(TagManager.SOLDIER_TAG) || other.CompareTag(TagManager.PRODUCT_TAG))
		{
			int rand = Random.Range(0, 2);
			if (rand == 0)
			{
				//random x movement for spawn point
				rand = Random.Range(0, 2);
				if (rand == 0)
					transform.position += new Vector3(ValueManager.cellSize, 0, 0);
				else
					transform.position += new Vector3(-ValueManager.cellSize, 0, 0);
			}
			else
			{
				//random y movement for spawn point
				rand = Random.Range(0, 2);
				if (rand == 0)
					transform.position += new Vector3(0, ValueManager.cellSize, 0);
				else
					transform.position += new Vector3(0, -ValueManager.cellSize, 0);
			}

		}
	}
	

}
