using UnityEngine;

public class GunkDebugController : MonoBehaviour
{
    GunkPainter gunkPainter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
        gunkPainter = GetComponentInChildren<GunkPainter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            gunkPainter.Explode();
        }
    }
}
