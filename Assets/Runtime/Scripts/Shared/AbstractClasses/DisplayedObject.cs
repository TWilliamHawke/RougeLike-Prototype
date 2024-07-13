using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisplayedObject : ScriptableObject
{
    [LocalisationKey]
    [SerializeField] string _displayName;
    [SpritePreview]
    [SerializeField] Sprite _icon;

    public Sprite icon => _icon;
    public string displayName => _displayName;

}
