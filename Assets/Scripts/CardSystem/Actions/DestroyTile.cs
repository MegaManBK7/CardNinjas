﻿using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.CardSystem.Actions
{
	class DestroyTile : Action
	{
		public override void useCard(Character actor)
		{
            Weapons.Projectiles.GroundDestroyer temp = MonoBehaviour.Instantiate(prefab).GetComponent<Weapons.Projectiles.GroundDestroyer>();
            temp.Damage = damage;
            temp.Distance = range;
            temp.Piercing = true;
            temp.Speed = 5;
            temp.TimesCanPierce = -1;
            temp.IsFlying = true;
            temp.Owner = actor.gameObject;
            Util.AddElements.AddElementByEnum(temp.gameObject, element, true);
            if (actor.Direction == Util.Enums.Direction.Left)
			{
                temp.Direction = Util.Enums.Direction.Left;
                temp.transform.position = actor.CurrentNode.Left.transform.position;
                temp.CurrentNode = actor.CurrentNode.Left;
            }

			if (actor.Direction == Util.Enums.Direction.Right)
            {
                temp.Direction = Util.Enums.Direction.Right;
                temp.transform.position = actor.CurrentNode.Right.transform.position;
                temp.CurrentNode = actor.CurrentNode.Right;
            }
		}
	}
}