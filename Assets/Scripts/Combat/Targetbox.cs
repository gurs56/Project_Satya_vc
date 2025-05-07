using UnityEngine;

public class TargetBox : MonoBehaviour
{
    [SerializeField]
    private Health health;

    public Health Health { get => this.health; private set => this.health =  value ; }
}
