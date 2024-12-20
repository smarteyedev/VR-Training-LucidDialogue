using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Events;
using System.Collections.Generic;
using Tproject.AudioManager;
public class DialogueController : MonoBehaviour {
    [Header("UI Elements")]
    public TextMeshProUGUI npcDialogueText;
    public Button[] optionButtons;
    [Header("Dialogue Data")]
    public DialogueNode startDialogue;
    private DialogueNode currentDialogue;

    [Header("References")]
    public ScoreManager scoreManager;

    [Header("Settings")]
    private float dialogueDelay = 1.0f;
    public float delayAdder = 0.5f;

    [Header("Events")]
    public UnityEvent onConversationEnd;

    private void Start() {
        npcDialogueText.transform.parent.gameObject.SetActive(false);

        if (startDialogue != null) {
            ShowDialogue(startDialogue);
        } else {
            Debug.LogError("No starting dialogue assigned!");
        }
    }

    public void ShowDialogue(NodeOption nodeOption) {
        StopAllCoroutines();
        StartCoroutine(DisplayDialogueWithDelay(nodeOption.nextDialogue));
    }

    public void ShowDialogue(DialogueNode nodeOption) {
        StopAllCoroutines();
        StartCoroutine(DisplayDialogueWithDelay(nodeOption));
    }

    IEnumerator SetActiveFalseWithAnimationDialogNpc(GameObject gameObject) {
        gameObject.GetComponent<ScaleYAnimation>().AnimateYScaleClose();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    private IEnumerator DisplayDialogueWithDelay(DialogueNode dialogueNode) {
        StartCoroutine(SetActiveFalseWithAnimationDialogNpc(npcDialogueText.transform.parent.gameObject));
        npcDialogueText.text = "";


        foreach (var button in optionButtons) {
            button.gameObject.SetActive(false);
        }

        currentDialogue = dialogueNode;

        yield return new WaitForSeconds(dialogueDelay + delayAdder);
        if (currentDialogue.npcDialogue != "") {
            npcDialogueText.transform.parent.gameObject.SetActive(true);
            npcDialogueText.transform.parent.gameObject.GetComponent<ScaleYAnimation>().AnimateYScaleOpen();
            npcDialogueText.text = currentDialogue.npcDialogue;
        } else {
            npcDialogueText.transform.parent.gameObject.SetActive(false);
            Debug.LogError("No dialogue text for npc found for this node!");
        }

        PlayDialogSfx(currentDialogue);
        yield return new WaitForSeconds(dialogueDelay + delayAdder);
        optionButtons[0].transform.parent.gameObject.SetActive(true);

        List<NodeOption> randomizedOptions = GetRandomizedOptions(currentDialogue.options);

        for (int i = 0; i < optionButtons.Length; i++) {
            if (i < randomizedOptions.Count) {
                optionButtons[i].gameObject.SetActive(true);

                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = randomizedOptions[i].text;

                NodeOption option = randomizedOptions[i];
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionSelected(option));
            } else {
                // Disable unused buttons
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnOptionSelected(NodeOption selectedOption) {
        AudioManager.Instance.PlaySFX("press");

        if (selectedOption.nextDialogue == null) {
            EndConversation();
        } else {
            PlayDialogSfx(selectedOption);
            ShowDialogue(selectedOption);
        }

        if (scoreManager != null) {
            // Store selected option in ScoreManager
            scoreManager.nodeOptions.Add(selectedOption);

            // Process the rating and adjust the score based on the selected option
            Rating selectedRating = selectedOption.rating;
            scoreManager.ProcessNodeOptionsBasedOnRating(selectedOption, selectedOption.audioClip.length);
        } else {
            Debug.LogWarning("ScoreManager is not assigned in DialogueController!");
        }

        optionButtons[0].transform.parent.gameObject.SetActive(false);
    }

    private void PlayDialogSfx(NodeOption node) {
        if (node.audioClip != null) {
            AudioManager.Instance.PlaySFX(node.audioClip);
            dialogueDelay = node.audioClip.length;
        }
    }

    private void PlayDialogSfx(DialogueNode node) {
        if (node.audioClip != null) {
            AudioManager.Instance.PlaySFX(node.audioClip);
            dialogueDelay = node.audioClip.length;
        }
    }

    private void EndConversation() {
        // Clear the NPC text and disable all option buttons
        npcDialogueText.text = "";
        foreach (var button in optionButtons) {
            button.gameObject.SetActive(false);
        }

        // Optionally, you can invoke the UnityEvent to signal the end of the conversation
        onConversationEnd.Invoke();

        // You can also log or perform other actions as needed, like saving state, etc.
        Debug.Log("Conversation has ended.");
    }

    // Utility function to get randomized options
    private List<NodeOption> GetRandomizedOptions(List<NodeOption> originalOptions) {
        // Create a copy of the options
        List<NodeOption> optionsCopy = new List<NodeOption>(originalOptions);

        // Shuffle the options
        for (int i = optionsCopy.Count - 1; i > 0; i--) {
            int randomIndex = Random.Range(0, i + 1);
            NodeOption temp = optionsCopy[i];
            optionsCopy[i] = optionsCopy[randomIndex];
            optionsCopy[randomIndex] = temp;
        }

        // If there are more than 5 options, select the first 5 after shuffling
        if (optionsCopy.Count > 5) {
            optionsCopy = optionsCopy.GetRange(0, 5);
        }

        return optionsCopy;
    }
}
