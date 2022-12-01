using System;
using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
    public class FigthStateMachine : MonoBehaviour
    {
        public event Action OnStartFigth;

        public static FigthStateMachine Instnance => _instance;

        private static FigthStateMachine _instance;

        private void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if(FindObjectsOfType<FigthStateMachine>().Length > 1)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            SetState(FigthState.PrepareToFigth);
        }

        public void SetState(FigthState state)
        {
            switch (state)
            {
                case FigthState.PrepareToFigth:
                    print("StartFigth");
                    break;
                case FigthState.StartFigth:
                    OnStartFigth?.Invoke();
                    break;
                case FigthState.FinishFigth:
                    print("FinishFigth");
                    break;
            }
        }
    }
    public enum FigthState
    { 
        PrepareToFigth,
        StartFigth,
        FinishFigth,
    }

}
