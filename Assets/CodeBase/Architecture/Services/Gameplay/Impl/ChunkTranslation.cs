using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Gameplay.Environment.ChunkGeneration;
using Gameplay.Environment.ChunkGeneration.Impl;
using UnityEngine;
using Zenject;

namespace Architecture.Services.Gameplay.Impl {
    public class ChunkTranslation: ITickable {
        private readonly List<Chunk> _chunks = new ();
        private readonly float _speed;
        
        private bool _canMove;

        public ChunkTranslation(IMetricProvider metricProvider) {
            _speed = metricProvider.PlayerMetric.Speed;
        }

        public void AddChunk(Chunk chunk) => _chunks.Add(chunk);
        public void RemoveChunk(Chunk chunk) => _chunks.Remove(chunk);

        public void Tick() {
            if(!_canMove) return;
            foreach (var chunk in _chunks) {
                chunk.transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);   
            }
        }

        public void Start() => _canMove = true;
        public void Stop() => _canMove = false;
    }
}