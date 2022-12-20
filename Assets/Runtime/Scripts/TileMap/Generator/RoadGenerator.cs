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
        public class RoadGeneratorr : IGenerationLogic
        {
            delegate IZoneWithCenterTiles MapZoneCreator(int x, int y);
            MapZonesManager _mapZonesManager;
            RoadConfig _config;

            Tilemap _tileMap;
            Rng _rng;
            LocationMapData _rawMapData;

            public RoadGeneratorr(Tilemap tileMap, RoadConfig config)
            {
                _config = config;
                _tileMap = tileMap;
                _rng = new Rng(_config._seed);
            }

            public LocationMapData StartGeneration()
            {
                int roadLength = _rng.Next(_config._minLength, _config._maxLength);

                _rawMapData = new LocationMapData();
                _rawMapData.width = _config.totalWidth;
                _rawMapData.height = roadLength;
                _rawMapData.walkabilityMap = new int[_rawMapData.width, roadLength];
                _rawMapData.playerSpawnPos = new Vector3Int(_rawMapData.width / 2, 0, 0);


                for (int y = 0; y < _rawMapData.height; y++)
                {
                    FillWithDefaultTile(y);
                    SetBorderTile(_config._voidWidth, y);
                    SetBorderTile(_config._voidWidth + 1, y);
                    SetBorderTile(_rawMapData.width - _config._voidWidth - 1, y);
                    SetBorderTile(_rawMapData.width - _config._voidWidth - 2, y);
                }

                CreateRoad();

                return _rawMapData;
            }

            public void CreateMapZones(MapZonesManager mapZonesManager)
            {
                _mapZonesManager = mapZonesManager;
                CreateSites();
            }


            private void CreateSites()
            {
                int leftSitesX = _config._voidWidth + _config._siteWidth / 2;
                int rightSitesX = _rawMapData.width - _config._voidWidth - _config._siteWidth / 2;

                CreateMapZone(leftSitesX, 0, CreateSite);
                CreateMapZone(rightSitesX, 0, CreateSite);
                CreateMapZone(_rawMapData.width / 2, 0, CreateEncounter, .5f);
            }

            private void FillWithDefaultTile(int y)
            {
                for (int x = 0; x < _rawMapData.width; x++)
                {
                    if (x < _config._voidWidth + _config._borderWidth) continue;
                    if (x > _rawMapData.width - _config._voidWidth - _config._borderWidth) continue;
                    _rawMapData.walkabilityMap[x, y] = 1;
                    var position = new Vector3Int(x, y, 0);
                    _tileMap.SetTile(position, _config._defaultTile);
                }
            }

            private void CreateMapZone(int x, int prevY, MapZoneCreator mapZoneCreator, float disanceMult = 1f)
            {
                float distanceFromPrev = _rng.Next(_config._minDistanceBetweenSites, _config._maxDistanceBetweenSites) * disanceMult;
                int y = prevY + (int)distanceFromPrev;
                var template = mapZoneCreator(x, y);

                ChangeCenterTiles(x, y, template);

                if (y + _config._maxDistanceBetweenSites * disanceMult > _rawMapData.height) return;
                CreateMapZone(x, y, mapZoneCreator, disanceMult);
            }

            private SiteTemplate CreateSite(int x, int y)
            {
                var template = _config._siteTemplates.GetRandom(_rng);
                var site = _mapZonesManager.CreateSite(new Vector3(x, y, 0));
                site.BindTemplate(template, _rng);
                return template;
            }

            private EncounterTemplate CreateEncounter(int x, int y)
            {
                var template = _config._encounterTemplates.GetRandom(_rng);
                var encounter = _mapZonesManager.CreateEncounter(new Vector3(x, y, 0));
                encounter.BindTemplate(template);
                return template;
            }

            private void ChangeCenterTiles(int centerX, int centerY, IZoneWithCenterTiles template)
            {
                if (template.centerZoneTile is null) return;

                for (int i = centerX - template.centerZoneWidth / 2; i <= centerX + template.centerZoneWidth / 2; i++)
                {
                    for (int j = centerY - template.centerZoneHeight / 2; j <= centerY + template.centerZoneHeight / 2; j++)
                    {
                        _tileMap.SetTile(new Vector3Int(i, j, 0), template.centerZoneTile);
                    }
                }
            }

            private void SetBorderTile(int x, int y)
            {
                _rawMapData.walkabilityMap[x, y] = 0;
                var position = new Vector3Int(x, y, 0);
                _tileMap.SetTile(position, _config._borderTile);
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

                //Fix diagonal roads
                //=====***** => ======****
                //*****=====    *****=====
                int prevPosition = roadPosition[0];
                for (int y = 0; y < roadPosition.Length; y++)
                {
                    int currentPosition = roadPosition[y];
                    _tileMap.SetTile(new Vector3Int(currentPosition, y, 0), _config._roadTile);

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
                            _tileMap.SetTile(new Vector3Int(x, y, 0), _config._roadTile);
                            x += direction;
                        }
                    }

                    prevPosition = currentPosition;
                }
            }
        }

    }
}

