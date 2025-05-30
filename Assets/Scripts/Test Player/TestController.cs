using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestController : MonoBehaviour {
    public HashSet<TestInstataneousBehaviour> InstantaneousBehaviourSet {
        get;
        private set;
    } = new();

    // A dict of sustained behaviours with a key of the behaviour type and a value of a tuple containing the behaviour itself and the behaviour's inputData type
    public Dictionary<Type, TestSustainedBehaviourParent> SustainedBehaviourDict {
        get;
        private set;
    } = new();
}
