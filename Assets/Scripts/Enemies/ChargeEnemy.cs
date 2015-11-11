using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Enemies
{
    class ChargeEnemy : Enemy
    {
        [SerializeField]
        private GameObject bullet;
        [SerializeField]
        private SkinnedMeshRenderer[] body;
        [SerializeField]
        private Util.SoundPlayer sfx;

        private Player.Player player;
        private float turn = 0;
        public Animator mechAnima;
        bool Attacking;
        bool ResetingPosition;

        protected override void Initialize()
        {
            player = FindObjectOfType<Player.Player>();
            mechAnima.GetComponent<Animator>();
            mechAnima.SetBool("Stab", true);
        }

        protected override void RunAI()
        { 

            //We change turns each second
            turn += Time.deltaTime;
            if (!hit && (!Attacking && !ResetingPosition))
            {
                if (turn > 1f)
                {
                    turn = 0;
                    //If player is above us
                    if (player.CurrentNode.Position.x < currentNode.Position.x)
                    {
                        //Check if we can move up.
                        if (!currentNode.Up.Occupied)
                        {
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Up;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                        }
                    }
                    //If player is above us
                    else if (player.CurrentNode.Position.x > currentNode.Position.x)
                    {
                        //Check if we can move up.
                        if (!currentNode.Down.Occupied)
                        {
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Down;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                        }
                    }
                    //If they are in front of us, ATTACK!.
                    else if(this.transform.position.z == currentNode.transform.position.z && this.transform.position.x == currentNode.transform.position.x)
                    {
                        AnimatorClipInfo[] temp = mechAnima.GetCurrentAnimatorClipInfo(0);
                        if (temp.Length > 0 && temp[0].clip.name.Equals("SamuraiWait1"))
                        {
                            mechAnima.SetBool("Attack", true);
                            //Weapons.Hitbox b = Instantiate(bullet).GetComponent<Weapons.Hitbox>();
                            // b.transform.position = currentNode.Left.transform.position;
                            //b.CurrentNode = currentNode.Left;
                            //sfx.PlaySong(0);
                            Attacking = true;
                            print("initiate attack");
                        }
                    }
                }
            }
            else if(Attacking)
            {
                if (!mechAnima.GetBool("HoldAttack") && mechAnima.GetCurrentAnimatorClipInfo(0).Length > 0)
                {
                    if (mechAnima.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("SamuraiStabEnter"))
                    {
                        mechAnima.SetBool("HoldAttack", true);
                        mechAnima.SetBool("Attack", false);
                    }
                }
                if((player.transform.position.z +2f)< this.transform.position.z)
                {
                    print("charge forward:" +" p: "+player.transform.position.z  +" vs "+transform.position.z);

                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
                }else
                {
                    print("initiate withdrawal");

                    ResetingPosition = true;
                    mechAnima.SetBool("HoldAttack", false);
                    mechAnima.SetBool("WithdrawAttack", true);
                    Attacking = false;
                }
            }
            else if(ResetingPosition)
            {
                if (currentNode.transform.position.z > this.transform.position.z)
                {
                    print("withdrawing");
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.3f);
                }
                else { mechAnima.SetBool("WithdrawAttack", false); ResetingPosition = false; print("witdrawed"); transform.position = currentNode.transform.position; }


            }
            else
            {
                mechAnima.SetBool("Hurt", true);
                turn = 0;
            }

            //transform.position = currentNode.transform.position;
        }

        protected override void Render(bool render)
        {
            foreach (SkinnedMeshRenderer b in body)
                b.enabled = render;
        }
    }
}

