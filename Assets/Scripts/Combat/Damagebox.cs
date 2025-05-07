using UnityEngine;


public class DamageBox : MonoBehaviour {
    [SerializeField]
    Weapon parentWeapon;

    private void OnTriggerEnter(Collider other) {
        var target = other.GetComponent<TargetBox>();
        if (target != null) {
            parentWeapon.weaponObject.HandleWeaponCollision(parentWeapon, target.Health);
        }
        print("a");
    }
}
