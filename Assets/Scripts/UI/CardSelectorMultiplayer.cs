using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Util;
using Assets.Scripts.CardSystem;

namespace Assets.Scripts.UI
{
    public class CardSelectorMultiplayer : MonoBehaviour
    {
        public delegate void CardSelectorAction();
        public static event CardSelectorAction CardSelectorEnabled, CardSelectorDisabled, CardsSelected, CardsDeselected;

        private static int playerIndex;
        private int thisPlayerIndex;
        private int numSelections = 0;
        private static int totalSelections = 0;
        private const int NUM_SELECTIONS = 8, MAX_SELECTIONS = 4;
        private const int ELEMENT_INDEX = 0, CHILD_IMAGE_INDEX = 1;

        private Player.Player player;
        private Deck deck;
        private List<Card> selectedCards, selectionOptions;
        private List<int> finalMap;
        private GameObject[] selectionButtons;
        private Image[] finalButtons;

        private static Image[] xboxButtons;
        private static Text[] keyboardButtons;

        [SerializeField]
        private List<Sprite> xboxButtonSprites;

        void OnEnable()
        {
            SelectionTimer.TimerFinish += EnableCanvas;
            CardTimer.TimerFinish += Okay;
        }
        void OnDisable()
        {
            SelectionTimer.TimerFinish -= EnableCanvas;
            CardTimer.TimerFinish -= Okay;
        }

        void Start()
        {
            //should find players provided they are named in the fashion: "Player 1" or "Player 42"
            thisPlayerIndex = ++playerIndex;
            player = GameObject.Find("Player " + playerIndex).GetComponent<Player.Player>();

            deck = player.Deck;
            selectionOptions = new List<Card>();
            selectedCards = new List<Card>();
            finalMap = new List<int>();

            selectionButtons = new GameObject[NUM_SELECTIONS];
            finalButtons = new Image[MAX_SELECTIONS];
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Selection").OrderBy(go => go.name).ToArray();
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].tag = "Selection " + thisPlayerIndex.ToString();
                selectionButtons[i] = gos[i];
            }
            
