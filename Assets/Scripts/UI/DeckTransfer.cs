using UnityEngine;
using Assets.Scripts.CardSystem;
using Assets.Scripts.Util;

namespace Assets.Scripts.UI
{
    public class DeckTransfer : MonoBehaviour
    {
        private Deck deck;

        private Enums.Element element;

        public Deck Deck
        {
            get { return deck; }
            set { deck = value; }
        }

        public Enums.Element Element
        {
            get { return element; }
            set { element = value; }
        }
    }
}