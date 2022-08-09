using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Rigidbody rBody;
	public float bulletTimer;
	
	public void Activate(Vector3 position, Vector3 velocity)
	{
		transform.position = position;
		rBody.velocity = velocity;
		
		gameObject.SetActive(true);
		StartCoroutine("Decay");
	}
	
	// Checks to see if a bullet has been active for too long without hitting anything
	private IEnumerator Decay()
	{
		yield return new WaitForSeconds(bulletTimer);
		Deactivate();
	}
	
	// Deactivates a bullet
	public void Deactivate()
	{
		BulletPool.main.AddToPool(this);
		StopAllCoroutines();
		gameObject.SetActive(false);
	}
	
	// Detects bullet impacts
	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("Bullet impact!");
		if(other.isTrigger)
        {
			ScoreManager.instance.AddHSPoint();
		}
		else
			ScoreManager.instance.AddPoint();
		Destroy(other.gameObject);
		BeatLevel.enemies--;
		Deactivate();
	}
}
