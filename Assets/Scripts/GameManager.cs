using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Slider slider, randomnessSlider;
    [SerializeField] private Toggle randomizeToggle, wrapAroundToggle;
    [SerializeField] private TMP_InputField inputFieldX, inputFieldY;
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve curve;
    
    private bool _simRunning;
    private bool  simRunning
    {
        get => _simRunning;
        set
        {
            if (value)
            {
                grid.StartSim();
                buttonText.text = StopSimText;
            }
            else
            {
                grid.StopSim();
                buttonText.text = StartSimText;
            }
            _simRunning = value;
        }
    }

    private const string StartSimText = "Start Simulation", StopSimText = "Stop Simulation";
    
    private void Start()
    {
        simRunning = false;
        grid.timeToWait = speed - (curve.Evaluate(slider.value) * speed);
        buttonText.text = StartSimText;
    }

    public void OnSliderValueChanged(float value)
    {
        grid.timeToWait = speed - (curve.Evaluate(value) * speed);
    }

    public void OnStartStopButton()
    {
        simRunning = !simRunning;
    }

    public void OneOneStepButton()
    {
        grid.UpdateCells();
    }

    public void OnResetButton()
    {
        grid.ResetGrid();
    }

    public void OnGenNewSimButton()
    {
        if (inputFieldX.text == "" || inputFieldY.text == "") return;
        grid.CreateGrid(int.Parse(inputFieldX.text), int.Parse(inputFieldY.text), randomizeToggle.isOn, randomnessSlider.value, wrapAroundToggle.isOn);
    }

    public void OnRandomizeButton()
    {
        grid.RandomizeGrid(randomnessSlider.value);
    }
}
