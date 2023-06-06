using System;
using UnityEngine;

public static class Extensions {
    public static MonoBehaviour DoForComponentsInChildren<T>(this MonoBehaviour monoBehaviour, Action<T> action) {
        monoBehaviour.gameObject.DoForComponentsInChildren(action);
        return monoBehaviour;
    }
    
    public static GameObject DoForComponentsInChildren<T>(this GameObject gameObject, Action<T> action) {
        foreach (T component in gameObject.GetComponentsInChildren<T>()) {
            action.Invoke(component);   
        }

        return gameObject;
    }
}