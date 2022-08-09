using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool main;
	public GameObject bulletPrefab;
	private List<Bullet> availableBullets;
	
	private void Awake()
	{
		main = this;
	}
	
	// Start is called before the first frame update
    private void Start()
    {
        int i;
		availableBullets = new List<Bullet>();
		
		// Set a hard limit of 99 bullets to be active at once
		for (i = 0; i < 99; i++)
		{
			Bullet b = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
			b.gameObject.SetActive(false);
			availableBullets.Add(b);
		}
    }
	
	// Subtracts a bullet from the pool
	public void ChooseFromPool(Vector3 position, Vector3 velocity)
	{
		if (availableBullets.Count == 0)
		{
			return;
		}
		
		availableBullets[0].Activate(position, velocity);
		availableBullets.RemoveAt(0);
	}
	
	// Adds a bullet back to the pool
	public void AddToPool(Bullet bullet)
	{
		if (!availableBullets.Contains(bullet))
		{
			availableBullets.Add(bullet);
		}
	}
}
