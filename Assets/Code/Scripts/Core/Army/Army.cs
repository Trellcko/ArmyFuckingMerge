using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
	public abstract class Army : MonoBehaviour
	{
		[field: SerializeField] public int Level { get; private set; }
        [SerializeField] private GameObject outline;

		private bool _isMoving = false;

		public const int MAX_LEVEL = 2;

        private void OnEnable()
        {
            FigthStateMachine.Instnance.OnStartFigth += OnStartFigth;
        }

        private void OnDisable()
        {
            FigthStateMachine.Instnance.OnStartFigth -= OnStartFigth;
        }

        private void Update()
        {
            if (_isMoving)
            {
                MakeNextMoveStep();
            }
        }

        public abstract void MakeNextMoveStep();

        public void TurnOnOutLine()
        {
            outline.gameObject.SetActive(true);
        }

        public void TurnOffOutLine()
        {
            outline.gameObject.SetActive(false);
        }

		public static bool IsArmyEqual(Army army1, Army army2)
        {
			return  army1.GetType() == army2.GetType() && army1.Level == army2.Level;
		}

        private void OnStartFigth()
        {
            _isMoving = true;
        }
    }
}
