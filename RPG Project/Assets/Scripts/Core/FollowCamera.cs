using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {

        [SerializeField] GameObject Target;
        float speed = 5;

        private void LateUpdate()
        {
            transform.position = Target.transform.position;
        }
    }
}
