using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace K12.EYP.ScubaSuit.Controller
{
    public class GameController : MonoBehaviour
    {
        public int gridRows;
        public int gridCols;
        public float offsetX;
        public float offsetY;

        [SerializeField] private MainCard originalCard;
        //private List<GameObject> game = new List<GameObject>();// make a list of gameobjects 
        [SerializeField] GameObject round2;
        [SerializeField] GameObject round1;
        [SerializeField] GameObject round3;
        [SerializeField] GameObject kudos;
        [SerializeField] GameObject levelCompletePanel;

        [SerializeField] AudioManager audioManager;

        [SerializeField] private Sprite[] roundImages;
        //[SerializeField] private Sprite[] round2Images;
        //[SerializeField] private Sprite[] round3Images;

        private MainCard firstRevealed;
        private MainCard secondRevealed;

        [SerializeField]private DontDestroy dd;

        int level;
        int[] numbers;
        int count;


        private void Start()
        {
            level = SceneManager.GetActiveScene().buildIndex;
            pickLevel(level);
            count = dd.count;
            kudos.SetActive(false);
            levelCompletePanel.SetActive(false);
        }


        void pickLevel(int level)
        {
            switch (level)
            {
                case 1:
                    numbers = new int[] { 0, 0, 1, 1 };
                    LevelSetup(numbers, 2, 2, 10, 3);
                    break;
                case 2:
                    numbers = new int[] { 0, 0, 1, 1, 2, 2 };
                    LevelSetup(numbers, 2, 3, 5, 3);
                    break;
                case 3:
                    numbers = new int[] { 0, 0, 1, 1, 2, 2 };
                    LevelSetup(numbers, 2, 3, 5, 3);
                    break;
                case 4:
                    numbers = new int[] { 0, 0, 1, 1, 2, 2 };
                    LevelSetup(numbers, 2, 3, 5, 3);
                    break;
            }
        }
        void LevelSetup(int[] numbers, int rows, int cols, int offX, int offY)
        {
            gridCols = cols;
            gridRows = rows;
            offsetX  = offX;
            offsetY  = offY;
            //level setup
            //add this in list gameobject card
            Vector3 startpos = originalCard.transform.position;
            numbers = ShuffleArray(numbers);
            for (int i = 0; i < gridCols; i++)
            {
                for (int j = 0; j < gridRows; j++)
                {
                    MainCard card;
                    if (i == 0 && j == 0)
                    {
                        card = originalCard;
                    }
                    else
                    {
                        card = Instantiate(originalCard) as MainCard;
                    }
                    int index = j * gridCols + i;
                    int id = numbers[index];

                    card.ChangeSprite(id, roundImages[id]);
                    float posX = (offsetX * i) + startpos.x;
                    float posY = (offsetY * j) + startpos.y;
                    card.transform.position = new Vector3(posX, posY, startpos.z);
                }
            }

        }
       
        IEnumerator ChangeRound(int count)
        {
            yield return new WaitForSeconds(.5f);
            switch(count)
            {
                case 4:
                    if(level ==1)
                    {
                        audioManager.PlaySound("CorrectTap");
                        DestroyClones();
                        originalCard.Unreveal();
                        round1.SetActive(false);
                        round2.SetActive(true);
                        dd.SetCount(count);
                    }
                    else
                    {
                        break;
                    }
                    break;
                case 3:
                    if (level == 1)
                    {
                        break;
                    }
                    else
                    {
                        audioManager.PlaySound("CorrectTap");
                        DestroyClones();
                        originalCard.Unreveal();
                        round1.SetActive(false);
                        round2.SetActive(true);
                        dd.SetCount(count);
                    }
                    break;
                case 2:
                    if (level == 1)
                    {
                        audioManager.PlaySound("CorrectTap");
                        StartCoroutine(LevelComplete());
                    }
                    else
                    {
                        break;
                    }
                    break;
                case 0:
                    if (level == 4)
                    {
                        audioManager.PlaySound("CorrectTap");
                        DestroyClones();
                        originalCard.Unreveal();
                        round2.SetActive(false);
                        round3.SetActive(true);
                        dd.SetCount(count);
                    }
                    else
                    {
                        audioManager.PlaySound("CorrectTap");
                        StartCoroutine(LevelComplete());
                    }
                    break;
                case -3:
                    audioManager.PlaySound("CorrectTap");
                    StartCoroutine(LevelComplete());
                    break;
            }
        }

        void DestroyClones()
        {
            var clones = GameObject.FindGameObjectsWithTag("Clone");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }
        }

        private int[] ShuffleArray(int[] numbers)
        {
            int[] newArray = numbers.Clone() as int[];
            for (int i = 0; i < newArray.Length; i++)
            {
                int temp = newArray[i];
                int r = Random.Range(i, newArray.Length);
                newArray[i] = newArray[r];
                newArray[r] = temp;
            }
            return newArray;
        }

        public bool canReveal
        {
            get { return secondRevealed == null; }
        }

        public void CardRevealed(MainCard card)
        {
            if (firstRevealed == null)
            {
                firstRevealed = card;
            }
            else
            {
                secondRevealed = card;
                StartCoroutine(CheckMatch());
            }
        }
         
        private IEnumerator CheckMatch()
        {
            if (firstRevealed.id == secondRevealed.id)
            {
                audioManager.PlaySound("CorrectMatch");
                //use loop destroy list items.
                count--;
                Debug.LogError(count);
                StartCoroutine(ChangeRound(count));
            }
            else
            {
                audioManager.PlaySound("Error");
                yield return new WaitForSeconds(0.5f);
                firstRevealed.Unreveal();
                secondRevealed.Unreveal();
            }
            firstRevealed = null;
            secondRevealed = null;
        }

        private IEnumerator LevelComplete()
        {
            yield return new WaitForSeconds(0.5f);
            audioManager.PlaySound("Kudos");
            kudos.SetActive(true);
            yield return new WaitForSeconds(2f);
            kudos.SetActive(false);
            levelCompletePanel.SetActive(true);
        }
    }
}
