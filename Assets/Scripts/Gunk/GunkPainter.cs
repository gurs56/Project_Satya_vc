using UnityEngine;

public class GunkPainter : MonoBehaviour
{
    [SerializeField]
    float explosionRadius = 1f;
    [SerializeField]
    Transform explosionOffset;

    [SerializeField]
    LayerMask paintableLayers;


    private GunkHandler gunkHandler;

    public void Explode() {
        Collider[] hitColliders = Physics.OverlapSphere(explosionOffset.position, explosionRadius, paintableLayers);
        foreach (var hitCollider in hitColliders) {
            //GunkHandler.instance.GetTex
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(explosionOffset.position, new Vector3(0.01f, 0.01f, 0.01f));

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(explosionOffset.position, explosionRadius);

    }
}