            gos = GameObject.FindGameObjectsWithTag("Final").OrderBy(go => go.name).ToArray();
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].tag = "Final " + thisPlayerIndex.ToString();
                finalButtons[i] = gos[i].GetComponent<Image>();
            }

            if (xboxButtons == null)
            {
                xboxButtons = new Image[xboxButtonSprites.Count];
                gos = GameObject.FindGameObjectsWithTag("Xbox Button").OrderBy(go => go.name).ToArray();
                for (int i = 0; i < gos.Length; i++)
                {
                    xboxButtons[i] = gos[i].GetComponent<Image>();
                }

                xboxButtons[0].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard0, thisPlayerIndex)));
                xboxButtons[1].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard1, thisPlayerIndex)));
                xboxButtons[2].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard2, thisPlayerIndex)));
                xboxButtons[3].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard3, thisPlayerIndex)));
                xboxButtons[4].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard4, thisPlayerIndex)));
                xboxButtons[5].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard5, thisPlayerIndex)));
                xboxButtons[6].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard6, thisPlayerIndex)));
                xboxButtons[7].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard7, thisPlayerIndex)));

                //xboxButtons[8].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard0, thisPlayerIndex)));
                //xboxButtons[9].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard1, thisPlayerIndex)));
                //xboxButtons[10].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard2, thisPlayerIndex)));
                //xboxButtons[11].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard3, thisPlayerIndex)));
                //xboxButtons[12].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard4, thisPlayerIndex)));
                //xboxButtons[13].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard5, thisPlayerIndex)));
                //xboxButtons[14].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard6, thisPlayerIndex)));
                //xboxButtons[15].sprite = xboxButtonSprites.Find(x => x.name.Contains(CustomInput.gamePadButton(CustomInput.UserInput.PickCard7, thisPlayerIndex)));
            }

            if (keyboardButtons == null)
            {
                keyboardButtons = new Text[8];
                gos = GameObject.FindGameObjectsWithTag("Keyboard Button").OrderBy(go => go.name).ToArray();
                for (int i = 0; i < gos.Length; i++)
                {
                    keyboardButtons[i] = gos[i].GetComponent<Text>();
                }

                keyboardButtons[0].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard0, thisPlayerIndex).ToString();
                keyboardButtons[1].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard1, thisPlayerIndex).ToString();
                keyboardButtons[2].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard2, thisPlayerIndex).ToString();
                keyboardButtons[3].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard3, thisPlayerIndex).ToString();
                keyboardButtons[4].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard4, thisPlayerIndex).ToString();
                keyboardButtons[5].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard5, thisPlayerIndex).ToString();
                keyboardButtons[6].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard6, thisPlayerIndex).ToString();
                keyboardButtons[7].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard7, thisPlayerIndex).ToString();

                /*
                keyboardButtons[8].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard0, thisPlayerIndex).ToString();
                keyboardButtons[9].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard1, thisPlayerIndex).ToString();
                keyboardButtons[10].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard2, thisPlayerIndex).ToString();
                keyboardButtons[11].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard3, thisPlayerIndex).ToString();
                keyboardButtons[12].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard4, thisPlayerIndex).ToString();
                keyboardButtons[13].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard5, thisPlayerIndex).ToString();
                keyboardButtons[14].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard6, thisPlayerIndex).ToString();
                keyboardButtons[15].text = CustomInput.keyBoardKey(CustomInput.UserInput.PickCard7, thisPlayerIndex).ToString();
                */
            }

            DrawPossibleSelections();
            InitializeDisplayData();
            UpdateFinalDisplayData();
        }

        void Update()
        {
            if (Managers.GameManager.State == Enums.GameStates.CardSelection)
            {
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.Attack))
                {
                    DrawPossibleSelections();
                }

                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard0, thisPlayerIndex)) SelectCard(0);
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard1, thisPlayerIndex)) SelectCard(1);
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard2, thisPlayerIndex)) SelectCard(2);
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard3, thisPlayerIndex)) SelectCard(3);
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard4, thisPlayerIndex)) SelectCard(4);
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard5, thisPlayerIndex)) SelectCard(5);
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard6, thisPlayerIndex)) SelectCard(6);
                if (CustomInput.BoolFreshPress(CustomInput.UserInput.PickCard7, thisPlayerIndex)) SelectCard(7);
            }
        }

        private void DrawPossibleSelections()
        {
            selectionOptions = deck.DrawHand();
            for (int i = 0; i < NUM_SELECTIONS; i++)
            {
                if (selectionOptions != null && i < selectionOptions.Count && selectionOptions[i] != null)
                {
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = selectionOptions[i].Image;
                }
                else
                {
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = null;
                }
            }
        }

        public void SelectCard(int index)
        {
            if (finalMap.Contains(index))
            {
                selectedCards.Remove(selectionOptions[index]);
                finalMap.Remove(index);
                UpdateFinalDisplayData();
                if (CardsDeselected != null) CardsDeselected();
                if (--numSelections < 0) numSelections = 0;
                if (--totalSelections < 0) totalSelections = 0;
            }
            else
            {
                if (numSelections >= MAX_SELECTIONS) return;
                numSelections++;
                totalSelections++;
                selectedCards.Add(selectionOptions[index]);
                finalMap.Add(index);
                UpdateFinalDisplayData();
            }
        }

        public void EnableCanvas()
        {
            selectedCards.Clear();
            finalMap.Clear();
            numSelections = 0;
            totalSelections = 0;
            transform.GetComponent<Canvas>().enabled = true;
            DrawPossibleSelections();
            UpdateDisplayData();
            UpdateFinalDisplayData();
            if (CardSelectorEnabled != null) CardSelectorEnabled();
        }

        public void Okay()
        {
            if (selectionOptions != null)
            {
                for (int i = 0; i < selectedCards.Count; i++)
                {
                    selectionOptions.Remove(selectedCards[i]);
                }
                deck.ReturnUsedCards(selectionOptions);
                player.AddCardsToHand(selectedCards);
            }
            transform.GetComponent<Canvas>().enabled = false;
            if (CardSelectorDisabled != null) CardSelectorDisabled();
        }

        void OnLevelWasLoaded(int i)
        {
            playerIndex = 0;
        }

        #region DISPLAY_DATA
        public void UpdateFinalDisplayData()
        {
            for (int i = 0; i < MAX_SELECTIONS; i++)
            {
                if (i < finalMap.Count)
                {
                    finalButtons[i].color = CustomColor.Convert255(0, 92, 122);
                }
                else
                {
                    finalButtons[i].color = CustomColor.Convert255(255, 255, 255);
                }
            }
            if(totalSelections >= MAX_SELECTIONS)
            {
                if (CardsSelected != null) CardsSelected();
            }
        }

        public void InitializeDisplayData()
        {
            UpdateDisplayData();
        }

        public void UpdateDisplayData()
        {
            for (int i = 0; i < NUM_SELECTIONS; i++)
            {
                if (selectionOptions != null && i < selectionOptions.Count)
                {
                    selectionButtons[i].transform.GetChild(ELEMENT_INDEX).GetComponent<Image>().color = GetElementDisplay(selectionOptions[i].Element);
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.white;
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = selectionOptions[i].Image;
                }
                else
                {
                    selectionButtons[i].transform.GetChild(ELEMENT_INDEX).GetComponent<Image>().color = GetElementDisplay(Enums.Element.None);
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().color = Color.black;
                    selectionButtons[i].transform.GetChild(CHILD_IMAGE_INDEX).GetComponent<Image>().sprite = null;
                }
            }
        }
        #endregion
        
        public Color GetElementDisplay(Enums.Element element)
        {
            switch (element)
            {
                case Enums.Element.Fire:
                    return CustomColor.Convert255(175.0f, 30.0f, 30.0f);
                case Enums.Element.Water:
                    return CustomColor.Convert255(30.0f, 30.0f, 175.0f);
                case Enums.Element.Thunder:
                    return CustomColor.Convert255(225.0f, 225.0f, 30.0f);
                case Enums.Element.Earth:
                    return CustomColor.Convert255(85.0f, 50.0f, 15.0f);
                case Enums.Element.Wood:
                    return CustomColor.Convert255(30.0f, 175.0f, 30.0f);
                default:
                    return CustomColor.Convert255(128.0f, 128.0f, 128.0f);
            }
        }
    }
}