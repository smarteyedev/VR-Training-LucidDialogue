using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tproject;
using Tproject.AudioManager;
public class ScoreManager : MonoBehaviour {
    [Header("UI Elements")]
    public Slider scoreSlider;
    public TextMeshProUGUI scorePercentageText;

    [Header("Score Settings")]
    public int maxPoints = 100;
    public int currentScore;
    private int initialScore;

    [Header("VFX")]
    public HandleAnimation handleAnimation;
    public SkyboxHandler skyboxHandler;

    [Header("Skybox Settings")]
    public Color positiveColor = Color.white;
    public Color negativeColor = Color.black;
    public float colorAnimationTime = 1f;
    public Ease colorAnimationEase = Ease.OutQuad;

    [Header("NPC Animator")]
    public Animator npcAnimator;

    [Header("Animation Settings")]
    public string[] goodAnimations;
    public string[] badAnimations;

    [Header("Node Options")]
    public List<NodeOption> nodeOptions; // List to store NodeOption objects

    private void Start() {
        initialScore = maxPoints / 2;
        currentScore = initialScore;

        if (scoreSlider != null) {
            scoreSlider.minValue = 0;
            scoreSlider.maxValue = maxPoints;
            scoreSlider.value = currentScore;
        }

        UpdateScoreUI();

        if (skyboxHandler != null) {
            skyboxHandler.SkyboxColor = skyboxHandler.originalSkyboxColor;
        }
    }

    public void AdjustScore(int scoreChange, float delay) {
        StartCoroutine(AdjustScoreWithDelay(scoreChange, delay));
    }

    IEnumerator AdjustScoreWithDelay(int scoreChange, float delay) {
        yield return new WaitForSeconds(delay);

        int newScore = Mathf.Clamp(currentScore + scoreChange, 0, maxPoints);

        if (scoreSlider != null) {
            scoreSlider.DOValue(newScore, 0.5f).SetEase(Ease.OutQuad);
        }

        if (scorePercentageText != null) {
            DOTween.To(() => currentScore, x => {
                currentScore = x;
                UpdatePercentageText();
            }, newScore, 0.5f).SetEase(Ease.OutQuad);
        }

        currentScore = newScore;

        if (scoreChange > 0) {
            AudioManager.Instance.PlaySFX("right");

            handleAnimation.AnimateToBlue();
            if (skyboxHandler != null) {
                skyboxHandler.AnimateSkyboxColor(positiveColor, colorAnimationTime, colorAnimationEase);
            }
            if (npcAnimator != null && goodAnimations.Length > 0) {
                string randomGoodAnimation = goodAnimations[Random.Range(0, goodAnimations.Length)];
                npcAnimator.SetTrigger(randomGoodAnimation);
            }
        } else if (scoreChange < 0) {
            AudioManager.Instance.PlaySFX("wrong");

            handleAnimation.AnimateToRed();
            if (skyboxHandler != null) {
                skyboxHandler.AnimateSkyboxColor(negativeColor, colorAnimationTime, colorAnimationEase);
            }
            if (npcAnimator != null && badAnimations.Length > 0) {
                string randomBadAnimation = badAnimations[Random.Range(0, badAnimations.Length)];
                npcAnimator.SetTrigger(randomBadAnimation);
            }
        }
    }

    private void UpdateScoreUI() {
        if (scoreSlider != null) {
            scoreSlider.value = currentScore;
        }
        UpdatePercentageText();
    }

    private void UpdatePercentageText() {
        if (scorePercentageText != null) {
            float percentage = ((float)currentScore / maxPoints) * 100f;
            scorePercentageText.text = $"{percentage:F1}%";
        }
    }

    public int GetCurrentScore() {
        return currentScore;
    }

    public void ProcessNodeOptionsBasedOnRating(NodeOption selectedOption, float delay) {
        //List<NodeOption> filteredOptions = nodeOptions.FindAll(option => option.rating == rating);

        //foreach (NodeOption option in filteredOptions) {
            //int scoreChange = option.GetScore();
            AdjustScore(selectedOption.GetScore(), delay); // Apply the score change immediately
        //}
    }

    public List<Rating> GetAllRatings() {
        List<Rating> ratings = new List<Rating>();
        foreach (NodeOption option in nodeOptions) {
            ratings.Add(option.rating);
        }
        return ratings;
    }

    public Dictionary<Rating, int> CountRatings() {
        Dictionary<Rating, int> ratingCounts = new Dictionary<Rating, int>();

        foreach (NodeOption option in nodeOptions) {
            if (ratingCounts.ContainsKey(option.rating)) {
                ratingCounts[option.rating]++;
            } else {
                ratingCounts[option.rating] = 1;
            }
        }

        return ratingCounts;
    }

    // New function to process and handle the ratings for each NodeOption
    public void ProcessRatingsAndAdjustScore() {
        // Count the number of ratings for each type
        Dictionary<Rating, int> ratingCounts = CountRatings();

        // Determine the predominant rating
        Rating predominantRating = Rating.None;
        int highestCount = 0;

        foreach (var entry in ratingCounts) {
            if (entry.Value > highestCount) {
                highestCount = entry.Value;
                predominantRating = entry.Key;
            }
        }

        // Process NodeOptions based on the predominant rating
        //ProcessNodeOptionsBasedOnRating(predominantRating,0f);
    }
}
