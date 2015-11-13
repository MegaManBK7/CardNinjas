using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Weapons
{
    public class TornadoHitbox : Hitbox
    {
        [SerializeField]
        public float zTargetDistance;

        [SerializeField]
        public float xTargetDistance;

        [SerializeField]
        public float zStartingPoint;

        [SerializeField]
        public float xStartingPoint;

        // Update is called once per frame
        public override void Update()
        {
            if (transform.position.z > zTargetDistance && transform.position.x == xStartingPoint)
            {
                transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z - 1f * Time.deltaTime);
            }
            else if(transform.position.x < xTargetDistance)
            {
                transform.position = new Vector3(transform.position.x + 1f * Time.deltaTime, transform.position.y, transform.position.z);
            }else if(transform.position.z < zStartingPoint)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f * Time.deltaTime);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
