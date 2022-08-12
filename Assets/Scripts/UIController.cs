using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text timeLabel;

    [SerializeField] TMP_Text sheepCountLabel;

    [SerializeField] Slider cooldownSlider;

    [SerializeField] TMP_Text recordLabel;

    private int score;
    private float cdValue;

    private const string playerPrefsRecordKey = "record";


    private void OnEnable()
    {
        EventSystem.OnSheepCaught += OnSheepCount;
        EventSystem.OnLevelStart += OnLevelStartSheepCountReset;
        EventSystem.OnLeapCooldown += OnCooldownSliderReset;
        EventSystem.OnRecord += OnRecordUpdate;
    }
    private void OnDisable()
    {
        EventSystem.OnSheepCaught -= OnSheepCount;
        EventSystem.OnLevelStart -= OnLevelStartSheepCountReset;
        EventSystem.OnLeapCooldown -= OnCooldownSliderReset;
        EventSystem.OnRecord -= OnRecordUpdate;
    }
    private void Start()
    {
        OnLevelStartSheepCountReset();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cooldownSlider.value = 1f;
        float lastRecord = PlayerPrefs.GetFloat(playerPrefsRecordKey, 0f);
        UpdateRecordLabel(TimeSpan.FromSeconds(lastRecord));
    }

    private void Update()
    {
        TimeSpan timeSinceStart = TimeManager.Instance.TimeSinceStartup;
        int minutes = (int)timeSinceStart.TotalMinutes;
        double seconds = timeSinceStart.Seconds;
        string timerText;

        if (minutes == 0)
            timerText = seconds.ToString("00") + " s.";
        else
            timerText = $"{minutes}:{seconds.ToString("00")} s.";

        timeLabel.text = timerText;
        
        if (cooldownSlider.value <= cdValue)
        {
            cooldownSlider.value += Time.deltaTime;
        }
    }

    private void OnSheepCount(GameObject _)
    {
        score += 1;
        ChangeSheepCountLabel();
    }
    private void OnLevelStartSheepCountReset()
    {
        score = 0;
        ChangeSheepCountLabel();
    }
    private void ChangeSheepCountLabel()
    {
        int maxSheep = SheepController.Instance.maxSheep;
        sheepCountLabel.text = $"{score}/{maxSheep}";
    }
    private void OnCooldownSliderReset(float cd)
    {
        cooldownSlider.value = 0;
        cdValue = cd;
        cooldownSlider.maxValue = cd;
    }
    private void OnRecordUpdate()
    {
        TimeSpan timeSinceStart = TimeManager.Instance.TimeSinceStartup;
        float lastRecord = PlayerPrefs.GetFloat(playerPrefsRecordKey, float.MaxValue);
        if (timeSinceStart.TotalSeconds < lastRecord)
        {
            PlayerPrefs.SetFloat(playerPrefsRecordKey, (float)timeSinceStart.TotalSeconds);
            UpdateRecordLabel(timeSinceStart);
        }
    }
    private void UpdateRecordLabel(TimeSpan timeSinceStart)
    {
        string recordText = "Record: " + timeSinceStart.ToString(@"mm\:ss");
        recordLabel.text = recordText;
    }

}
