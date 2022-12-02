using System;
using System.Collections.Generic;
using Trell.ArmyFuckingMerge.Core;
using Trell.ArmyFuckingMerge.Data;
using UnityEngine;

namespace Trell.ArmyFuckingMerge.Spawner
{
    public class ArmySpawner : MonoBehaviour
    {
        [SerializeField] private GridWrapper gridController;
        [SerializeField] private Transform parent;

        [Space]
        [SerializeField] private ArmyData cubes;
        [SerializeField] private ArmyData cylinders;
        [SerializeField] private ArmyData spheres;

        private Dictionary<Type, Func<int, bool>> armySpawnDelegates;

        private void Awake()
        {
            armySpawnDelegates = new Dictionary<Type, Func<int, bool>>()
            {
                [typeof(Cube)] = x => TrySpawnCube(x),
                [typeof(Sphere)] = x => TrySpawnSphere(x),
                [typeof(Cylinder)] = x => TrySpawnCylinder(x)
            };
        }

        private void Start()
        {
            TrySpawnCube();
            TrySpawnCube();
            TrySpawnCylinder();
            TrySpawnSphere();
        }

        public bool TrySpawn(Type type, int level)
        {
            if (armySpawnDelegates.ContainsKey(type))
            {
                return armySpawnDelegates[type](level);
            }
            Debug.LogError($"Army spawner cannot Spawn {type}");
            return false;
        }



        public bool TrySpawnCube(int level = 1)
        {
            return TrySpawn(cubes.GetArmyByLevel(level));
        }

        public bool TrySpawnCylinder(int level = 1)
        {
            return TrySpawn(cylinders.GetArmyByLevel(level));
        }

        public bool TrySpawnSphere(int level = 1)
        {
            return TrySpawn(spheres.GetArmyByLevel(level));
        }

        private bool TrySpawn(Army army)
        {
            if (army == null)
                return false;
            Vector3 spawnPosition = Vector3.zero;

            if (gridController.GetFreeCellPosition(ref spawnPosition))
            {
                Army spawnedArmy = Instantiate(army, spawnPosition, Quaternion.identity, parent);
                gridController.ChangeStorageState(spawnPosition, spawnedArmy);

                return true;
            }
            return false;
        }
    }
}
