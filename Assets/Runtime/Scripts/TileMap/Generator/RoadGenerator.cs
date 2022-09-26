using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

using Rng = System.Random;

namespace Map.Generator
{
    public partial class RoadGenerator : MapGenerator, IInjectionTarget
    {

        public override LocationMapData StartGeneration(Tilemap tilemap)
        {
            _tileMap = tilemap;
            _tileMap.ClearAllTiles();
            _rng = new Rng(_seed);
            int totalWidth = (_emptyWidth + _siteWidth + _borderWidth + _voidWidth) * 2 + _roadWidth;
            int pathLength = _rng.Next(_minLength, _maxLength);

            _mapData = new LocationMapData();
            _mapData.width = totalWidth;
            _mapData.height = pathLength;
            _mapData.walkabilityMap = new int[totalWidth, pathLength];
            _mapData.playerSpawnPos = new Vector3Int(totalWidth / 2, 0, 0);


            for (int y = 0; y < _mapData.height; y++)
            {
                FillWithDefaultTile(y);
                SetBorderTile(_voidWidth, y);
                SetBorderTile(_voidWidth + 1, y);
                SetBorderTile(_mapData.width - _voidWidth - 1, y);
                SetBorderTile(_mapData.width - _voidWidth - 2, y);
            }

            CreateRoad();
            CreateSites();

            return _mapData;
        }

        void IInjectionTarget.FinalizeInjection()
        {
            int posX = _voidWidth + _siteWidth / 2;
            int posY = _rng.Next(_minDistanceBetweenSites, _maxDistanceBetweenSites);
            SiteTemplate template = _siteTemplates.GetRandom();

            _tileMap.DestroyChildren();

            var site = _mapObjectsManager.CreateSite(new Vector3(_mapData.width / 2, 10, 0));
            site.SetTemplate(template, _rng);

            if (!template.siteTile) return;

            ChangeCenterTiles(posX, posY, template);
        }

        private void FillWithDefaultTile(int y)
        {
            for (int x = 0; x < _mapData.width; x++)
            {
                if (x < _voidWidth + _borderWidth) continue;
                if (x > _mapData.width - _voidWidth - _borderWidth) continue;
                _mapData.walkabilityMap[x, y] = 1;
                var position = new Vector3Int(x, y, 0);
                _tileMap.SetTile(position, _defaultTile);
            }
        }

        private void CreateSites()
        {
            _objectsManagerInjector.AddInjectionTarget(this);
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
            _mapData.walkabilityMap[x, y] = 0;
            var position = new Vector3Int(x, y, 0);
            _tileMap.SetTile(position, _borderTile);
        }

        void CreateRoad()
        {
            var roadPosition = new int[_mapData.height];
            int minRoadPosition = _siteWidth + _voidWidth + _borderWidth + _emptyWidth;
            int maxRoadPosition = minRoadPosition + _roadWidth;
            int curveDirection = 1;

            for (int i = 0; i < roadPosition.Length; i++)
            {
                roadPosition[i] = (minRoadPosition + maxRoadPosition) / 2;
            }

            for (int j = 0; j < _roadCurvesCount; j++)
            {
                int startCurve = _rng.Next(_mapData.height);
                int endCurve = _rng.Next(startCurve, _mapData.height);

                for (int k = startCurve; k <= endCurve; k++)
                {
                    roadPosition[k] = Mathf.Clamp(roadPosition[k] + curveDirection, minRoadPosition, maxRoadPosition);
                }

                curveDirection *= -1;
            }

            //Fix diagonal roads
            //=====***** => ======****
            //*****=====    *****=====
            int prevPosition = roadPosition[0];
            for (int y = 0; y < roadPosition.Length; y++)
            {
                int currentPosition = roadPosition[y];
                _tileMap.SetTile(new Vector3Int(currentPosition, y, 0), _roadTile);

                if (currentPosition == prevPosition)continue;

                //prevent 1 tile length curve
                if (y + 1 == roadPosition.Length || roadPosition[y + 1] == prevPosition)
                {
                    roadPosition[y] = prevPosition;
                }

                int direction = (int)Mathf.Sign(currentPosition - prevPosition);

                //fill empty space with road tiles
                if (direction != 0)
                {
                    int x = prevPosition;

                    while (x != currentPosition && x > 0 && x < _mapData.width)
                    {
                        _tileMap.SetTile(new Vector3Int(x, y, 0), _roadTile);
                        x += direction;
                    }
                }

                prevPosition = currentPosition;
            }
        }

    }
}

