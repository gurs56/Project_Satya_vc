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
            print(col);
        }
        //col.excludeLayers = ~0;
        col.includeLayers = weaponObject.damageableLayers;

        parentEntity = GetComponentInParent<Entity>();
    }
}
