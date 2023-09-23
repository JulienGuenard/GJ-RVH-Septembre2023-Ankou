using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NegoBar : MonoBehaviour
{

    [Header("Refs")]
    [SerializeField]
    private GameObject backgroundBar;
    [SerializeField]
    private GameObject middleBar;
    [SerializeField]
    private GameObject topBar;
    [SerializeField]
    private GameObject cursor;

    [Header("Bar Colors")]
    [SerializeField]
    private Color lowColor;
    [SerializeField]
    private Color middleColor;
    [SerializeField]
    private Color highColor;

    [Header("Parameters")]
    [SerializeField]
    private float totalSize;
    [SerializeField]
    private GameObject lowGO;
    [SerializeField] [Range(1f, 99f)]
    private float lowPercentage;
    [SerializeField] [Range(-50f, 50f)]
    private float lowPosition;
    [Space]
    [SerializeField]
    private GameObject middleGO;
    [SerializeField] [Range(1f, 99f)]
    private float middlePercentage;
    [SerializeField] [Range(-50f, 50f)]
    private float middlePosition;
    [Space]
    [SerializeField]
    private GameObject highGO;
    [SerializeField] [Range(1f, 99f)]
    private float highPercentage;
    [SerializeField] [Range(-50f, 50f)]
    private float highPosition;

    [Header("In Play")]
    [SerializeField]
    private float cursorMinTime;
    [SerializeField]
    private float cursorMaxTime;
    [SerializeField]
    private AnimationCurve timeToCursorPosCurve;
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private float pauseTime;
    [SerializeField]
    private bool isPaused;
    [SerializeField]
    private bool isPlaying;

    public void UpdatePercentages(float _lowPercentage, float _middlePercentage, float _highPercentage)
    {
        _lowPercentage    = Mathf.Clamp(_lowPercentage, 1f, 99f);
        _middlePercentage = Mathf.Clamp(_middlePercentage, 1f, 99f);
        _highPercentage   = Mathf.Clamp(_highPercentage, 1f, 99f);

        float total = _lowPercentage + _middlePercentage + _highPercentage;

        if (total != 100f)
        {
            _lowPercentage = _lowPercentage / total * 100f;
            _middlePercentage = _middlePercentage / total * 100f;
            _highPercentage = _highPercentage / total * 100f;
        }

        lowPercentage = _lowPercentage;
        middlePercentage = _middlePercentage;
        highPercentage = _highPercentage;

        CheckLayers(ref _lowPercentage, ref _middlePercentage, ref _highPercentage);
        Debug.Log($"{_lowPercentage}, {_middlePercentage}, {_highPercentage}");

        float trueLowSize = lowGO == backgroundBar ? totalSize : _lowPercentage * totalSize / 100f;
        Debug.Log($"low size : {trueLowSize}");
        ((RectTransform)lowGO.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, trueLowSize);
        lowGO.GetComponent<Image>().color = lowColor;

        float trueMiddleSize = middleGO == backgroundBar ? totalSize : _middlePercentage * totalSize / 100f;
        Debug.Log($"middle size : {trueMiddleSize}");
        ((RectTransform)middleGO.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, trueMiddleSize);
        middleGO.GetComponent<Image>().color = middleColor;

        float trueHighSize = highGO == backgroundBar ? totalSize : _highPercentage * totalSize / 100f;
        Debug.Log($"high size : {trueHighSize}");
        ((RectTransform)highGO.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, trueHighSize);
        highGO.GetComponent<Image>().color = highColor;
    }

    private void CheckLayers(ref float _lowPercentage, ref float _middlePercentage, ref float _highPercentage)
    {
        lowGO = null;
        middleGO = null;
        highGO = null;

        float max = Mathf.Max(_lowPercentage, _middlePercentage, _highPercentage);

        if (max == _lowPercentage)
        {
            lowGO = backgroundBar;
            _lowPercentage = 100f;
        }
        else if (max == _middlePercentage)
        {
            middleGO = backgroundBar;
            _middlePercentage = 100f;
        }
        else
        {
            highGO = backgroundBar;
            _highPercentage = 100f;
        }

        float min = Mathf.Min(_lowPercentage, _middlePercentage, _highPercentage);

        if (min == _lowPercentage && lowGO != backgroundBar)
            lowGO = topBar;
        else if (min == _middlePercentage && middleGO != backgroundBar)
            middleGO = topBar;
        else if (highGO != backgroundBar)
            highGO = topBar;

        if (!lowGO)
        { 
            lowGO = middleBar;
            _lowPercentage += min;
        }
        else if (!middleGO)
        {
            middleGO = middleBar;
            _middlePercentage += min;
        }
        else
        {
            highGO = middleBar;
            _highPercentage += min;
        }
    }

    public void CheckPositions(float _lowPosition = 0f, float _middlePosition = 0f, float _highPosition = 0f)
    {
        _lowPosition    = Mathf.Clamp(_lowPosition, -50f, 50f);
        _middlePosition = Mathf.Clamp(_middlePosition, -50f, 50f);
        _highPosition   = Mathf.Clamp(_highPosition, -50f, 50f);

        if(lowGO == middleBar)
        {
            if(middleGO == topBar)
                CorrectPositions(lowGO, _lowPosition, middleGO, _middlePosition);
            else
                CorrectPositions(lowGO, _lowPosition, highGO, _highPosition);
        }
        else if (middleGO == middleBar)
        {
            if (lowGO == topBar)
                CorrectPositions(middleGO, _middlePosition, lowGO, _lowPosition);
            else
                CorrectPositions(middleGO, _middlePosition, highGO, _highPosition);
        }
        else
        {
            if(lowGO == topBar)
                CorrectPositions(highGO, _highPosition, lowGO, _lowPosition);
            else
                CorrectPositions(highGO, _highPosition, middleGO, _middlePosition);
        }
    }

    private void CorrectPositions(GameObject secondBar, float secondPosition, GameObject thirdBar, float thirdPosition)
    {
        float firstTrueSize = ((RectTransform)backgroundBar.transform).rect.width;
        float secondTrueSize = ((RectTransform)secondBar.transform).rect.width;
        float thirdTrueSize = ((RectTransform)thirdBar.transform).rect.width;

        float secondMaxOffSet = firstTrueSize - secondTrueSize;
        Vector3 newPos = secondBar.transform.localPosition;
        newPos.x = secondMaxOffSet * secondPosition / 100f;
        secondBar.transform.localPosition = newPos;

        float thirdMaxOffSet = secondTrueSize - thirdTrueSize;
        newPos = thirdBar.transform.localPosition;
        newPos.x = thirdMaxOffSet * thirdPosition / 100f;
        thirdBar.transform.localPosition = newPos;
    }

    public void UpdateCurrentTimer(float progression)
    {
        currentTime = Mathf.Lerp(cursorMinTime, cursorMaxTime, progression);
    }

    public void StartPlaying()
    {
        StartCoroutine(Loop());
    }

    public bool PickUpValue(out float val)
    {
        if (isPaused)
        {
            val = 0f;
            return false;
        }

        isPaused = true;

        float truePos = cursor.transform.localPosition.x;

        Debug.Log($"truePos = {truePos}");

        GameObject bar;

        RectTransform middleTransform = (RectTransform)middleBar.transform;
        float middleLocalPosX = middleTransform.localPosition.x;
        float minX = middleLocalPosX - middleTransform.rect.size.x / 2f;
        float maxX = middleLocalPosX + middleTransform.rect.size.x / 2f;

        Debug.Log($"min Middle = {minX}, max Middle = {maxX}");

        if (truePos >= minX && truePos <= maxX) // isInMiddleBar
        {
            Debug.Log("Is in Middle");
            RectTransform topTransform = (RectTransform)topBar.transform;
            float topLocalPosX = middleTransform.localPosition.x;
            minX = middleLocalPosX + topLocalPosX - topTransform.rect.size.x / 2f;
            maxX = middleLocalPosX + topLocalPosX + topTransform.rect.size.x / 2f;

            Debug.Log($"min Top = {minX}, max Top = {maxX}");

            if (truePos >= minX && truePos <= maxX) // isTopBar
            {
                bar = topBar;
                Debug.Log("Is Top");
            }
            else
            {
                bar = middleBar;
                Debug.Log("Is Middle");
            }
        }
        else
        {
            bar = backgroundBar;
            Debug.Log("Is Background");
        }

        if (bar == lowGO)
            val = 5f;
        else if (bar == middleGO)
            val = 10f;
        else
            val = 15f;

        DrawNewBar();
        return true;
    }

    private void DrawNewBar()
    {
        float _lowPercentage = UnityEngine.Random.Range(1f, 95f);
        float _middlePercentage = UnityEngine.Random.Range(1f, _lowPercentage - 5f);
        float _highPercentage = 100f - _middlePercentage;

        UpdatePercentages(_lowPercentage, _middlePercentage, _highPercentage);
        CheckPositions(UnityEngine.Random.Range(-50f,50f), UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f));
    }

    public void Stop()
    {
        isPlaying = false;
    }

    private IEnumerator Loop(
        float _lowPercentage = 50f, float _middlePercentage = 30f,
        float _highPercentage = 20f, float _lowPosition = 0f,
        float _middlePosition = 0f, float _highPosition = 0f
    )
    {
        UpdatePercentages(_lowPercentage, _middlePercentage, _highPercentage);
        CheckPositions(_lowPosition, _middlePosition, _highPosition);

        isPlaying = true;
        float playTimer = 0f, pauseTimer = 0f;
        bool isOneWay = true;

        UpdateCurrentTimer(playTimer);

        while(isPlaying)
        {
            if(isPaused)
            {
                pauseTimer += Time.deltaTime;

                if(pauseTimer >= pauseTime)
                {
                    pauseTimer = 0f;
                    isPaused = false;
                }
            }
            else
            {
                playTimer += Time.deltaTime;

                if (playTimer >= currentTime)
                {
                    playTimer = 0f;
                    isOneWay = !isOneWay;
                }

                Vector3 cursorPos = cursor.transform.localPosition;
                cursorPos.x = (isOneWay ? 1 : -1) * (-totalSize/2 + totalSize * Mathf.Lerp(0f, currentTime, playTimer));
                cursor.transform.localPosition = cursorPos;
            }

            yield return null;
        }
    }

    /*[Header("Test")]
    public float testLowPercentage;
    public float testMiddlePercentage;
    public float testHighPercentage;
    [Space]
    public float testLowPosition;
    public float testMiddlePosition;
    public float testHighPosition;
    [Space]
    public bool launchTest;

    private void Update()
    {
        if (launchTest)
        {
            launchTest = false;
            UpdatePercentages(testLowPercentage, testMiddlePercentage, testHighPercentage);
            CheckPositions(testLowPosition, testMiddlePosition, testHighPosition);
        }
    }*/
}
