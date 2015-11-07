using UnityEngine;

namespace Assets.Scripts.Enemies
{
    class Statue : Enemy
    {
        protected override void Initialize()
        {
        }

        protected override void RunAI()
        {
        }

        protected override void Render(bool render)
        {
            GetComponent<MeshRenderer>().enabled = render;
        }
    }
}
