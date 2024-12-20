using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RapportScore : MonoBehaviour {
    [Header("References")]
    [SerializeField] private ScoreManager scoreManager; // Reference to the ScoreManager script

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI rapportTextPercentageTMP; // Text to display the score percentage
    [SerializeField] private TextMeshProUGUI adviceTextTMP; // Text to display advice based on ratings
    [SerializeField] private TextMeshProUGUI graduateTMP; // Text to display graduation result

    [Header("Advice Settings")]
    [SerializeField] private string[] adviceTexts; // Array of advice strings based on predominant ratings

    private void Start() {
        if (scoreManager == null) {
            Debug.LogError("ScoreManager reference is not set in RapportScore!");
            return;
        }
    }

    public void UpdateRapportUI() {
        // Get the current score from ScoreManager
        int currentScore = scoreManager.GetCurrentScore();
        float percentage = ((float)currentScore / scoreManager.maxPoints) * 100f;

        // Update rapport percentage text
        if (rapportTextPercentageTMP != null) {
            // Format percentage to 1 decimal place
            rapportTextPercentageTMP.text = $"{percentage}%";
        }

        // Get the count of each rating category
        Dictionary<Rating, int> ratingCounts = scoreManager.CountRatings();

        // Determine which rating category is predominant
        Rating predominantRating = GetPredominantRating(ratingCounts);

        // Update advice text based on the predominant rating
        if (adviceTextTMP != null && adviceTexts.Length >= 4) {
            if (predominantRating == Rating.Good && currentScore > 70) {
                adviceTextTMP.text = adviceTexts[0]; // Example: "Great job! Keep it up!"
            } else if (predominantRating == Rating.Good) {
                adviceTextTMP.text = adviceTexts[1]; // Example: "You're doing okay, but there's room for improvement."
            } else if (predominantRating == Rating.Neutral) {
                adviceTextTMP.text = adviceTexts[1]; // Example: "You're doing okay, but there's room for improvement."
            } else if (predominantRating == Rating.Bad) {
                adviceTextTMP.text = adviceTexts[2]; // Example: "You're struggling, try harder."
            } else if (predominantRating == Rating.Worst) {
                adviceTextTMP.text = adviceTexts[3]; // Example: "Things are not looking good. Focus on improving."
            } else {
                adviceTextTMP.text = "No ratings available.";
            }
        }

        // Update graduation result based on score percentage
        if (graduateTMP != null) {
            graduateTMP.text = percentage < 70 ? "FAIL" : "SUCCESS";
        }
    }

    private Rating GetPredominantRating(Dictionary<Rating, int> ratingCounts) {
        Rating predominantRating = Rating.None;
        int maxCount = 0;

        foreach (KeyValuePair<Rating, int> entry in ratingCounts) {
            if (entry.Value > maxCount) {
                maxCount = entry.Value;
                predominantRating = entry.Key;
            }
        }

        return predominantRating;
    }
}
