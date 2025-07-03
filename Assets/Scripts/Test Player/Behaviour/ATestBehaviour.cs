using UnityEngine;

public abstract class ATestBehaviour<T> : MonoBehaviour {
    /// <summary>
    /// Call <c>Act</c> with no data.<br />
    /// Shorthand for <c>Act&lt;object&gt;(null)</c>.
    /// </summary>
    public void Act() {
       Act((T)(null as object));
    }

    public abstract void Act(T data);
}