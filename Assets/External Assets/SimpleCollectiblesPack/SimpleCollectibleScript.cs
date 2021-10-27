using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleTypes {NoType, Coin, Fuel, Type3, Type4, Type5};

	public CollectibleTypes CollectibleType;

	public bool rotate;

	public float rotationSpeed;

	public AudioClip collectSound;

	public GameObject collectEffect;


	public delegate void OnCoinPickedDelegate();
	public static OnCoinPickedDelegate coinPickedDelegate;

	public delegate void OnFuelPickedDelegate(float value);
	public static OnFuelPickedDelegate fuelPickedDelegate;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			Collect ();
		}
	}

	public void Collect()
	{
		if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);





		if (CollectibleType == CollectibleTypes.NoType) 
		{
		}
		if (CollectibleType == CollectibleTypes.Coin)
		{
			coinPickedDelegate();
		}
		if (CollectibleType == CollectibleTypes.Fuel) 
		{
			fuelPickedDelegate(25f);
		}
		if (CollectibleType == CollectibleTypes.Type3) 
		{
		}
		if (CollectibleType == CollectibleTypes.Type4) 
		{
		}
		if (CollectibleType == CollectibleTypes.Type5) 
		{
		}



		Destroy (gameObject);
	}
}
