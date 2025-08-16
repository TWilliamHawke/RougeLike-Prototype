using Map;
using UnityEngine;

namespace Entities
{
    public class PositionController : MonoBehaviour, IEntityComponent, IPositionData
    {
        [SerializeField] Body _body;

        public Vector3 position => transform.position;
        public Vector3Int intPosition => position.ToInt();

        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        public void SpawnAt(TileNode node)
        {
            MoveTo(node.intPosition);
        }

        public void UpdateBodyPosition(Vector3 position)
        {
            _body.transform.position = position;
        }
    }
}