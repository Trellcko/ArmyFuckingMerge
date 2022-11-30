using Trell.ArmyFuckingMerge.Spawner;
using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
	public class MergeController : MonoBehaviour
	{
		[SerializeField] private ArmySpawner armySpawner;

		public bool TryMerge(Army army1, Army army2)
        {
			if (Army.IsArmyEqual(army1, army2))
            {
				print("iSeQUAL");
				int level = army1.Level + 1;
				if (level <= Army.MAX_LEVEL)
				{
					Destroy(army1.gameObject);
					Destroy(army2.gameObject);
					armySpawner.TrySpawn(army1.GetType(), level);
					return true;
				}
            }
			return false;
        }
	}
}
