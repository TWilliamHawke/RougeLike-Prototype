using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Map
{
    public class TileSelector : MonoBehaviour
    {
		public static event UnityAction<Vector3Int> OnHoveredTileChange;


		Vector3Int _lastTilePos = Vector3Int.zero;


        void Update()
        {
			if(EventSystem.current.IsPointerOverGameObject()) return;

            var startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var hit = Physics2D.Raycast(startPoint, Vector2.zero);
			if(!hit) return;

			Vector3Int newTilePos = hit.point.Toint().AddZ(0);

			if(newTilePos != _lastTilePos)
			{
				_lastTilePos = newTilePos;
				OnHoveredTileChange?.Invoke(newTilePos);
				transform.position = newTilePos;
			}
        }
    }
}