using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{
    [SerializeField] private PlayerStatistics m_playerStats;

    public string m_lastClaimTime;

    [SerializeField] private Button m_button;
    [SerializeField] private Slider m_slider;
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private TextMeshProUGUI m_collectionText;
    [SerializeField] private GameObject m_collectionScreen;

    public bool m_todayClaimed;

    float m_maxSliderTime;

    private void Start()
    {
        float hour = DateTime.MaxValue.Hour * 3600;
        float minute = DateTime.MaxValue.Minute * 60;
        float second = DateTime.MaxValue.Second;
        m_maxSliderTime = hour + minute + second;

        //m_slider.maxValue = m_maxSliderTime;

        string lastTime = m_lastClaimTime;

        DateTime lastClaimTime;

        if (!string.IsNullOrEmpty(lastTime))
        {
            lastClaimTime = DateTime.Parse(lastTime);
        }
        else
        {
            lastClaimTime = DateTime.MinValue;
        }

        if (DateTime.Today > lastClaimTime)
        {
            m_button.interactable = true;
            m_slider.value = m_maxSliderTime;
            m_todayClaimed = false;
            m_text.text = "Ready!";
        }
        else
        {
            m_button.interactable = false;
            m_text.text = TimeTillNextClaim();
        }        
    }

    private void Update()
    {
        if (m_todayClaimed)
        {
            m_text.text = TimeTillNextClaim();

            float sliderHour = SliderTimeTillNextClaim().Item1 * 3600;
            float sliderMinute = SliderTimeTillNextClaim().Item2 * 60;
            float sliderSecond = SliderTimeTillNextClaim().Item3;

            float totalTime = sliderHour + sliderMinute + sliderSecond;
            m_slider.value = (m_maxSliderTime - totalTime) / m_maxSliderTime;

            if (totalTime == 0)
            {
                NewDay();   
            }

        }
    }

    public void RewardClaimed()
    {
        m_lastClaimTime = DateTime.Now.ToString();

        m_button.interactable = false;
        m_text.text = TimeTillNextClaim();

        RewardScreen();

        if (!m_todayClaimed)
        {
            m_todayClaimed = true;
        }
    }

    public void RewardScreen() // TODO: NOT COMPLETE, TEMP THINGS
    {
        m_collectionScreen.SetActive(true);

        int random = UnityEngine.Random.Range(0, 3);

        if (random == 0)
        {
            m_collectionText.text = "You've just earned 1000 moneys!";
            m_playerStats.m_money += 1000;
        }
        else if (random == 1)
        {
            m_collectionText.text = "You've just earned 100 moneys!";
            m_playerStats.m_money += 100;
        }
        else
        {
            m_collectionText.text = "You've just earned 50 moneys!";
            m_playerStats.m_money += 50;
        }
    }

    public string TimeTillNextClaim()
    {
        int hours = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalHours);
        int mins = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalMinutes) % 60;
        int secs = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalSeconds) % 60;

        return (hours + " : " + mins + " : " + secs);
    }

    public (float, float, float) SliderTimeTillNextClaim()
    {
        int hours = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalHours);
        int mins = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalMinutes) % 60;
        int secs = Mathf.FloorToInt((float)(DateTime.Today.AddDays(1) - DateTime.Now).TotalSeconds) % 60;

        return (hours, mins, secs);
    }

    public void NewDay()
    {
        m_todayClaimed = false;
        m_button.interactable = true;
        m_text.text = "Ready!";
        m_slider.value = m_maxSliderTime;
    }
}
