using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private UIController _dialogue;
    [SerializeField] private NPC _currentNPC;
    [SerializeField] private GameObject _key;
    private DialogueNode _dialogueStartNode;
    private DialogueNode _currentNode;
    private int _currentLine = 0;
    private bool _waitingForPlayerResponse;
    public bool _appear = false;

    

    public void Talk(NPC npc)
    {
        _currentNPC = npc;

        if (_currentNPC.GetName() == "Sister" //end condition
            && Player.Instance._inventoryString.Contains("Puddle")
            && Player.Instance._inventoryString.Contains("Cup"))
        {
            _dialogueStartNode = _currentNPC._dialogueStartingNodes[2];
        }
        else if (_currentNPC.GetName() == "Neighbor" //end condition
            && Player.Instance._inventoryString.Contains("Key"))
        {
            string _nameObject = Player.Instance.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Item>().GetName();
            Debug.Log(_nameObject);
            _dialogueStartNode = _currentNPC._dialogueStartingNodes[1];
            _appear = true;
            Player.Instance._inventory.Remove(_key);
            Player.Instance._inventoryString.Remove("Key");
            _dialogue._inventoryText.text = _dialogue._inventoryText.text.Replace ($"\nKey", string.Empty);
            if (_nameObject == "Key")
            {
                Destroy(Player.Instance.GetComponent<Transform>().GetChild(0).gameObject.GetComponent<Transform>().GetChild(0).gameObject);
                Player.Instance._itemOut = false;
            }

        }
        else if (_currentNPC._playerHasKeyItem)
        {
            Debug.Log("player has key item for npc");
            _dialogueStartNode = _currentNPC._dialogueStartingNodes[1];
        }
        else
        {
            Debug.Log("player doesn't have key item for npc");
            _dialogueStartNode = _currentNPC._dialogueStartingNodes[0];
        }


        _currentNode = _dialogueStartNode;
        _currentNPC._npcReaction = NPCSpeech.Talking;
        _dialogue.ShowDialogue(_currentNode._lines[_currentLine]);

    }

    public void AdvanceDialogue()
    {
        Debug.Log("advanced dialogue");
        if (_currentLine < _currentNode._lines.Length)
        {
            // if we still have NPC lines left, keep playing NPC lines
            _dialogue.ShowDialogue(_currentNode._lines[_currentLine]);
            _currentLine++;
        }
        else if (_currentNode._playerReplyOptions != null && _currentNode._playerReplyOptions.Length > 0)
        {
            // show player dialogue options, if there are any
            _waitingForPlayerResponse = true;
            _dialogue.ShowPlayerOptions(_currentNode._playerReplyOptions);
        }
        else
        {
            // if there are no NPC or player lines left, close dialogue UI
            EndDialogue();
        }
    }

    public void SelectedOption(int option)
    {
        _currentLine = 0;
        _waitingForPlayerResponse = false;

        _currentNode = _currentNode._npcReplies[option];
        AdvanceDialogue();
    }

    private void EndDialogue()
    {
        Debug.Log("ended dialogue");

        _currentNPC._npcReaction = NPCSpeech.Idle; // put state off talking
        _waitingForPlayerResponse = false;
        _currentNode = _dialogueStartNode;
        _currentLine = 0;

        _dialogue.HideDialogue();
    }
}
