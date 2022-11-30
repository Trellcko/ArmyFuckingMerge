using System.Collections.Generic;
using System.Linq;
using Trell.ArmyFuckingMerge.Core;
using UnityEngine;

namespace Trell.ArmyFuckingMerge.Data
{
	[CreateAssetMenu(fileName = "ArmyData", menuName = "SO/ArmyData", order = 41)]
	public class ArmyData : ScriptableObject
	{
		[SerializeField] private List<Army> armies;

		public Army GetArmyByLevel(int level)
        {
			return armies.Where(x => x.Level == level).FirstOrDefault();
        }
	}
}
