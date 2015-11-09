using UnityEngine;
using Assets.Scripts.Util;
using Assets.Scripts.Grid;

namespace Assets.Scripts.Weapons
{
    public class BackNForth : Hitbox
    {
        private int bounceCount = 0;
        override void Update()
        {
            if (Managers.GameManager.State == Enums.GameStates.Battle)
            {
                if (moveCompleted)
                {
                    switch (direction)
                    {
                        case Enums.Direction.Left:
                            if (isFlying)
                            {
                                if (currentNode.panelExists(Enums.Direction.Left))
                                    target = currentNode.Left;
                                else
                                    if (bounceCount > 0)
                                    {
                                        dead = true;
                                    }
                                bounceCount++;
                                direction = Enums.Direction.Right;
                            }
                            else
                            {
                                if (currentNode.panelNotDestroyed(Enums.Direction.Left))
                                    target = currentNode.Left;
                                else
                                    if (bounceCount > 0)
                                    {
                                        dead = true;
                                    }
                                bounceCount++;
                                direction = Enums.Direction.Right;
                            }
                            moveCompleted = false;
                            break;
                        case Enums.Direction.Right:
                            if (isFlying)
                            {
                                if (currentNode.panelExists(Enums.Direction.Right))
                                    target = currentNode.Right;
                                else
                                    if (bounceCount > 0)
                                    {
                                        dead = true;
                                    }
                                bounceCount++;
                                direction = Enums.Direction.Left;
                            }
                            else
                            {
                                if (currentNode.panelNotDestroyed(Enums.Direction.Right))
                                    target = currentNode.Right;
                                else
                                    if (bounceCount > 0)
                                    {
                                        dead = true;
                                    }
                                bounceCount++;
                                direction = Enums.Direction.Left;
                            }
                            moveCompleted = false;
                            break;
                        default: deathTime -= Time.deltaTime; break;
                    }
                }
                if (deathTime < 0 || dead || distance == 0)
                    Destroy(this.gameObject);
                Move();
            }
        }
    }
}