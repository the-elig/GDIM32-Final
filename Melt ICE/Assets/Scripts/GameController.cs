using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum _objective
{
    empty, cup, key, flamethrower
}

public class GameController : MonoBehaviour
{
    [SerializeField] private NPC[] _NPCs;
    [SerializeField] private NPC _NPCscript;
    [SerializeField] private UIController UI;
    [SerializeField] private DontDestroy DestroyScript;
    public delegate void PickedUpDelegate();

    public event PickedUpDelegate PickedUp;
   

    public void Start()
    {
        Player.Instance.Interacted += PlayerInteracted;
    }


    private void PlayerInteracted(GameObject inter)
    {


        // find out if inter is an item or NPC and act accordingly
        if (inter.GetComponent<Door>() == null && inter.GetComponent<NPC>() == null)
        {
            // if the interactable is an item
            PickedUp?.Invoke();

            float _time = 3.0f;
            _time -= Time.deltaTime;
            inter.gameObject.SetActive(false); //remove from scene to prevent further interaction
            Player.Instance._inventory.Add(inter.GetComponent<Item>().GetPrefab()); //add to inventory
            string _name = inter.GetComponent<Item>().GetName();
            Player.Instance._inventoryString.Add(_name);
            UI._inventoryText.text = $"{_name}";
            if ( _time > 0 )
            {
                UI._notifText.SetActive(true);
            }
            else
            {
                UI._notifText.SetActive(false);
            }

        }
        else if (inter.GetComponent<Door>() != null) // if the interactable is a door
        {
            // load correct scene
            if (SceneManager.GetActiveScene().name != inter.GetComponent<Door>().GetSceneName())
            {
                SceneManager.LoadScene(inter.GetComponent<Door>().GetSceneName());
            }

            // put in correct location
            Player.Instance.GetComponent<Transform>().SetPositionAndRotation(
                inter.GetComponent<Door>().GetPosition(), Quaternion.identity);

        }
        else // Interacting with NPC
        {
            if (inter.GetComponent<NPC>()._npcReaction != NPCSpeech.Talking)
            {
                inter.GetComponent<NPC>().SetToTalking();
                Player.Instance.SetCanMoveCamera(false);
            }
        }
    }
}