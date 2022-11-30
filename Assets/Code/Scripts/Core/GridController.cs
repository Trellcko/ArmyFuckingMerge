using UnityEngine;
using UnityEngine.Tilemaps;

namespace Trell.ArmyFuckingMerge
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Tilemap _tilemap;

        [SerializeField] private Transform _startPoint;

        [SerializeField] private Vector2 _size;

        [SerializeField] private TileBase _standartTile;
        [SerializeField] private TileBase _selectedTile;

        private void Start()
        {
            InitializeGrid();
        }
        
        public void SelectTile(Vector3 worldPosition)
        {
            SelectTile(_grid.WorldToCell(worldPosition));
        }

        public void SelectTile(Vector3Int cell)
        {
            _tilemap.SetTile(cell, _selectedTile);
        }

        private void InitializeGrid()
        {
            Vector3Int startPosition = _grid.WorldToCell(_startPoint.position);

            for(int  i = 0; i < _size.x; i++)
            {
                for(int  j = 0; j < _size.y; j++)
                {
                    Vector3Int position = startPosition + new Vector3Int(i, j, 0);
                    _tilemap.SetTile(position, _standartTile);
                }
            }
            SelectTile(_startPoint.position);
        }
    }
}