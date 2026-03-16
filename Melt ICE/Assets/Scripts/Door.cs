using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private Vector3 _position; // coordinates for where the door should take player

    public Vector3 GetPosition()
    { return _position; }
}
