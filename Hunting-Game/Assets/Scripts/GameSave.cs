using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSave : MonoBehaviour
{
    [SerializeField] public bool level1Complete;
    [SerializeField] public bool level2Complete;
    [SerializeField] public bool level3Complete;
    [SerializeField] public int weapon;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            level1Complete = SaveManager.instance.activeSave.level1Complete;
            level2Complete = SaveManager.instance.activeSave.level2Complete;
            level3Complete = SaveManager.instance.activeSave.level3Complete;
            weapon = SaveManager.instance.activeSave.weapon;
        }   
        else
        {
            SaveManager.instance.activeSave.level1Complete = level1Complete;
            SaveManager.instance.activeSave.level2Complete = level2Complete;
            SaveManager.instance.activeSave.level3Complete = level3Complete;
            SaveManager.instance.activeSave.weapon = weapon;
        }
    }

    public void SaveProgress()
    {
        SaveManager.instance.activeSave.level1Complete = level1Complete;
        SaveManager.instance.activeSave.level2Complete = level2Complete;
        SaveManager.instance.activeSave.level3Complete = level3Complete;
        SaveManager.instance.activeSave.weapon = weapon;
    }

    public void level1()
    {
        level1Complete = true;
    }

    public void level2()
    {
        level2Complete = true;
    }

    public void level3()
    {
        level3Complete = true;
    }

    public void weaponChoice(int choice)
    {
        weapon = choice;
        SaveManager.instance.activeSave.weapon = weapon;
    }
    // public void weaponChoice1()
    // {
    //     weapon = 0;
    //     SaveManager.instance.activeSave.weapon = weapon;
    // }
    // public void weaponChoice2()
    // {
    //     weapon = 1;
    //     SaveManager.instance.activeSave.weapon = weapon;
    // }
    // public void weaponChoice3()
    // {
    //     weapon = 2;
    //     SaveManager.instance.activeSave.weapon = weapon;
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
