using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K12.EYP.ScubaSuit.Controller
{
    public class MainCard : MonoBehaviour
    {
        [SerializeField] GameObject backCard;
        [SerializeField] GameController gameController;
        [SerializeField] AudioManager audioManager;
        [SerializeField] DontDestroy dontDestroy;

        private int cardId;

        private void Awake()
        {
            StartCoroutine(OnLevelStart());
        }
        public void OnMouseDown()
        {
            if (!dontDestroy.isPlaying)
            {
                audioManager.PlaySound("Tap");
                if (backCard.activeSelf && gameController.canReveal)
                {
                    backCard.SetActive(false);
                    gameController.CardRevealed(this);
                }
            }
        }

        public int id
        {
            get
            {
                return cardId;
            }
        }

        public void ChangeSprite(int id, Sprite image)
        {
            cardId = id;
            GetComponent<SpriteRenderer>().sprite = image;
        }

        public void Unreveal()
        {
            backCard.SetActive(true);
        }

        IEnumerator OnLevelStart()
        {
            backCard.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            backCard.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            backCard.SetActive(true);
        }

    }
}
