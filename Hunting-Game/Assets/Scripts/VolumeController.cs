using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider soundSlider = null;
    [SerializeField] private float defaultValue = 0.5f;
    [SerializeField] private Button exitButton;
    private float initialValue;
    public bool test;

    public AudioMixer mixer;

    // Sets the slider level and saves its value
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("soundVolume", Mathf.Log10 (sliderValue) * 20);
        PlayerPrefs.SetFloat("soundVolume", sliderValue);
    }

    private void Start()
    {
        // If the user has never set/saved a sound level preference
        if (soundSlider == null) 
        {
            PlayerPrefs.SetFloat("soundVolume", defaultValue);
            soundSlider.value = defaultValue;
        }
        test = false;
        LoadValues();
    }

    // If the user clicks the exit button and test is false, the user did not click apply, and therefore,
    // the slider values are set back to what they were previously.
    public void ExitButton()
    {
        bool t = test;
        test = false;
        if (!t) 
        {
            PlayerPrefs.SetFloat("soundValue", initialValue);
            t = false;
            LoadValues();
        }
    }

    // Saves the value, resets the new initial value, test is then true to verify that the user did in fact 
    // save their preferences,then loads the values
    public void SaveButton()
    {
        float soundValue = soundSlider.value;
        PlayerPrefs.SetFloat("soundValue", soundValue);
        initialValue = soundValue;
        //mixer.SetFloat("soundVolume", Mathf.Log10 (musicValue) * 20);
        test = true;

        LoadValues();
    }

    // If clicked, the music levels and slider is reset to default.
    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            mixer.SetFloat("soundVolume", Mathf.Log10 (defaultValue) * 20);
            soundSlider.value = defaultValue;
            SaveButton();
        }
    }

    // Loads the values by getting them, and setting them.
    void LoadValues()
    {
        float soundValue = PlayerPrefs.GetFloat("soundValue");
        initialValue = soundValue;
        soundSlider.value = soundValue;
        mixer.SetFloat("soundVolume", Mathf.Log10 (soundValue) * 20);

    }

}
