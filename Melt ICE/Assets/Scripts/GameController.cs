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
    public delegate void PickedUpDelegate();

    public event PickedUpDelegate PickedUp;


    public void Start()
    {
        Locator.Instance.Player.Interacted += PlayerInteracted;
    }


    private void PlayerInteracted(GameObject inter)
    {
        // name of the object we interacted with
        Debug.Log("player interacted with " + inter.GetComponent<Interactable>().GetName());


        // find out if inter is an item or NPC and act accordingly
        if (inter.GetComponent<Door>() == null && inter.GetComponent<NPC>() == null)
        {
            // if the interactable is an item
            PickedUp?.Invoke();

            inter.gameObject.SetActive(false); //remove from scene to prevent further interaction
            Locator.Instance.Player._inventory.Add(inter.GetComponent<Interactable>().GetName()); //add to inventory

        }
        else if (inter.GetComponent<NPC>() == null) // if the interactable is a door
        {
            // load correct scene
            if (SceneManager.GetActiveScene().name != inter.GetComponent<Door>().GetSceneName())
            {
                SceneManager.LoadScene(inter.GetComponent<Door>().GetSceneName());
            }

            // put in correct location
            Locator.Instance.Player.GetComponent<Transform>().SetPositionAndRotation(
                inter.GetComponent<Door>().GetPosition(), Quaternion.identity);

        }
        else // if the interactable is an NPC
        {

        }
    }
}