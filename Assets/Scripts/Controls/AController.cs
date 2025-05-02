using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Links together the behavour and the controls of an entity
/// </summary>
public class Controller : MonoBehaviour {
    //public event Action<CallbackContext> performed {
    //    add => m_OnPerformed.AddCallback(value);
    //    remove => m_OnPerformed.RemoveCallback(value);
    //}
    Control control;

    [UDictionary.Split(30, 70)]
    public UDictionary3 dictionary3;
    [Serializable]
    public class UDictionary3 : UDictionary<Component, Vector3> { }

    [Serializable]
    public class Control : ISerializationCallbackReceiver {
        public Action test;

        public void OnAfterDeserialize() => throw new NotImplementedException();
        public void OnBeforeSerialize() => throw new NotImplementedException();
    }
}
