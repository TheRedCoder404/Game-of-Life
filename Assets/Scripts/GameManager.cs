using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Slider slider, randomnessSlider;
    [SerializeField] private Toggle randomizeToggle;
    [SerializeField] private TMP_InputField inputFieldX, inputFieldY;
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve curve;
    
    private bool _simRunning;
    private bool simRunning
    {
        get => _simRunning;
        set
        {
            if (value)
            {
                grid.StartSim();
                buttonText.text = stopSimText;
            }
            else
            {
                grid.StopSim();
                buttonText.text = startSimText;
            }
            _simRunning = value;
        }
    }

    private string startSimText = "Start Simulation", stopSimText = "Stop Simulation";
    
    private void Start()
    {
        simRunning = false;
        grid.speed = speed - (curve.Evaluate(slider.value) * speed);
        buttonText.text = startSimText;
    }

    public void OnSliderValueChanged(float value)
    {
        grid.speed = speed - (curve.Evaluate(value) * speed);
    }

    public void OnStartStopButton()
    {
        simRunning = !simRunning;
    }

    public void OnGenNewSimButton()
    {
        if (inputFieldX.text == "" || inputFieldY.text == "") return;
        grid.CreateGrid(int.Parse(inputFieldX.text), int.Parse(inputFieldY.text), randomizeToggle.isOn, randomnessSlider.value);
    }

    public void OnRandomizeButton()
    {
        grid.RandomizeGrid(randomnessSlider.value);
    }
}
