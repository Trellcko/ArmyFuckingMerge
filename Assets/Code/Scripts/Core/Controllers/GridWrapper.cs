using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Trell.ArmyFuckingMerge.Core
{
    public class GridWrapper : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Tilemap tilemap;

        [SerializeField] private Transform startPoint;

        [SerializeField] private Vector2 size;

        [SerializeField] private TileBase standartTile;
        [SerializeField] private TileBase selectedTile;

        private Dictionary<Vector3Int, Army> _storage = new Dictionary<Vector3Int, Army>(); 

        private void Awake()
        {
            InitializeGrid();
        }

        public bool CheckInGrid(Vector3 worldPosition)
        {
            return CheckInGrid(grid.WorldToCell(worldPosition));
        }

        public bool CheckInGrid(Vector3Int cell)
        {
            return _storage.ContainsKey(cell);
        }

        public bool CheckIsFree(Vector3 worldPosition)
        {
            return CheckIsFree(grid.WorldToCell(worldPosition));
        }
        
        public bool CheckIsFree(Vector3Int cell)
        {
            if(_storage.ContainsKey(cell))
            {
                return _storage[cell] == null;
            }
            Debug.LogError($"{cell} doesn't contain in grid");
            return false;
        }

        public Army GetArmy(Vector3 worldPosition)
        {
            return GetArmy(grid.WorldToCell(worldPosition));
        }

        public Army GetArmy(Vector3Int cell)
        {
            if(_storage.ContainsKey(cell))
            {
                return _storage[cell];
            }
            return null;
        }

        public Vector3 GetCenterPosition(Vector3 worldPosition)
        {
            Vector3Int cellPosition = grid.WorldToCell(worldPosition);
            return grid.GetCellCenterWorld(cellPosition);
        }

        public void ChangeStorageState(Vector3 worldPosition, Army army)
        {
            ChangeStorageState(grid.WorldToCell(worldPosition), army);
        }

        public void ChangeStorageState(Vector3Int cell, Army army)
        {
            if(_storage.ContainsKey(cell))
            {
                _storage[cell] = army;
                return;
            }

            Debug.LogError($"{cell} doesn't contain in grid");
        }

        public bool GetFreeCellPosition(ref Vector3 position)
        {
            foreach(var cell in _storage)
            {
                if(cell.Value == null)
                {
                    position = grid.GetCellCenterWorld(cell.Key);
                    return true;
                }
            }
            return false;
        }

        public void SelectTile(Vector3 worldPosition)
        {
            SelectTile(grid.WorldToCell(worldPosition));
        }

        public void SelectTile(Vector3Int cell)
        {
            if (_storage.ContainsKey(cell))
            {
                tilemap.SetTile(cell, selectedTile);
                return;
            }
        }

        public void UnSelectTile(Vector3 worldPosition)
        {
            UnSelectTile(grid.WorldToCell(worldPosition));
        }

        public void UnSelectTile(Vector3Int cell)
        {
            if (_storage.ContainsKey(cell))
            {
                tilemap.SetTile(cell, standartTile);
                return;
            }
        }

        private void InitializeGrid()
        {
            Vector3Int startPosition = grid.WorldToCell(startPoint.position);

            for(int  i = 0; i < size.x; i++)
            {
                for(int  j = 0; j < size.y; j++)
                {
                    Vector3Int position = startPosition + new Vector3Int(i, j, 0);
                    tilemap.SetTile(position, standartTile);
                    _storage.Add(position, null);
                }
            }
        }
    }
}