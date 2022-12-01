using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
    public class Cylinder : Army
    {
        [Header("Movement")]
        [SerializeField] private float speed;

        [Header("Rotation")]
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Transform rotateTransform;

        public override void MakeNextMoveStep()
        {
            Rotate();
            Move();
        }

        private void Move()
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }

        private void Rotate()
        {
            rotateTransform.rotation = Quaternion.RotateTowards(rotateTransform.rotation,
                rotateTransform.rotation * Quaternion.LookRotation(Vector3.up, Vector3.left),
                rotationSpeed * Time.deltaTime);
        }
    }
}
