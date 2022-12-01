using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
    public class Sphere : Army
    {
        [Header("Jump")]
        [SerializeField] private AnimationCurve jumpCurve;
        [SerializeField] private float jumpTime = 2f;
        [SerializeField] private float jumpPower = 2f;
        
        [Space]
        [Header("Movemement")]
        [SerializeField] private float speed = 1f;

        private float _currentFlyTime;

        public override void MakeNextMoveStep()
        {
            float y = CalculateY();
            float z = CalculateZ();


            transform.position = new Vector3(transform.position.x, y, z);
        }

        private float CalculateZ()
        {
            return transform.position.z + speed * Time.deltaTime;
        }

        private float CalculateY()
        {
            float percent = Mathf.Clamp01(_currentFlyTime / jumpTime);
            
            float y = jumpCurve.Evaluate(percent) * jumpPower;

            _currentFlyTime += Time.deltaTime;

            if(_currentFlyTime > jumpTime)
            {
                _currentFlyTime = 0;
            }
            return y;
        }
    }
}
