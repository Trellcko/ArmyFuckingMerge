using Trell.ArmyFuckingMerge.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Trell.ArmyFuckingMerge.DragAndDrop
{
    public class Draging : MonoBehaviour, IDragHandler, IPointerDownHandler, IDropHandler
    {
        [SerializeField] private GridWrapper gridController;
        [SerializeField] private Merger mergeController;
        [SerializeField] private Vector3 offset;
        [SerializeField] private LayerMask battleField;

        public Vector3 StartWorldPosition { get; private set; }
        public Vector3 LastWorldPosition { get; private set; }
        public Vector3 CurrentWorldPosition { get; private set; }

        private Dragable _dragable;

        private Camera _camera;

        private bool _isDraging;

        private bool _canHandleDragging = true;

        private Transform _dragableTransform => _dragable.transform;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            FigthStateMachine.Instnance.OnStartFigth += OnStartFigth;
        }

        private void OnDisable()
        {
            FigthStateMachine.Instnance.OnStartFigth -= OnStartFigth;
        }

        private void OnStartFigth()
        {
            _canHandleDragging = false;
            _isDraging = false;
            _dragable = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_canHandleDragging)
                return;

            Ray ray = _camera.ScreenPointToRay(eventData.position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                if (_isDraging = hit.transform.TryGetComponent(out _dragable))
                {
                    _dragable.Army.TurnOnOutLine();
                    StartWorldPosition = CurrentWorldPosition = LastWorldPosition = hit.transform.position;
                    gridController.SelectTile(CurrentWorldPosition);
                }
                return;
            }
            _isDraging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDraging)
            {
                Ray ray = _camera.ScreenPointToRay(eventData.position);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, battleField.value))
                {
                    gridController.UnSelectTile(LastWorldPosition);

                    Vector3 dragPosition = hit.point;
                    dragPosition += offset;

                    LastWorldPosition = CurrentWorldPosition;
                    CurrentWorldPosition = _dragableTransform.position = dragPosition;

                    gridController.SelectTile(CurrentWorldPosition);
                }
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!_isDraging)
                return;

            gridController.UnSelectTile(CurrentWorldPosition);

            _dragable.Army.TurnOffOutLine();
            
            if (gridController.CheckInGrid(CurrentWorldPosition))
            {
                Army armyInCell = gridController.GetArmy(CurrentWorldPosition);

                if (armyInCell != null && armyInCell != _dragable.Army)
                {
                    gridController.ChangeStorageState(StartWorldPosition, null);
                    gridController.ChangeStorageState(CurrentWorldPosition, null);

                    if (!mergeController.TryMerge(armyInCell, _dragable.Army))
                    {
                        SetArmyToPosition(armyInCell, StartWorldPosition);
                        SetArmyToPosition(_dragable.Army, CurrentWorldPosition);
                    }
                    return;
                }
                SetArmyToPosition(null, StartWorldPosition);
                SetArmyToPosition(_dragable.Army, CurrentWorldPosition);
               
                return;
            }
            _dragableTransform.position = gridController.GetCenterPosition(StartWorldPosition);
            
        }

        private void SetArmyToPosition(Army army, Vector3 position)
        {
            gridController.ChangeStorageState(position, army);
            if (army != null)
            {
                army.transform.position = gridController.GetCenterPosition(position);
            }
        }
    }
}
