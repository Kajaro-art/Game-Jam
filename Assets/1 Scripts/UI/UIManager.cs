using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] int currentScore;

    [SerializeField] TMP_Text _scoreText;
    int localPlayerHealthTracker = 2;
    int localEggTracker = 0;

    public Image basketImage;
    public List<Sprite> basketSprites = new();
    public List<Image> healthTracker = new();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }

        _scoreText.text = currentScore.ToString();
    }

    private void OnEnable()
    {
        EggController.IncreaseScore += IncreaseScore;
        DamageGiver.OnPlayerHit += DecreaseScore;
        PlayerHealth.OnHealthLost += UpdatePlayerHealth;
        PlayerEggController.OnLargeEggAdded += UpdateLargeEggUI;
        PlayerEggController.OnLargeEggLost += UpdateLargeEggUI;
    }

    void UpdatePlayerHealth()
    {
        if(localPlayerHealthTracker > 0)
        {
            healthTracker[localPlayerHealthTracker].enabled = false;
            localPlayerHealthTracker--;
        }
    }

    void UpdateLargeEggUI(int egg)
    {
        localEggTracker += egg;
        basketImage.sprite = basketSprites[localEggTracker];
    }

    void IncreaseScore(int score)
    {
        if (_scoreText != null)
        {
            currentScore += score;
            FormattableString message = $"{currentScore:N0}";
            string formattedString = FormattableString.Invariant(message);
            _scoreText.text = formattedString;
        }
    }

    void DecreaseScore(int score)
    {
        if (_scoreText != null)
        {
            currentScore -= score;
            FormattableString message = $"{currentScore:N0}";
            string formattedString = FormattableString.Invariant(message);
            _scoreText.text = formattedString;
        }
    }

    private void OnDisable()
    {
        EggController.IncreaseScore -= IncreaseScore;
        DamageGiver.OnPlayerHit -= DecreaseScore;
        PlayerHealth.OnHealthLost -= UpdatePlayerHealth;
    }
}