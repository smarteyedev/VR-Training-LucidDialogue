using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Dialogue Node")]
public class DialogueNode : ScriptableObject {
    [TextArea(3, 10)]
    public string npcDialogue;
    public List<NodeOption> options;
    public AudioClip audioClip;

}
