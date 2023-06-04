using UnityEngine;

namespace Architecture.Services.General.Impl {
    public class RandomService : IRandomService {
        private readonly System.Random _random;

        public RandomService(int seed) {
            _random = new System.Random(seed);
        }

        public float Range(float min, float max) {
            float range = max - min;
            if (range <= 0) return min;
            
            float sample = (float)_random.NextDouble();
            return min + (sample * range);
        }

        public int Range(int min, int max) => _random.Next(min, max);
        public int Int() => _random.Next();
        public float Float() => Range(float.MinValue, float.MaxValue);
        public bool Chance(float value) => Chance(0, 1, value);
        public bool ChancePercents(float value) => Chance(0, 100, value);

        public bool Chance(float from, float to, float value) {
            var random = Range(from, to - Mathf.Epsilon);
            return random < value;
        }
    }
}
