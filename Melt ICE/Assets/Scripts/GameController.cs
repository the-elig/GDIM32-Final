using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.LightingExplorerTableColumn;
public enum _objective
{
    empty, cup, key, flamethrower
}

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController UI;
    [SerializeField] private DialogueController _dialogue;
    
    [SerializeField] private GameObject _flamethrower;
    private bool _spawnedFlamethrower = false;

    public delegate void PickedUpDelegate();
    public event PickedUpDelegate PickedUp;
   

    void Start()
    {
        Player.Instance.Interacted += PlayerInteracted;

    }

    private void Update()
    {
        // flamethrower spawning logic
        if (SceneManager.GetActiveScene().name == "Main Scene"
           && Player.Instance._inventoryString.Contains("Key") && !_spawnedFlamethrower)
        {
            // if in the correct scene and hasKey and the flamethrower hasn't been spawned
            Vector3 myPosition = new Vector3(65.6100006f, 0.200000003f, 48.5099983f);
            Quaternion myRotation = new Quaternion(-0.280872971f, -0.648930192f, -0.648930192f, 0.28087303f);
            
            Instantiate(_flamethrower, myPosition, myRotation);

            _spawnedFlamethrower = true;
        }
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
            UI._inventoryText.text = UI._inventoryText.text + $"\n{_name}";

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
        else // if the interactable is an NPC
        {
            if (inter.GetComponent<NPC>()._npcReaction != NPCSpeech.Talking)
            {
                Debug.Log("talking with npc");
                _dialogue.Talk(inter.GetComponent<NPC>());
                Player.Instance.SetCanMoveCamera(false);
            }
            else
            {
                Debug.Log("something has gone very wrong.");
            }
        }
    }
}