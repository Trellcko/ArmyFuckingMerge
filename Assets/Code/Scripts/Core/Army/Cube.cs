using UnityEngine;

namespace Trell.ArmyFuckingMerge.Core
{
    public class Cube : Army
    {
        [Header("Movement")]
        [SerializeField] private float speed;
        public override void MakeNextMoveStep()
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }
}
