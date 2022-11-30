using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
	public abstract class Army : MonoBehaviour
	{
		[field: SerializeField] public int Level { get; private set; }

		public const int MAX_LEVEL = 2;

        public abstract void MakeNextMoveStep();

		public static bool IsArmyEqual(Army army1, Army army2)
        {
			print(army1.GetType());
			print(army2.GetType());
			return  army1.GetType() == army2.GetType() && army1.Level == army2.Level;
		}

	}
}
