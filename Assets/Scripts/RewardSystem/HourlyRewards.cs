using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HourlyRewards : MonoBehaviour
{
    [SerializeField] private PlayerStatistics m_playerStats;

    public string m_lastClaimTime;
    DateTime m_lastTime;

    [SerializeField] private Button m_button;
    [SerializeField] private Slider m_slider;
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private TextMeshProUGUI m_collectionText;
    [SerializeField] private GameObject m_collectionScreen;

    float m_maxSliderTime;
    public bool m_hourClaimed;

    private void Start()
    {
        float hour = 3 * 3600;
        float minute = DateTime.MaxValue.Minute * 60;
        float second = DateTime.MaxValue.Second;
        m_maxSliderTime = hour + minute + second;

        string lastTime = m_lastClaimTime;

        if (!string.IsNullOrEmpty(lastTime))
        {
            m_lastTime = DateTime.Parse(lastTime);
        }
        else
        {
            m_lastTime = DateTime.MinValue;
        }

        if (DateTime.Now > m_lastTime.AddHours(4))
        {
            m_button.interactable = true;
            m_slider.value = m_maxSliderTime;
            m_hourClaimed = false;
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
        if (m_hourClaimed)
        {
            m_text.text = TimeTillNextClaim();

            float sliderHour = SliderTimeTillNextClaim().Item1 * 3600;
            float sliderMinute = SliderTimeTillNextClaim().Item2 * 60;
            float sliderSecond = SliderTimeTillNextClaim().Item3;

            float totalTime = sliderHour + sliderMinute + sliderSecond;
            m_slider.value = (m_maxSliderTime - totalTime) / m_maxSliderTime;

            if (totalTime == 0)
            {
                NewHour();
            }
        }
    }

    public void RewardClaimed()
    {
        m_lastClaimTime = DateTime.Now.ToString();

        m_button.interactable = false;
        m_text.text = TimeTillNextClaim();

        RewardScreen();

        if (!m_hourClaimed)
        {
            m_hourClaimed = true;
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

    public void NewHour()
    {
        m_hourClaimed = false;
        m_button.interactable = true;
        m_text.text = "Ready!";
        m_slider.value = m_maxSliderTime;
    }

    public string TimeTillNextClaim()
    {
        DateTime lastTime = DateTime.Parse(m_lastClaimTime);

        int hours = Mathf.FloorToInt((float)(lastTime.AddHours(4) - DateTime.Now).TotalHours);
        int mins = Mathf.FloorToInt((float)(lastTime.AddHours(4) - DateTime.Now).TotalMinutes) % 60;
        int secs = Mathf.FloorToInt((float)(lastTime.AddHours(4) - DateTime.Now).TotalSeconds) % 60;

        return (hours.ToString() + " : " + mins.ToString() + " : " + secs.ToString());
    }

    public (float, float, float) SliderTimeTillNextClaim()
    {
        DateTime lastTime = DateTime.Parse(m_lastClaimTime);

        int hours = Mathf.FloorToInt((float)(lastTime.AddHours(4) - DateTime.Now).TotalHours);
        int mins = Mathf.FloorToInt((float)(lastTime.AddHours(4) - DateTime.Now).TotalMinutes) % 60;
        int secs = Mathf.FloorToInt((float)(lastTime.AddHours(4) - DateTime.Now).TotalSeconds) % 60;

        return (hours, mins, secs);
    }
}
