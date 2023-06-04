using System.Collections;
using UnityEngine;

namespace Architecture.Services.General {
    public interface ICoroutineRunner {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
    }
}