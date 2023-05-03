using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ProjectileTypeSO : ScriptableObject
{
    public Type type;

    public int level;

    public float health;

    public float speed;

    public float damage;

    public Color normalColor;

    public Color popupColor;
    public enum Type
    {
        Friend,
        Enemy
    }
}
