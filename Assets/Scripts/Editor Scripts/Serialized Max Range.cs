using System;
using UnityEngine;

/// <summary>Unfinished</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public sealed class SerializedMaxRange : PropertyAttribute {
    public readonly float min;

    public readonly float max;

    //
    // Summary:
    //     Attribute used to make a float or int variable in a script be restricted to a
    //     specific range.
    //
    // Parameters:
    //   min:
    //     The minimum allowed value.
    //
    //   max:
    //     The maximum allowed value.
    public SerializedMaxRange(float min, float max) {
        this.min = min;
        this.max = max;
    }
}

