using Trell.ArmyFuckingMerge.Core;
using Trell.ArmyFuckingMerge.Data;
using UnityEngine;

namespace Trell.ArmyFuckingMerge.Spawner
{
	public class ArmySpawner : MonoBehaviour
	{
		[SerializeField] private GridController gridController;
        [SerializeField] private Transform parent;

        [Space]
		[SerializeField] private ArmyData cubes;
		[SerializeField] private ArmyData cylinders;
		[SerializeField] private ArmyData spheres;

        private void Start()
        {
            SpawnCube();
            SpawnCylinder();
            SpawnSphere();
        }
        public void SpawnCube()
        {
            Spawn(cubes.GetArmyByLevel(1));
        }

        public void SpawnCylinder()
        {
            Spawn(cylinders.GetArmyByLevel(1));
        }

        public void SpawnSphere()
        {
            Spawn(spheres.GetArmyByLevel(1));
        }

        private void Spawn(Army army)
        {
            Vector3 spawnPosition = Vector3.zero;

            if(gridController.GetFreeCellPosition(ref spawnPosition))
            {
                Army spawnedArmy = Instantiate(army, spawnPosition, Quaternion.identity, parent);
                gridController.ChangeStorageState(spawnPosition, spawnedArmy);
            }
        }
	}
}
