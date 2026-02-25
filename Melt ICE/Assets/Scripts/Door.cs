using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string _sceneName; 
    [SerializeField] private Vector3 _position; // coordinates for where the door should take player

    public string GetSceneName()
    { return _sceneName; }

    public Vector3 GetPosition()
    { return _position; }
}
