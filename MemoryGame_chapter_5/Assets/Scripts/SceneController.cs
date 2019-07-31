using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2.0f;
    public const float offsetY = 2.5f;

    [SerializeField]
    private MemoryCard originalCard;

    [SerializeField]
    private Sprite[] images;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    private int _score = 0;

    [SerializeField]
    private TextMesh scoreLabel;

    private int[] generateStartPairNumbersArray()
    {
        int[] newNumbers = new int[images.Length * 2];

        int count = 0;
        for (int i = 0; i < newNumbers.Length; i+=2)
        {
            newNumbers[i] = count;
            newNumbers[i+1] = count;

            ++count;
        }

        return newNumbers;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = generateStartPairNumbersArray();
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; ++i)
        {
            for (int j = 0; j < gridRows; ++j)
            {
                MemoryCard card;

                if (i == 0 && j == 0)
                    card = originalCard;
                else
                    card = Instantiate(originalCard) as MemoryCard;

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; ++i)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            UpdateScore(1);
        }
        else
        {
            UpdateScore(-1);

            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    private void UpdateScore(int incrementScore)
    {
        _score = _score + incrementScore;
        scoreLabel.text = "Score: " + _score;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
