using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BehaviourDebug : MonoBehaviour {
    public TextMeshProUGUI textMeshPro;

    private string debugString = "";

    private static Dictionary<string, object> data = new Dictionary<string, object>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void addToDebugTracking(string name, ref object obj) {
        data[name] = obj;
    }

    public static void removeFromDebugTracking(string name) {
        data.Remove(name);
    }

    // Update is called once per frame
    void Update() {
        Action<string, object> addToDebug = (name, obj) => {
            Func<object, string> getStr = (obj) => obj switch {
                Vector2 v2 => v2.ToString("F3"),
                Vector3 v3 => v3.ToString("F3"),
                float f => f.ToString("F3"),
                _ => obj.ToString()
            };

            debugString += name + " " + getStr.Invoke(obj) + "\n";
        };

        foreach (var tup in data) {
          addToDebug.Invoke(tup.Key, tup.Value);
        }

      
        textMeshPro.text = debugString;
        debugString = "";
    }
}
