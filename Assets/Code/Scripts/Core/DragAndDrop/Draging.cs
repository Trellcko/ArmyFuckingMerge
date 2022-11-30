using Trell.ArmyFuckingMerge.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Trell.ArmyFuckingMerge.DragAndDrop
{
    public class Draging : MonoBehaviour, IDragHandler, IPointerDownHandler, IDropHandler
    {
        [SerializeField] private GridController gridController;
        [SerializeField] private MergeController mergeController;
        [SerializeField] private LayerMask battleField;

        public Vector3 StartWorldPosition { get; private set; }
        public Vector3 LastWorldPosition { get; private set; }
        public Vector3 CurrentWorldPosition { get; private set; }

        private Dragable _dragable;

        private Camera _camera;

        private bool _isDraging;

        private Transform _dragableTransform => _dragable.transform;

        private void Awake()
        {
            print((int)battleField);
            _camera = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Ray ray = _camera.ScreenPointToRay(eventData.position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                _isDraging = hit.transform.TryGetComponent(out _dragable);
                StartWorldPosition = CurrentWorldPosition = LastWorldPosition = hit.transform.position;
                gridController.SelectTile(CurrentWorldPosition);
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
                    print(hit.transform.name);
                    print(hit.transform.gameObject.layer);
                    gridController.UnSelectTile(LastWorldPosition);
                    Vector3 dragPosition = hit.point;
                    LastWorldPosition = CurrentWorldPosition;
                    CurrentWorldPosition = _dragableTransform.position = dragPosition;
                    gridController.SelectTile(CurrentWorldPosition);
                }
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            gridController.UnSelectTile(CurrentWorldPosition);

            Army armyInCell = gridController.GetArmy(CurrentWorldPosition);

            _dragable.transform.position = gridController.GetCenterPosition(CurrentWorldPosition);

            if (armyInCell != null)
            {
                armyInCell.transform.position = gridController.GetCenterPosition(StartWorldPosition);
                
                gridController.ChangeStorageState(StartWorldPosition, null);
                gridController.ChangeStorageState(CurrentWorldPosition, null);
                if (!mergeController.TryMerge(armyInCell, _dragable.Army))
                {
                    Swap(armyInCell, _dragable.Army);
                }

            }
            else
            {
                gridController.ChangeStorageState(StartWorldPosition, null);
                gridController.ChangeStorageState(CurrentWorldPosition, _dragable.Army);
            }
            _dragable = null;
            _isDraging = false;
        }

        private void Swap(Army army1, Army army2)
        {
            gridController.ChangeStorageState(StartWorldPosition, army1);
            army1.transform.position = gridController.GetCenterPosition(StartWorldPosition);
            gridController.ChangeStorageState(CurrentWorldPosition, army2);
            army2.transform.position = gridController.GetCenterPosition(CurrentWorldPosition);
        }
    }
}
