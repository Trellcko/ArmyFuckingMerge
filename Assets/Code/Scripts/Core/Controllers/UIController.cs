using Trell.ArmyFuckingMerge.Spawner;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Trell.ArmyFuckingMerge.Core
{
	public class UIController : MonoBehaviour
	{
		[SerializeField] private Button addCubeButton;
		[SerializeField] private Button addSphereButton;
		[SerializeField] private Button addCylinderButton;

		[Space]
		[SerializeField] private ArmySpawner armySpawner;

		private UnityAction _spawnCubeAction;
		private UnityAction _spawnSphereAction;
		private UnityAction _spawnCylinderAction;

        private void Awake()
        {
			_spawnCubeAction = ()=> { armySpawner.TrySpawnCube(); };
			_spawnSphereAction = () => { armySpawner.TrySpawnSphere(); };
			_spawnCylinderAction = () => { armySpawner.TrySpawnCylinder(); };
		}

        private void OnEnable()
        {
			addCubeButton.onClick.AddListener(_spawnCubeAction);
			addCylinderButton.onClick.AddListener(_spawnCylinderAction);
			addSphereButton.onClick.AddListener(_spawnSphereAction);
        }

        private void OnDisable()
        {
			addCubeButton.onClick.RemoveListener(_spawnCubeAction);
			addCylinderButton.onClick.RemoveListener(_spawnCylinderAction);
			addSphereButton.onClick.RemoveListener(_spawnSphereAction);
		}
    }
}
