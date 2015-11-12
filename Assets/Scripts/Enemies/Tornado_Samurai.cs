using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Enemies
{
    class Tornado_Samurai : Enemy
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
            mechAnima.SetBool("VertiSlash", true);
        }

        protected override void RunAI()
        {

            //We change turns each second
            turn += Time.deltaTime;
            if (!hit)
            {
                if (turn > 1f)
                {
                    mechAnima.SetBool("Attack", false);

                    //mechAnima.SetBool("Hop", false);

                    turn = 0;

                    if (currentNode.Left.Type == Util.Enums.FieldType.Blue)
                    {
                        if (!currentNode.Left.Occupied)
                        {
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Left;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                        }
                        else if (currentNode.Left.Down != null && !currentNode.Left.Down.Occupied && !currentNode.Down.Occupied)
                        {
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Down;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                        }
                        else if (currentNode.Left.Up != null && !currentNode.Left.Up.Occupied && !currentNode.Up.Occupied)
                        {
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Up;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                        }
                    }
                    //If player is above us
                    else if (player.CurrentNode.Position.x < currentNode.Position.x)
                    {
                        //Check if we can move up.
                        if (!currentNode.Up.Occupied)
                        {
                            currentNode.clearOccupied();//Say we aren't here
                            currentNode = currentNode.Up;//Say we're there
                            currentNode.Owner = (this);//Tell the place we own it.
                            //mechAnima.SetBool("Hop", true);

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
                            //mechAnima.SetBool("Hop", true);
                        }
                    }
                    //If they are in front of us, ATTACK!.
                    else
                    {
                        AnimatorClipInfo[] temp = mechAnima.GetCurrentAnimatorClipInfo(0);
                        if (temp.Length > 0 && temp[0].clip.name.Equals("SamuraiWait1"))
                        {
                            mechAnima.SetBool("Attack", true);
                            //Weapons.Hitbox b = Instantiate(bullet).GetComponent<Weapons.Hitbox>();
                            // b.transform.position = currentNode.Left.transform.position;
                            //b.CurrentNode = currentNode.Left;
                            //sfx.PlaySong(0);

                            Grid.GridNode t = currentNode;

                            do
                            {
                                t = t.Left;
                            } while (t.Left != null && t.Left.Type != Util.Enums.FieldType.Red);

                            while (t.Up != null)
                            {
                                t = t.Up;
                            }

                            t = t.Right;
                            Grid.GridNode s = t;

                            while(s.Down != null)
                            {
                                s = s.Down;
                            }

                            Weapons.TornadoHitbox b = Instantiate(bullet).GetComponent<Weapons.TornadoHitbox>();

                            b.transform.position = t.transform.position;
                            b.CurrentNode = t;
                            b.zTargetDistance = player.transform.position.z;
                            b.xTargetDistance = s.transform.position.x;
                            b.zStartingPoint = t.transform.position.z;
                            b.xStartingPoint = t.transform.position.x;

                        }
                    }
                    transform.position = currentNode.transform.position;
                }
            }
            /*else if (Attacking)
            {
                print("anim Hold Attack? " + mechAnima.GetBool("HoldAttack") + " clip info array: " + mechAnima.GetCurrentAnimatorClipInfo(0).Length);
                if (!mechAnima.GetBool("HoldAttack") && mechAnima.GetCurrentAnimatorClipInfo(0).Length > 0)
                {
                    if (mechAnima.GetCurrentAnimatorClipInfo(0)[0].clip.name.Equals("SamuraiStabEnter"))
                    {
                        mechAnima.SetBool("HoldAttack", true);
                        mechAnima.SetBool("Attack", false);
                    }
                }

                print("charging into the Player? " + player.transform.position.z + " vs " + this.transform.position.z);


                if ((player.transform.position.z + 2f) < this.transform.position.z)
                {
                    print("charge forward:" + " p: " + player.transform.position.z + " vs " + transform.position.z);

                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
                }
                else
                {
                    print("initiate withdrawal");

                    ResetingPosition = true;
                    mechAnima.SetBool("Attack", false);
                    mechAnima.SetBool("HoldAttack", false);
                    mechAnima.SetBool("WithdrawAttack", true);
                    Attacking = false;
                    Weapons.Hitbox b = Instantiate(bullet).GetComponent<Weapons.Hitbox>();

                    Grid.GridNode t = currentNode;

                    while (t.transform.position.z >= this.transform.position.z && t.Left != null)
                    {
                        t = t.Left;
                    }


                    b.transform.position = t.transform.position;
                    b.CurrentNode = t;
                }
            }
            else if (ResetingPosition)
            {
                print("charging away from Player? " + currentNode.transform.position.z + " vs " + this.transform.position.z);
                AnimatorClipInfo[] temp = mechAnima.GetCurrentAnimatorClipInfo(0);

                if (currentNode.transform.position.z > this.transform.position.z)
                {
                    print("withdrawing");
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.3f);
                }
                else if (temp.Length > 0 && temp[0].clip.name.Equals("SamuraiStabExit"))
                {
                    mechAnima.SetBool("WithdrawAttack", false);
                    ResetingPosition = false; print("witdrawed");
                    transform.position = currentNode.transform.position;
                }


            }*/
            else
            {
                mechAnima.SetBool("Hurt", true);
                turn = 0;
            }

        }

        protected override void Render(bool render)
        {
            foreach (SkinnedMeshRenderer b in body)
                b.enabled = render;
        }
    }
}

