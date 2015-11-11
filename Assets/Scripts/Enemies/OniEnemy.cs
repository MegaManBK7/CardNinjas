using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;
using Assets.Scripts.Enemies;

namespace Assets.Scripts.Enemies
{
    class OniEnemy : Enemy
    {

        public Util.Enums.Direction mdec = Util.Enums.Direction.Left;
        private Player.Player player;
        public float turn = 0;
        [SerializeField]
        private Animator anim;

        public bool turnIsEnded = false;


        protected override void Initialize()
        {
            transform.position = currentNode.transform.position;
            player = FindObjectOfType<Player.Player>();
        }

        protected override void RunAI()
        {

            if (animDone) { 
                if (turn <= 3)
                {
                    changeSpot();
                }
                else
                {
                    turn = 0;
                }
            }


        }

        protected override void Render(bool render)
        {   
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = render;
        }

        public void endTurn()
        {
            print("HUELOL");
            turnIsEnded = true;
            turn+= 1;
            anim.SetTrigger("WaitTrigger");
        }

        public void changeSpot()
        {
            
            mdec = (Util.Enums.Direction)Random.Range(0, 3);

            if(mdec == Util.Enums.Direction.Up)
            {
                //If we're randomly going up
                if(currentNode.panelAllowed(Util.Enums.Direction.Up, Type))
                {
                    anim.SetTrigger("MoveEndTrigger");
                    currentNode.clearOccupied();//Say we aren't here
                    currentNode = currentNode.Up;//Say we're there
                    currentNode.Owner = (this);//Tell the place we own it.
                }
            }

            else if (currentNode.panelAllowed(Util.Enums.Direction.Right, Type))
            {
                anim.SetTrigger("MoveEndTrigger");
                currentNode.clearOccupied();//Say we aren't here
                currentNode = currentNode.Right;//Say we're there
                currentNode.Owner = (this);//Tell the place we own it.
            }

            else if (currentNode.panelAllowed(Util.Enums.Direction.Left, Type))
            {
                anim.SetTrigger("MoveEndTrigger");
                currentNode.clearOccupied();//Say we aren't here
                currentNode = currentNode.Left;//Say we're there
                currentNode.Owner = (this);//Tell the place we own it.
            }

            else if (currentNode.panelAllowed(Util.Enums.Direction.Down, Type))
            {
                //If we're randomly going down
                if (currentNode.Up.Occupied == false)
                {
                    anim.SetTrigger("MoveEndTrigger");
                    currentNode.clearOccupied();//Say we aren't here
                    currentNode = currentNode.Down;//Say we're there
                    currentNode.Owner = (this);//Tell the place we own it.
                }
            }
            transform.position = currentNode.transform.position;
        }
    }
}