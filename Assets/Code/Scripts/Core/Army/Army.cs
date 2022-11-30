using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
	public abstract class Army : MonoBehaviour
	{
		[field: SerializeField] public int Level { get; private set; }

		public abstract void MakeNextMoveStep();
	}
}
