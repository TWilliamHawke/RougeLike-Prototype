using System.Collections;
using System.Collections.Generic;
using Map.Zones;
using UnityEngine;
using UnityEngine.Tilemaps;

using Rng = System.Random;

namespace Map.Generator
{
    public partial class RoadConfig : GeneratorConfig
    {
        public class RoadGenerator : IGenerationLogic
        {
            delegate IZoneWithCenterTiles MapZoneSelector(int x, int y);
            RoadConfig _config;

            Tilemap _tileMap;
            Rng _rng;
            LocationMapData _rawMapData;

            List<Vector3> _sitePositions = new();
            List<Vector3> _encounterPositions = new();
            List<SiteTemplate> _siteTemplates = new();
            List<EncounterTemplate> _encounterTemplates = new();

            public RoadGenerator(Tilemap tileMap, RoadConfig config)
            {
                _config = config;
                _tileMap = tileMap;
                _rng = new Rng(_config._seed);
            }

            public LocationMapData StartGeneration()
            {
                int roadLength = _rng.Next(_config._minLength, _config._maxLength);

                _rawMapData = new LocationMapData()
                {
                    width = _config.totalWidth,
                    height = roadLength,
                    tiles = new TileTemplate[_config.totalWidth, roadLength],
                    playerSpawnPos = new Vector3Int(_config.totalWidth * 3 / 4, 10, 0),
                };

                for (int y = 0; y < _rawMapData.height; y++)
                {
                    FillWithDefaultTile(y);
                    SetTile(_config._borderTile,_config._voidWidth, y);
                    SetTile(_config._borderTile, _rawMapData.width - _config._voidWidth - 1, y);
                }

                CreateRoad();
                CreateMapZoneTiles();

                return _rawMapData;
            }

            public void CreateMapZones(MapZonesManager mapZonesManager)
            {
                Random.InitState(_config._seed);
                for (int i = 0; i < _sitePositions.Count; i++)
                {
                    Vector3 positions =_sitePositions[i];
                    SiteTemplate template = _siteTemplates[i];
                    var site = mapZonesManager.CreateSite(positions);
                    site.BindTemplate(template, _rng);
                }

                for (int i = 0; i < _encounterPositions.Count; i++)
                {
                    Vector3 positions = _encounterPositions[i];
                    EncounterTemplate template = _encounterTemplates[i];
                    var encounter = mapZonesManager.CreateEncounter(positions);
                    encounter.BindTemplate(template, _rng);
                }
            }

            private void CreateMapZoneTiles()
            {
                int leftSitesX = _config._voidWidth + _config._siteWidth / 2;
                int rightSitesX = _rawMapData.width - _config._voidWidth - _config._siteWidth / 2;

                CreateTiles(leftSitesX, 0, SelectSite);
                CreateTiles(rightSitesX, 0, SelectSite);
                CreateTiles(_rawMapData.width / 2, 0, SelectEncounter, .5f);
            }

            private void FillWithDefaultTile(int y)
            {
                for (int x = 0; x < _rawMapData.width; x++)
                {
                    if (x < _config._voidWidth + _config._borderWidth) continue;
                    if (x > _rawMapData.width - _config._voidWidth - _config._borderWidth) continue;
                    SetTile(_config._defaultTile, x, y);
                }
            }

            private void CreateTiles(int x, int prevY, MapZoneSelector templateSelector, float disanceMult = 1f)
            {
                float distanceFromPrev = _rng.Next(_config._minDistanceBetweenSites, _config._maxDistanceBetweenSites) * disanceMult;
                int y = prevY + (int)distanceFromPrev;
                var template = templateSelector(x, y);

                ChangeCenterTiles(x, y, template);

                if (y + _config._maxDistanceBetweenSites * disanceMult > _rawMapData.height) return;
                CreateTiles(x, y, templateSelector, disanceMult);
            }

            private SiteTemplate SelectSite(int x, int y)
            {
                var template = _config._siteTemplates.GetRandom(_rng);
                _sitePositions.Add(new Vector3(x, y, 0));
                _siteTemplates.Add(template);
                return template;
            }

            private EncounterTemplate SelectEncounter(int x, int y)
            {
                var template = _config._encounterTemplates.GetRandom(_rng);
                _encounterPositions.Add(new Vector3(x, y, 0));
                _encounterTemplates.Add(template);
                return template;
            }

            private void ChangeCenterTiles(int centerX, int centerY, IZoneWithCenterTiles template)
            {
                if (template.centerZoneTile is null) return;

                for (int x = centerX - template.centerZoneSize.x / 2; x <= centerX + template.centerZoneSize.x / 2; x++)
                {
                    for (int y = centerY - template.centerZoneSize.y / 2; y <= centerY + template.centerZoneSize.y / 2; y++)
                    {
                        SetTile(template.centerZoneTile, x, y);
                    }
                }
            }

            private void CreateRoad()
            {
                var roadPosition = new int[_rawMapData.height];
                int minRoadPosition = _config._siteWidth + _config._voidWidth + _config._borderWidth + _config._emptyWidth;
                int maxRoadPosition = minRoadPosition + _config._roadWidth;
                int curveDirection = 1;

                for (int i = 0; i < roadPosition.Length; i++)
                {
                    roadPosition[i] = (minRoadPosition + maxRoadPosition) / 2;
                }

                for (int j = 0; j < _config._roadCurvesCount; j++)
                {
                    int startCurve = _rng.Next(_rawMapData.height);
                    int endCurve = _rng.Next(startCurve, _rawMapData.height);

                    for (int k = startCurve; k <= endCurve; k++)
                    {
                        roadPosition[k] = Mathf.Clamp(roadPosition[k] + curveDirection, minRoadPosition, maxRoadPosition);
                    }

                    curveDirection *= -1;
                }

                FixRoadCurves(roadPosition);
            }

            //=====***** => ======****
            //*****=====    *****=====
            private void FixRoadCurves(int[] roadPosition)
            {
                int prevPosition = roadPosition[0];
                for (int y = 0; y < roadPosition.Length; y++)
                {
                    int currentPosition = roadPosition[y];
                    SetTile(_config._roadTile, currentPosition, y);

                    if (currentPosition == prevPosition) continue;

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

                        while (x != currentPosition && x > 0 && x < _rawMapData.width)
                        {
                            SetTile(_config._roadTile, x, y);
                            x += direction;
                        }
                    }

                    prevPosition = currentPosition;
                }
            }

            private void SetTile(TileTemplate template, int x, int y)
            {
                Vector3Int tilePos = new (x, y, 0);
                _tileMap.SetTile(tilePos, template.tile);
                _rawMapData.tiles[x, y] = template;
            }
        }

    }
}

