using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public WeaponObject weaponObject;

    public Entity parentEntity { get; private set; }

    private void Awake() {
        Collider col;
        TryGetComponent(out col);
        if (col == null) { 
            col = GetComponentInChildren<Collider>();
        }
        col.excludeLayers = ~0;
        col.includeLayers = weaponObject.damageableLayers;

        parentEntity = GetComponentInParent<Entity>();
    }

    private void OnTriggerEnter(Collider other) {
        var target = other.GetComponent<Entity>();
        if (target != null) {
            weaponObject.HandleWeaponCollision(this, target);
        }
    }
}
