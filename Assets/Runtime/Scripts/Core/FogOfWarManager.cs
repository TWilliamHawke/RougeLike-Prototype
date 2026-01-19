using System;
using System.Collections.Generic;
using Entities.PlayerScripts;
using Entities.Stats;
using Map;
using UnityEngine;

namespace Core
{
    public class FogOfWarManager : MonoBehaviour
    {
        [SerializeField] int _fowPosZ = -2;
        [SerializeField] MeshRenderer _fowPlane;
        [SerializeField] StaticStat _lineOfSightStat;
        [SerializeField] PlayerStats _playerStats;

        [InjectField] Player _player;
        [InjectField] TilesGrid _tilesGrid;

        int _textureResolution = 100;
        int _mapSize = 100;
        Texture2D _fowTexture;
        Color32[] _pixels;
        FowStates[,] _fowData;
        int _radiusTex;
        float _lineOfSightRadius;
        float _squaredLineOfSightRadius;

        Dictionary<FowStates, Color> _fowStateToColor = new()
        {
            { FowStates.Hidden, Color.black },
            { FowStates.Explored, new Color(0, 0, 0, .5f) },
            { FowStates.Visible, Color.clear }
        };

        void LateUpdate()
        {
            if (_player is null || _tilesGrid is null) return;
            UpdateFow();
        }

        void OnDestroy()
        {
            var lineOfSightContainer = _playerStats.FindContainer(_lineOfSightStat);
            lineOfSightContainer.OnFloatValueChanged -= UpdateLineOfSightRadius;
        }

        //Used in Unity Editor
        public void CreateTexture()
        {
            if (_player is null || _tilesGrid is null) return;
            _mapSize = Math.Max(_tilesGrid.gridSize.x, _tilesGrid.gridSize.y);
            _textureResolution = _mapSize;
            _radiusTex = Mathf.CeilToInt(_lineOfSightRadius / _mapSize * _textureResolution);
            _fowTexture = new(_textureResolution, _textureResolution);
            _fowData = new FowStates[_textureResolution, _textureResolution];
            _pixels = new Color32[_textureResolution * _textureResolution];

            for (int i = 0; i < _pixels.Length; i++)
            {
                _pixels[i] = Color.black;
            }
            _fowTexture.SetPixels32(_pixels);
            _fowTexture.Apply();

            float xPos = _mapSize / 2f - .5f;
            float yPos = _mapSize / 2f - .5f;
            _fowPlane.transform.position = new Vector3(xPos, yPos, _fowPosZ);
            _fowPlane.transform.localScale = new Vector3(_mapSize, _mapSize, 1f);
            _fowPlane.material.mainTexture = _fowTexture;
        }

        //Used in Unity Editor
        public void SetLineOfSightRadius()
        {
            var _lineOfSightContainer = _playerStats.FindContainer(_lineOfSightStat);
            UpdateLineOfSightRadius(_lineOfSightContainer.floatValue);
            _lineOfSightContainer.OnFloatValueChanged += UpdateLineOfSightRadius;
        }

        private void UpdateLineOfSightRadius(float newValue)
        {
            _lineOfSightRadius = newValue - 1f;
            _squaredLineOfSightRadius = _lineOfSightRadius * _lineOfSightRadius;
            _radiusTex = Mathf.CeilToInt(_lineOfSightRadius / _mapSize * _textureResolution);
        }

        private void UpdateFow()
        {
            RevealCircle(_player.transform.position);

            for (int x = 0; x < _textureResolution; x++)
            {
                for (int y = 0; y < _textureResolution; y++)
                {
                    int index = y * _textureResolution + x;
                    FowStates state = _fowData[x, y];
                    _pixels[index] = _fowStateToColor[state];
                }
            }

            _fowTexture.SetPixels32(_pixels);
            _fowTexture.Apply();
        }

        private void RevealCircle(Vector3 worldPosition)
        {
            Vector2Int texturePosition = WorldToTexture(worldPosition);
            int cx = texturePosition.x;
            int cy = texturePosition.y;

            for (int y = -_radiusTex; y <= _radiusTex; y++)
            {
                for (int x = -_radiusTex; x <= _radiusTex; x++)
                {
                    int fowX = cx + x;
                    int fowY = cy + y;

                    if (!_fowData.IndexIsInsideBounds(fowX, fowY)) continue;

                    if (x * x + y * y < _squaredLineOfSightRadius)
                    {
                        _fowData[fowX, fowY] = FowStates.Visible;
                    }
                }
            }
        }

        private Vector2Int WorldToTexture(Vector3 worldPos)
        {
            float normX = Mathf.InverseLerp(0, _mapSize, worldPos.x);
            float normY = Mathf.InverseLerp(0, _mapSize, worldPos.y);

            int x = Mathf.RoundToInt(normX * _textureResolution);
            int y = Mathf.RoundToInt(normY * _textureResolution);

            return new Vector2Int(x, y);
        }

        private void FadeVision()
        {
            for (int x = 0; x < _textureResolution; x++)
            {
                for (int y = 0; y < _textureResolution; y++)
                {
                    if (_fowData[x, y] == FowStates.Visible)
                    {
                        _fowData[x, y] = FowStates.Explored;
                    }
                }
            }
        }

        private void AttachShaderData()
        {
            Vector2 texelSize = new(1f / _textureResolution, 1f / _textureResolution);
            _fowPlane.material.SetVector("_TexelSize", new Vector4(texelSize.x, texelSize.y, 0, 0));
        }

        enum FowStates
        {
            Hidden = 0,
            Explored = 1,
            Visible = 2,
        }
    }
}