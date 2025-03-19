using System;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class NewScriptableObjectScript : ScriptableObject
{
    public Sprite Sprite;
    public bool IsConsommable;
}
