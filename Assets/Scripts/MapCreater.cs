using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// It will generate game map
/// </summary>
public class MapCreater : Singleton<MapCreater>
{

	[SerializeField] private GameObject cellPrefab;
	[SerializeField] private GameObject rightBorder;
	[SerializeField] private GameObject bottomBorder;

	[SerializeField] private int rowCount;
	[SerializeField] private int columnCount;

	private float cellSize;
	private Vector3 firstCellPos;

	void Start()
	{
		cellSize = cellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		firstCellPos = cellPrefab.transform.position;
		GenerateCells();
	}

	void GenerateCells()
	{
		GameObject cell = null;
		for (int i = 0; i < columnCount; i++)
		{
			for (int j = 0; j < rowCount; j++)
			{
				cell = Instantiate(cellPrefab, transform);
				cell.transform.position = firstCellPos + new Vector3(i * cellSize, -j * cellSize, 0);
				
			}
		}
		rightBorder.transform.position = new Vector3(cell.transform.position.x,rightBorder.transform.position.y,rightBorder.transform.position.z);
		bottomBorder.transform.position = new Vector3(bottomBorder.transform.position.x,cell.transform.position.y,bottomBorder.transform.position.z);
	}

}
