using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reglette : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private TextMeshProUGUI goalPrize;
    [SerializeField]
    private TextMeshProUGUI currentPrize;
    [SerializeField]
    private TextMeshProUGUI maxPrize;

    [Header("Values")]
    [SerializeField]
    private float goalValue;
    [SerializeField]
    private float currentValue;
    [SerializeField]
    private float maxValue;
    public float CurrentValue => currentValue;

    public void SetUp(float goal, float max)
    {
        goalValue = goal;
        maxValue = max;

        goalPrize.text = $"{(int)goalValue}d";
        maxPrize.text = $"{(int)maxValue}d";

        UpdateTextAndPosition(max);
    }

    public bool UpdateTextAndPosition(float _currentValue)
    {
        if (_currentValue < goalValue)
            return false;

        currentValue = _currentValue;
        currentPrize.text = $"{(int)_currentValue}d";

        float goalX = goalPrize.gameObject.transform.position.x;
        float maxX  = maxPrize.gameObject.transform.position.x;

        Vector3 currentPos = currentPrize.transform.position;
        currentPos.x = Mathf.Lerp(goalX, maxX, (_currentValue - goalValue) / (maxValue - goalValue));
        currentPrize.transform.position = currentPos;
        return true;
    }
    /*
    [Header("Test")]
    [SerializeField]
    private float testNewValue;
    [SerializeField]
    private bool launchTest;

    private void Start()
    {
        SetUp(10f, 100f);
    }

    private void Update()
    {
        if(launchTest)
        {
            launchTest = false;
            UpdateTextAndPosition(testNewValue);
        }
    }*/
}
