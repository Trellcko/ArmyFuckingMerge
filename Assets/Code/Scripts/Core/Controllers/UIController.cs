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
		[SerializeField] private Button startFigthButton;

		[Space]
		[SerializeField] private ArmySpawner armySpawner;

		private UnityAction _spawnCube;
		private UnityAction _spawnSphere;
		private UnityAction _spawnCylinder;
		private UnityAction _startFigth;

        private void Awake()
        {
			_spawnCube = ()=> { armySpawner.TrySpawnCube(); };
			_spawnSphere = () => { armySpawner.TrySpawnSphere(); };
			_spawnCylinder = () => { armySpawner.TrySpawnCylinder(); };
			_startFigth = () => { FigthStateMachine.Instnance.SetState(FigthState.StartFigth); };
		}

        private void OnEnable()
        {
			addCubeButton.onClick.AddListener(_spawnCube);
			addCylinderButton.onClick.AddListener(_spawnCylinder);
			addSphereButton.onClick.AddListener(_spawnSphere);
			startFigthButton.onClick.AddListener(_startFigth);
		}

        private void OnDisable()
        {
			addCubeButton.onClick.RemoveListener(_spawnCube);
			addCylinderButton.onClick.RemoveListener(_spawnCylinder);
			addSphereButton.onClick.RemoveListener(_spawnSphere);
			startFigthButton.onClick.RemoveListener(_startFigth);
		}
    }
}
