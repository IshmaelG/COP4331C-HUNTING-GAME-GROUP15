using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    int selectedWeapon = SaveManager.instance.activeSave.weapon;
	
	// Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }
	
	// This is probably what has to be edited to work with the menu.
    // Update is called once per frame
    void Update()
    {
        
    }
	
	// Weapon selection function
	void SelectWeapon()
	{
		// selectedWeapon = 0 will select the first weapon under Weapon Holder (Mossberg Patriot)
		// selectedWeapon = 1 will select the second weapon (Steyr Scout)
		// selectedWeapon = 2 will select the third weapon (Double Deuce)
		int i = 0;
		
		// Loop to sort through each weapon
		foreach (Transform weapon in transform)
		{
			// Enables the selected weapon
			if (i == selectedWeapon)
			{
				weapon.gameObject.SetActive(true);
				i = i + 1;
			}
			
			// Disables the rest
			else
			{
				weapon.gameObject.SetActive(false);
				i = i + 1;
			}
		}
	}
}
