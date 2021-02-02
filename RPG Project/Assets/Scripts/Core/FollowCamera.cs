using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {

        [SerializeField] GameObject Target;

        private void LateUpdate()
        {
            transform.position = Target.transform.position;
        }
    }
}
