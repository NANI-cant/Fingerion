using System.Collections;
using System.Collections.Generic;
using Architecture.Services.AssetProviding;
using Architecture.Services.Factories;
using Architecture.Services.General;
using Gameplay.Environment.BackgroundGeneration;
using Gameplay.Environment.ChunkGeneration.Impl;
using Metric;
using UnityEngine;

namespace Architecture.Services.Gameplay.Impl {
    public class BiomGenerationService : IBiomGenerationService {
        private readonly IGameplayFactory _gameplayFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IDestroyProvider _destroyProvider;
        private readonly ChunkTranslation _chunkTranslation;
        private readonly Background _background;
        private readonly IBiomService _biomService;
        private readonly Chunk _startChunk;
        private readonly Queue<List<Chunk>> _biomChunks = new();
        private readonly Queue<BackgroundTile> _biomBackgrounds = new();
        private readonly IPlayerMetric _playerMetric;
        private readonly IGameMetric _gameMetric;

        private Coroutine _generationRoutine;
        private float _lastChunkExcess = 0;
        private Chunk _lastChunk;
        private BackgroundTile _lastTile;

        public BiomGenerationService(
            IGameplayFactory gameplayFactory,
            ICoroutineRunner coroutineRunner,
            IDestroyProvider destroyProvider,
            ChunkTranslation chunkTranslation,
            Background background,
            IBiomService biomService,
            Chunk startChunk,
            IMetricProvider metricProvider
        ) {
            _gameplayFactory = gameplayFactory;
            _coroutineRunner = coroutineRunner;
            _destroyProvider = destroyProvider;
            _chunkTranslation = chunkTranslation;
            _background = background;
            _biomService = biomService;
            _startChunk = startChunk;
            _playerMetric = metricProvider.PlayerMetric;
            _gameMetric = metricProvider.GameMetric;

            _chunkTranslation.AddChunk(_startChunk);
            _lastChunk = _startChunk;
            NextBiom();
        }
        
        public void Start() {
            _generationRoutine = _coroutineRunner.StartCoroutine(ExecuteGeneration());
        }

        public void Stop() {
            _coroutineRunner.StopCoroutine(_generationRoutine);
        }

        private IEnumerator ExecuteGeneration() {
            while (true) {
                NextBiom();

                float chunksLength = 0;
                foreach (var chunk in _biomChunks.Peek()) {
                    chunksLength += chunk.Length;
                }
                yield return new WaitForSeconds(chunksLength / _playerMetric.Speed);
            }
        }

        private void NextBiom() {
            GenerateBackground();
            GenerateChunks();

            DestroyOldBiom();
            _biomService.Next();
        }

        private void GenerateBackground() {
            Vector2 tilePosition = _lastTile == null
                ? _background.transform.position
                : _lastTile.transform.position + Vector3.right * _lastTile.Length;
            
            _lastTile = _gameplayFactory.CreateTile(_background);
            _lastTile.transform.position = tilePosition;
            
            _biomBackgrounds.Enqueue(_lastTile);
        }

        private void GenerateChunks() {
            List<Chunk> chunks = new();
            float totalLength = 0;
            if (_lastChunk == _startChunk) {
                chunks.Add(_lastChunk);
                totalLength += _lastChunk.Length;
            }

            float tileSpeed = _gameMetric.CalculateParallaxSpeed(_playerMetric.Speed, _background.transform.position.z);
            float translationTime = _lastTile.Length / tileSpeed;
            float availableLength = _playerMetric.Speed * translationTime - _lastChunkExcess;
            
            while (totalLength < availableLength) {
                var newChunk = SpawnChunk(); 
                chunks.Add(newChunk);
                totalLength += newChunk.Length;
            }
            Debug.Log($"total {totalLength}");
            Debug.Log($"available {availableLength}");
            Debug.Log($"lastExcess {_lastChunkExcess}");
            
            _lastChunkExcess = totalLength - availableLength;
            _biomChunks.Enqueue(chunks);
        }

        private void DestroyOldBiom() {
            const int biomBuffer = 3;
            
            if (_biomChunks.Count >= biomBuffer) {
                var chunks = _biomChunks.Dequeue();
                foreach (var chunk in chunks) {
                    _chunkTranslation.RemoveChunk(chunk);
                    _destroyProvider.Destroy(chunk.gameObject);
                }
            }

            if (_biomBackgrounds.Count >= biomBuffer) {
                var background = _biomBackgrounds.Dequeue();
                _destroyProvider.Destroy(background.gameObject);
            }
        }

        private Chunk SpawnChunk() {
            Chunk chunk = _gameplayFactory.CreateChunk(_lastChunk);
            chunk.ConcatTo(_lastChunk);
            _chunkTranslation.AddChunk(chunk);
            _lastChunk = chunk;
            return chunk;
        }
    }
}