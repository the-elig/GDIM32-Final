using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum _objective
{
    empty, cup, key, flamethrower
}

public class GameController : MonoBehaviour
{
    [SerializeField] private List<string> _items;
    [SerializeField] private NPC[] _NPCs;


    public void Start()
    {
        Debug.Log("GameControl start");

        Locator.Instance.Player.Interacted += PlayerInteracted;
    }


    private void PlayerInteracted(GameObject inter)
    {
        // name of the object we interacted with
        Debug.Log("player interacted with" + inter.GetComponent<Interactable>().GetName());


        // find out if inter is an item or NPC and act accordingly
        if (inter.GetComponent<Item>() != null) // if the interactable is an item
        {
            _items.Add(inter.GetComponent<Interactable>().GetName()); //add to inventory
            inter.gameObject.SetActive(false); //remove from scene to prevent further interaction
        }
        else // if the interactable is an NPC
        {

        }
    }
}