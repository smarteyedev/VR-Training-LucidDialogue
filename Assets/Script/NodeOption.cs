using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Option", menuName = "Dialogue/Dialogue Option")]
public class NodeOption : ScriptableObject {
    [TextArea(1, 2)]
    public string text;
    public DialogueNode nextDialogue;
    public Rating rating;
    public AudioClip audioClip;

    public int GetScore() {
        switch (rating) {
            case Rating.Good:
            return 10;
            case Rating.Neutral:
            return 5;
            case Rating.Bad:
            return -5;
            case Rating.Worst:
            return -10;
            default:
            return 0;
        }
    }
}

public enum Rating {
    Good,
    Neutral,
    Bad,
    Worst,
    None
}

