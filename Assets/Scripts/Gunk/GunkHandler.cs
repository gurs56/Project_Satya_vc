using System.Collections.Generic;
using UnityEngine;

public class GunkHandler : MonoBehaviour
{
    Dictionary<int, Texture2D> splatMaskTextures = new();

    public static GunkHandler instance { get; private set; }
    

    private void Awake() {

        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    public Texture2D GetTextureForObject(GameObject o) {
        var id = o.GetInstanceID();

        if (!splatMaskTextures.ContainsKey(id)) {
            //create new textures
            //splatMaskTextures.Add(id, new Texture2D());
        }


        return splatMaskTextures[id];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
