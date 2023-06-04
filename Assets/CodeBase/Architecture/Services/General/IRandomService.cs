namespace Architecture.Services.General {
    public interface IRandomService {
        public float Range(float min, float max);
        public int Range(int min, int max);
        public int Int();
        public float Float();
        bool Chance(float value);
        bool ChancePercents(float value);
        bool Chance(float from, float to, float value);
    }
}
