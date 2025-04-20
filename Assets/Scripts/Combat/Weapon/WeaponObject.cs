using System;
using UnityEngine;
/// <summary>
/// Determines how the weapon behaves and looks
/// </summary>

[CreateAssetMenu(fileName = "WeaponObject", menuName = "Project Satya/WeaponObject")]
public class WeaponObject : ScriptableObject {
    public GameObject weaponPrefab;

    /// <summary>
    /// The layers that the can hit
    /// </summary>
    public LayerMask damageableLayers;

    public float damage = 0f;

    /// <summary>
    /// Called when the enity attempts to use the weapon
    /// </summary>
    /// <param name="weapon">The weapon that is bieng attempted to use</param>
    /// <returns>Whether or not the weapon was able to be used</returns>
    public virtual bool HandleWeaponAttack(Weapon weapon) { return false; }

    /// <summary>
    /// Called when the weapon hits something that it is allowed to hit
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="target"></param>
    /// <returns>Whether or not the collision was successfully handled</returns>
    public virtual bool HandleWeaponCollision(Weapon weapon, Entity target) { return false; }


}
