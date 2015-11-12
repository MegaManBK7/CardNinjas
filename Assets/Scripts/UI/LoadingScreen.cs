using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.CardSystem;
using Assets.Scripts.Util;

namespace Assets.Scripts.UI
{
	public class LoadingScreen : MonoBehaviour
	{
        public delegate void LoadingAction();
        public static event LoadingAction BeginLoadLevel, FinishedLoading;

		public static LoadingScreen instance;
		private static string levelToLoad = "MenuTest";

		private Transform loadingCard, randomCard, activeCard;
        private const float START_Y_ROT = -90, TURN_AMOUNT = 180;

		private Canvas win;

        [SerializeField]
        private Text cardName, type, damage, range, description;
        [SerializeField]
        private Image image, cardBase;

        private float vel, counter, turnStep = 0.75f, smoothTime = 0.1f;

        private List<Card> allCards;

        AsyncOperation async;

        private float timer = 0, timeToChange = 5f;

		void Awake()
		{
			if(instance == null)
			{
				instance = this;
				DontDestroyOnLoad(this.gameObject);
			}
			else if(instance != this)
			{
				Destroy(this.gameObject);
			}

            win = this.GetComponent<Canvas>();
            loadingCard = GameObject.Find("Loading Card").transform;
            randomCard = GameObject.Find("Random Card").transform;
            randomCard.gameObject.SetActive(false);

            allCards = FindObjectOfType<CardList>().Cards;

            Init();

			LoadLevel(levelToLoad);
		}

        private void Init()
        {
            activeCard = loadingCard;
            activeCard.eulerAngles = new Vector3(0, 0, 0);
            counter = TURN_AMOUNT / 2;
        }

        void Update()
        {
            if (async != null)
            {
                LoadInProgress();
            }
        }

        private void LoadInProgress()
        {
            timer += Time.deltaTime;
            activeCard.Rotate(0f, -turnStep, 0f);
            counter += turnStep;
            if (counter > TURN_AMOUNT) SwapCards();
            if (timer >= timeToChange && async.progress >= 0.9f) FinishLoading();
        }

        private void SwapCards()
        {
            if(activeCard.Equals(loadingCard))
            {
                randomCard.gameObject.SetActive(true);
                loadingCard.gameObject.SetActive(false);
                activeCard = randomCard;

                Card nextCard = allCards[Random.Range(0, allCards.Count)];

                cardName.text = nextCard.Name;
                type.text = nextCard.Type.ToString();
                damage.text = nextCard.Action.Damage.ToString();
                range.text = nextCard.Action.Range.ToString();
                description.text = nextCard.Description;

                image.sprite = nextCard.Image;
                cardBase.color = CustomColor.ColorFromElement(nextCard.Element);
            }
            else
            {
                randomCard.gameObject.SetActive(false);
                loadingCard.gameObject.SetActive(true);
                activeCard = loadingCard;
            }
            counter = 0;
            activeCard.eulerAngles = new Vector3(0, -START_Y_ROT, 0);
        }

        public void LoadLevel(string level)
		{
			win.enabled = true;
			timer = 0;
            LevelToLoad = level;
            if (BeginLoadLevel != null) BeginLoadLevel();
            async = Application.LoadLevelAsync(level);
            async.allowSceneActivation = false;
        }

        private void FinishLoading()
        {
            async.allowSceneActivation = true;
            timer = 0;
            if (FinishedLoading != null) FinishedLoading();
        }

        void OnLevelWasLoaded(int level)
        {
            win.enabled = false;
        }

		public static string LevelToLoad
		{
			set { levelToLoad = value; }
			get { return levelToLoad; }
		}
	}
}
