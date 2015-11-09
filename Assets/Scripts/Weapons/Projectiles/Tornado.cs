using UnityEngine;

namespace Assets.Scripts.Weapons.Projectiles
{
    class Tornado : Hitbox
    {
        public override void OnTriggerEnter(Collider collider)
        {
            Hitbox h = collider.gameObject.GetComponent<Hitbox>();
            if (h != null)
            {
                Physics.IgnoreCollision(this.GetComponent<Collider>(), collider);
                return;
            }
            if (collider.tag == "Enemy")
            {
                Physics.IgnoreCollision(this.GetComponent<Collider>(), collider);
                return;
            }
        }
    }
}
