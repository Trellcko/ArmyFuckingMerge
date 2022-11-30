using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Trell.ArmyFuckingMerge.Core
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Tilemap tilemap;

        [SerializeField] private Transform startPoint;

        [SerializeField] private Vector2 size;

        [SerializeField] private TileBase standartTile;
        [SerializeField] private TileBase selectedTile;

        private Dictionary<Vector3Int, Army> storage = new Dictionary<Vector3Int, Army>(); 

        private void Awake()
        {
            InitializeGrid();
        }

        public bool CheckIsFree(Vector3 worldPosition)
        {
            return CheckIsFree(grid.WorldToCell(worldPosition));
        }
        
        public bool CheckIsFree(Vector3Int cell)
        {
            if(storage.ContainsKey(cell))
            {
                return storage[cell] == null;
            }
            Debug.LogError($"{cell} doesn't contain in grid");
            return false;
        }

        public void ChangeStorageState(Vector3 worldPosition, Army army)
        {
            ChangeStorageState(grid.WorldToCell(worldPosition), army);
        }

        public void ChangeStorageState(Vector3Int cell, Army army)
        {
            if(storage.ContainsKey(cell))
            {
                storage[cell] = army;
                return;
            }

            Debug.LogError($"{cell} doesn't contain in grid");
        }

        public bool GetFreeCellPosition(ref Vector3 position)
        {
            foreach(var cell in storage)
            {
                print($"{cell} {storage}");
                if(cell.Value == null)
                {
                    print(cell.Key);
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
            tilemap.SetTile(cell, selectedTile);
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
                    storage.Add(position, null);
                }
            }
        }
    }
}