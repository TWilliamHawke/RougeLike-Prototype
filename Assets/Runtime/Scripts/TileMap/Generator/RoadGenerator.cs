using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

using Rng = System.Random;

namespace Map.Generator
{
    public class RoadGenerator : IMapGenerator
    {
        int[,] IMapGenerator.intMap => _intMap;
        Tilemap _tileMap;
        Vector3Int IMapGenerator.playerSpawnPos => _playerSpawnPos;

        MapSize IMapGenerator.mapSize => _mapSize;

        RoadConfig _config;
        Rng _rng;
        int[,] _intMap;

        Vector3Int _playerSpawnPos;
        MapSize _mapSize;

        public RoadGenerator(RoadConfig pathConfig, Tilemap tileMap)
        {
            _config = pathConfig;
            _tileMap = tileMap;

            _rng = new Rng(_config.seed);
            int pathLength = _rng.Next(_config.minLength, _config.maxLength);

            _mapSize = new MapSize(_config.totalWidth, pathLength);
            _playerSpawnPos = new Vector3Int(_mapSize.width / 2, 0, 0);
        }

        void IMapGenerator.StartGeneration()
        {
            _tileMap.ClearAllTiles();
            _intMap = new int[_mapSize.width, _mapSize.height];

            for (int y = 0; y < _mapSize.height; y++)
            {
                FillWithDefaultTile(y);
                SetBorderTile(_config.voidWidth, y);
                SetBorderTile(_config.voidWidth + 1, y);
                SetBorderTile(_mapSize.width - _config.voidWidth - 1, y);
                SetBorderTile(_mapSize.width - _config.voidWidth - 2, y);

            }

            CreateRoad();
            CreateSites();
        }

        private void FillWithDefaultTile(int y)
        {
            for (int x = 0; x < _mapSize.width; x++)
            {
                if (x < _config.voidWidth + _config.borderWidth) continue;
                if (x > _mapSize.width - _config.voidWidth - _config.borderWidth) continue;
                _intMap[x, y] = 1;
                var position = new Vector3Int(x, y, 0);
                _tileMap.SetTile(position, _config.defaultTile);
            }
        }

        private void CreateSites()
        {
            int posX = _config.voidWidth + _config.siteWidth / 2;
            int posY = _rng.Next(_config.minDistanceBetweenSites, _config.maxDistanceBetweenSites);
            SiteTemplate template = _config.siteTemplates.GetRandom();

            _tileMap.DestroyChildren();

            var site = _tileMap.CreateChild(_config.sitePrefab, new Vector3(posX, posY, 0));
            site.SetTemplate(template);
            site.SpawnEnemies(_rng);

            if (!template.siteTile) return;

            ChangeCenterTiles(posX, posY, template);

        }

        private void ChangeCenterTiles(int centerX, int centerY, SiteTemplate template)
        {
            for (int i = centerX - template.tilesWidth / 2; i <= centerX + template.tilesWidth / 2; i++)
            {
                for (int j = centerY - template.tilesHeight / 2; j <= centerY + template.tilesHeight / 2; j++)
                {
                    _tileMap.SetTile(new Vector3Int(i, j, 0), template.siteTile);
                }
            }
        }

        private void SetBorderTile(int x, int y)
        {
            _intMap[x, y] = 0;
            var position = new Vector3Int(x, y, 0);
            _tileMap.SetTile(position, _config.borderTile);
        }

        void CreateRoad()
        {
            var roadPosition = new int[_mapSize.height];
            int minRoadPosition = _config.siteWidth + _config.voidWidth + _config.borderWidth + _config.emptyWidth;
            int maxRoadPosition = minRoadPosition + _config.roadWidth;
            int curveDirection = 1;

            for (int i = 0; i < roadPosition.Length; i++)
            {
                roadPosition[i] = (minRoadPosition + maxRoadPosition) / 2;
            }

            for (int j = 0; j < _config.roadCurvesCount; j++)
            {
                int startCurve = _rng.Next(_mapSize.height);
                int endCurve = _rng.Next(startCurve, _mapSize.height);

                for (int k = startCurve; k <= endCurve; k++)
                {
                    roadPosition[k] = Mathf.Clamp(roadPosition[k] + curveDirection, minRoadPosition, maxRoadPosition);
                }

                curveDirection *= -1;
            }

            int prevPosition = roadPosition[0];
            for (int y = 0; y < roadPosition.Length; y++)
            {
                int currentPosition = roadPosition[y];
                _tileMap.SetTile(new Vector3Int(currentPosition, y, 0), _config.roadTile);

                if (currentPosition != prevPosition)
                {
                    if (y + 1 == roadPosition.Length || roadPosition[y + 1] == prevPosition)
                    {
                        roadPosition[y] = prevPosition;
                    }

                    int direction = (int)Mathf.Sign(currentPosition - prevPosition);

                    if (direction != 0)
                    {
                        int x = prevPosition;

                        while (x != currentPosition && x > 0 && x < _mapSize.width)
                        {
                            _tileMap.SetTile(new Vector3Int(x, y, 0), _config.roadTile);
                            x += direction;
                        }
                    }

                    prevPosition = currentPosition;
                }

            }
        }
    }
}

