namespace Architecture.Services.General.Impl {
    public class UnityTimeProvider : ITimeProvider {
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float Time => UnityEngine.Time.time;
        public float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public float UnscaledTime => UnityEngine.Time.unscaledTime;
    }
}
