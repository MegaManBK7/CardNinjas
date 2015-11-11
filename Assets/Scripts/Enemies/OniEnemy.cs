using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;
using Assets.Scripts.Enemies;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Enemies
{
    class OniEnemy : Enemy
    {

        public Util.Enums.Direction mdec = Util.Enums.Direction.Left;
        private Player.Player player;
        public float turn = 0;
        [SerializeField]
        private Animator anim;
        public bool shouldAttack = false;
        public enum attackType { Melee, Projectile }
        public attackType attackStyle = attackType.Melee;


		public Hitbox meleeHitbox;
		public GameObject meleeEffect;


        protected override void Initialize()
        {
			meleeHitbox.Owner = this.gameObject;
			meleeHitbox.DeathTime = .5f;

			meleeEffect.transform.RotateAround(Vector3.up, 180f);

            transform.position = currentNode.transform.position;
            player = FindObjectOfType<Player.Player>();
        }

        protected override void RunAI()
        {

            if (animDone) {
				if (shouldAttack)
				{
					Attack();
				}
				else if (currentNode.Type != Type)
				{
					returnToField();
				}
				else
				{
					if (turn < 3)
					{
						changeSpot();
					}
					else
					{
						stepToPlayer();
						shouldAttack = true;
						turn = 0;
					}
				}
            }


        }

        protected override void Render(bool render)
        {   
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = render;
        }

		public void Attack()
		{
			anim.SetTrigger("AttackTrigger");
			GameObject.Instantiate(meleeHitbox, new Vector3(currentNode.Left.transform.position.x, currentNode.Left.transform.position.y, currentNode.Left.transform.position.z - 1.5f), Quaternion.identity);

			GameObject.Instantiate(meleeEffect, new Vector3(currentNode.Left.transform.position.x, currentNode.Left.transform.position.y+2, currentNode.Left.transform.position.z), Quaternion.identity);
			shouldAttack = false;
		}

        public void changeSpot()
        {
            anim.SetTrigger("MoveBeginTrigger");
            mdec = (Util.Enums.Direction)Random.Range(0, 3);

            if(mdec == Util.Enums.Direction.Up)
            {
                //If we're randomly going up
                if(currentNode.panelAllowed(Util.Enums.Direction.Up, Type))
                {
                    currentNode.clearOccupied();//Say we aren't here
                    currentNode = currentNode.Up;//Say we're there
                    currentNode.Owner = (this);//Tell the place we own it.
                }
            }

            else if (currentNode.panelAllowed(Util.Enums.Direction.Right, Type))
            {
                currentNode.clearOccupied();//Say we aren't here
                currentNode = currentNode.Right;//Say we're there
                currentNode.Owner = (this);//Tell the place we own it.
            }

            else if (currentNode.panelAllowed(Util.Enums.Direction.Left, Type))
            {
                currentNode.clearOccupied();//Say we aren't here
                currentNode = currentNode.Left;//Say we're there
                currentNode.Owner = (this);//Tell the place we own it.
            }

            else if (currentNode.panelAllowed(Util.Enums.Direction.Down, Type))
            {
                //If we're randomly going down
                if (currentNode.Up.Occupied == false)
                {
                    currentNode.clearOccupied();//Say we aren't here
                    currentNode = currentNode.Down;//Say we're there
                    currentNode.Owner = (this);//Tell the place we own it.
                }
            }
            anim.SetTrigger("MoveEndTrigger");
            transform.position = currentNode.transform.position;
            turn++;
        }

		public void stepToPlayer()
		{

			currentNode = player.CurrentNode.Right;
			anim.SetTrigger("MoveEndTrigger");
			transform.position = currentNode.transform.position;
		}

		public void returnToField()
		{
			currentNode = grid[rowStart, colStart];
			mdec = (Util.Enums.Direction)Random.Range(0, 3);

			if (mdec == Util.Enums.Direction.Up)
			{
				//If we're randomly going up
				if (currentNode.panelAllowed(Util.Enums.Direction.Up, Type))
				{
					currentNode.clearOccupied();//Say we aren't here
					currentNode = currentNode.Up;//Say we're there
					currentNode.Owner = (this);//Tell the place we own it.
				}
			}

			else if (currentNode.panelAllowed(Util.Enums.Direction.Right, Type))
			{
				currentNode.clearOccupied();//Say we aren't here
				currentNode = currentNode.Right;//Say we're there
				currentNode.Owner = (this);//Tell the place we own it.
			}

			else if (currentNode.panelAllowed(Util.Enums.Direction.Left, Type))
			{
				currentNode.clearOccupied();//Say we aren't here
				currentNode = currentNode.Left;//Say we're there
				currentNode.Owner = (this);//Tell the place we own it.
			}

			else if (currentNode.panelAllowed(Util.Enums.Direction.Down, Type))
			{
				//If we're randomly going down
				if (currentNode.Up.Occupied == false)
				{
					currentNode.clearOccupied();//Say we aren't here
					currentNode = currentNode.Down;//Say we're there
					currentNode.Owner = (this);//Tell the place we own it.
				}
			}
			transform.position = currentNode.transform.position;
		}
	}
}