using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllManager : Singleton<ControllManager> {

	public bool isAvailableToBuild = true;

	public GameObject activeBarrack;

	public GameObject moveProducableObject;

	public GameObject activeObject; // currently moving object

	/// <summary>
	/// The function round the current float value to requested float value.
	/// "It used to make position multiple of .32f"
	/// </summary>
	/// <param name="value">Value to round</param>
	/// <param name="multipleOf">Multiplier of round</param>
	/// <returns></returns>
	public float RoundTo(float value, float multipleOf)
	{
		return Mathf.Round(value / multipleOf) * multipleOf;
	}


}
