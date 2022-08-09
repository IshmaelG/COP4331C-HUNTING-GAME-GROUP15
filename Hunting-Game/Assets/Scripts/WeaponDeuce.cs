// Weapon file for Double Deuce

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDeuce : MonoBehaviour
{
    BulletPool bPool;
	public Transform camera;
	public Transform firePoint;
	public Animator animator;
	
	// Weapon stat variables
	public float bulletSpeed = 10;
	public bool isFiring;
	public int currentAmmo = 2;
	public int totalAmmo = 2;
	public float reloadTimer = 2.9f;
	
	// Animation things
	private bool isReloading = false;
	private bool isRechambering = false;
	private bool isScoped = false;
	public GameObject crosshair;
	public GameObject scopeReticle;
	public GameObject weaponCamera;
	public Camera PlayerCamera;
	public float scopeFOV = 60f;
	private float previousFOV;
	
	// Audio things
	public AudioSource[] sounds;
	public AudioSource fireSound;
	public AudioSource reloadSound;
	public AudioSource scopeSound;
	
	// Ammo counter variables
	public Text ammoCounter;
	public GameObject patriotCounter;
	public GameObject scoutCounter;
	public GameObject deuceCounter;
	
	// Start is called before the first frame update
    private void Start()
    {
        currentAmmo = totalAmmo;
		bPool = BulletPool.main;
		
		// Activate crosshair by default
		scopeReticle.SetActive(false);
		crosshair.SetActive(true);
		weaponCamera.SetActive(true);
		
		// Initialize ammo counter
		patriotCounter.SetActive(false);
		scoutCounter.SetActive(false);
		deuceCounter.SetActive(true);
		ammoCounter.text = currentAmmo.ToString() + " | " + totalAmmo.ToString();
    }
	
	// Update is called once per frame
    void Update()
    {
		// Return if the player is reloading
		if (isReloading == true)
		{
			return;
		}
		
		// Automatically reloads if the magazine is empty and the player is not reloading
		else if ((currentAmmo <= 0) && (isReloading == false))
		{
			StartCoroutine(Reload());
			return;
		}
		
		// Return if the player is reloading
		else if (isRechambering == true)
		{
			return;
		}
		
		// Checks if the player is firing
		else if (isFiring == true)
		{
			// Checks if the weapon cooldown is active
			if (isRechambering == true)
			{
				Debug.Log("Cannot fire while cooldown is active.");
			}
			
			else
			{
				Fire();
				Debug.Log("Fired a bullet.");
			}
		}
    }
	
	// Spawns a bullet from the BulletPool
	public void Fire()
	{
		// Refuse to fire if reloading
		if (isReloading == true)
		{
			Debug.Log("Cannot fire while reloading.");
		}
		
		// Refuse to fire if rechamberiqng
		else if (isRechambering == true)
		{
			Debug.Log("Cannot fire while rechambering.");
		}
		
		else
		{
			// Initialize bullet velocity
			Vector3 bulletVelocity = camera.forward * bulletSpeed;
			
			// Fire!
			bPool.ChooseFromPool(firePoint.position, bulletVelocity);
			fireSound.Play();
			
			// Only play firing animation when unscoped
			if (isScoped == false)
			{
				animator.SetTrigger("Shoot");
			}
			
			Debug.Log("Fired a bullet.");
			
			// Remove a bullet from the magazine and update ammo counter
			currentAmmo = currentAmmo - 1;
			ammoCounter.text = currentAmmo.ToString() + " | " + totalAmmo.ToString();
			
			// Rechamber
			isRechambering = true;
			StartCoroutine(RateOfFire());
		}
	}
	
	// Player presses fire button
	public void FirePress()
	{
		Debug.Log("Fire button pressed.");
		Fire();
	}
	
	// Player releases fire button
	public void FireRelease()
	{
		Debug.Log("Fire button released.");
		isFiring = false;
	}
	
	// (Un)scope function
	IEnumerator Scope()
	{
		// Refuse to scope in while reloading
		if (isReloading == true)
		{
			Debug.Log("Cannot scope in while reloading.");
		}
		
		// Scope in
		else if (isScoped == false)
		{
			Debug.Log("Scoping in...");
			animator.SetBool("Scoped", true);
			yield return new WaitForSeconds(0.4f);
			scopeSound.Play();
			crosshair.SetActive(false);
			weaponCamera.SetActive(false);
			previousFOV = PlayerCamera.fieldOfView;
			PlayerCamera.fieldOfView = scopeFOV;
			isScoped = true;
		}
		
		// Scope out
		else
		{
			Debug.Log("Scoping out...");
			animator.SetBool("Scoped", false);
			scopeSound.Play();
			crosshair.SetActive(true);
			weaponCamera.SetActive(true);
			PlayerCamera.fieldOfView = previousFOV;
			isScoped = false;
		}
	}
	
	// Player presses fire button
	public void ScopePress()
	{
		Debug.Log("Scope button pressed.");
		StartCoroutine(Scope());
	}
	
	// Player releases fire button
	public void ScopeRelease()
	{
		Debug.Log("Scope button released.");
	}
	
	// Reload function
	IEnumerator Reload()
	{
		// Unscope if scoped in
		if (isScoped == true)
		{
			Debug.Log("Scoping out to reload.");
			animator.SetBool("Scoped", false);
			scopeSound.Play();
			crosshair.SetActive(true);
			weaponCamera.SetActive(true);
			PlayerCamera.fieldOfView = previousFOV;
			isScoped = false;
		}
		
		// Prevent reload if the magazine is already full
		if (currentAmmo == totalAmmo)
		{
			Debug.Log("Weapon already full; cannot reload.");
		} 
		
		// Reload!
		else
		{
			// Reload start
			isReloading = true;
			Debug.Log("Reloading...");
			animator.SetTrigger("Reload");
			yield return new WaitForSeconds(0.6f);
			reloadSound.Play();
			yield return new WaitForSeconds(1.9f);
			
			// Reload complete
			currentAmmo = totalAmmo;
			ammoCounter.text = currentAmmo.ToString() + " | " + totalAmmo.ToString();
			isReloading = false;
			Debug.Log("Reloading complete.");
		}
	}
	
	// Player presses reload button
	public void ReloadPress()
	{
		Debug.Log("Reload button pressed.");
		
		// Prevent reload if magazine and chamber are already full
		if (currentAmmo == totalAmmo)
		{
			Debug.Log("Weapon already full; cannot reload.");
		}
		
		else
		{
			// Begin reloading
			StartCoroutine(Reload());
		}
	}
	
	// Player releases reload button
	public void ReloadRelease()
	{
		Debug.Log("Reload button released.");
	}
	
	// Rate of Fire function
	IEnumerator RateOfFire()
	{
		if (isRechambering == true)
		{
			Debug.Log("Rechambering...");
			// rechamberSound.Play();
			yield return new WaitForSeconds(0.3f); // fireDelay
			Debug.Log("Ready to fire.");
			isRechambering = false;
		}
	}
}
