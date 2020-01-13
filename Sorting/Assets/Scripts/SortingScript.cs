using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingScript : MonoBehaviour
{
    public float x;
    public float y;
    public float speed;
    public int boxSize;
    public int numberOfBoxes;
    public int algorithm;
    public Texture2D tex;
    public GameObject[] Boxes;

    public void StartSort()
    {
        InitializeRandom();

        switch (algorithm)
        {
            case 1:
                StartCoroutine(SelectionSort(Boxes));
                break;
            case 2:
                StartCoroutine(BubbleSort(Boxes));
                break;
            case 3:
                StartCoroutine(InsertionSort(Boxes));
                break;
            case 4:
                sort(Boxes);
                break;
        }
    }

    void InitializeRandom()
    {
        Boxes = new GameObject[numberOfBoxes];

        for (int i = 0; i < numberOfBoxes; i++)
        {
            GameObject box = new GameObject("Box");
            SpriteRenderer renderer = box.AddComponent<SpriteRenderer>();
            Sprite genSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0, 0), 1);

            renderer.sprite = genSprite;

            box.transform.localScale = new Vector2(boxSize/100f, Mathf.Round(Random.Range(.01f, .1f) * 100f) / 100f);
            box.transform.position = new Vector2((numberOfBoxes / x) + ((.4f * i) * boxSize), y);
            box.tag = "Box";

            Boxes[i] = box;
        }
    }

    IEnumerator SelectionSort(GameObject[] unsortedList)
    {
        int min;
        GameObject temp;

        for (int i = 0; i < unsortedList.Length - 1; i++)
        {
            min = i;
            yield return new WaitForSeconds(speed);

            for (int j = i + 1; j < unsortedList.Length; j++)
            {
                if (unsortedList[j].transform.localScale.y < unsortedList[min].transform.localScale.y)
                {
                    min = j;
                }
            }

            if (min != 1)
            {
                temp = unsortedList[i];
                unsortedList[i] = unsortedList[min];
                unsortedList[min] = temp;

                Vector2 tempPos = unsortedList[i].transform.localPosition;

                unsortedList[i].transform.position = new Vector2(unsortedList[min].transform.localPosition.x, y);
                unsortedList[min].transform.position = new Vector2(tempPos.x, y);
            }
        }
    }

    IEnumerator BubbleSort(GameObject[] unsortedList)
    {
        for (int i = 0; i < unsortedList.Length; i++)
        {
            for (int j = 0; j < unsortedList.Length - 1; j++)
            {
                if (unsortedList[j].transform.localScale.y > unsortedList[j + 1].transform.localScale.y)
                {
                    yield return new WaitForSeconds(speed);

                    GameObject temp = unsortedList[j + 1];
                    unsortedList[j + 1] = unsortedList[j];
                    unsortedList[j] = temp;

                    Vector2 tempPos = temp.transform.localPosition;

                    unsortedList[j].transform.position = new Vector2(unsortedList[j + 1].transform.localPosition.x, y);
                    unsortedList[j + 1].transform.position = new Vector2(tempPos.x, y);
                }
            }
        }
    }

    IEnumerator InsertionSort(GameObject[] unsortedList)
    {
        for (int i = 0; i < unsortedList.Length; i++)
        {
            int j = i;

            while (j > 0 && unsortedList[j - 1].transform.localScale.y > unsortedList[j].transform.localScale.y)
            {
                GameObject temp = unsortedList[j];
                unsortedList[j] = unsortedList[j - 1];
                unsortedList[j - 1] = temp;

                Vector2 tempPos = temp.transform.localPosition;

                unsortedList[j - 1].transform.position = new Vector2(unsortedList[j].transform.localPosition.x, y);
                unsortedList[j].transform.position = new Vector2(tempPos.x, y);

                j--;

                yield return new WaitForSeconds(speed);
            }
        }
    }

    IEnumerator HeapSort(GameObject[] unsortedList, int n, int i)
    {
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < unsortedList.Length && unsortedList[l].transform.localPosition.y > unsortedList[largest].transform.localPosition.y)
        {
            largest = l;
        }

        if (r < n && unsortedList[r].transform.localPosition.y > unsortedList[largest].transform.localPosition.y)
        {
            largest = r;
        }

        if (largest != i)
        {
            GameObject swap = unsortedList[i];
            unsortedList[i] = unsortedList[largest];
            unsortedList[largest] = swap;

            Vector2 tempPos = swap.transform.localPosition;

            unsortedList[i].transform.position = new Vector2(unsortedList[largest].transform.localPosition.x, y);
            unsortedList[largest].transform.position = new Vector2(tempPos.x, y);

            yield return new WaitForSeconds(speed);

            HeapSort(Boxes, n, largest);
        }
    }

    void sort(GameObject[] unsortedList)
    {
        for (int i = unsortedList.Length / 2 - 1; i >= 0; i--)
        {
            HeapSort(Boxes, unsortedList.Length, i);
        }

        for (int i = unsortedList.Length - 1; i >=  0; i--)
        {
            GameObject temp = unsortedList[0];
            unsortedList[0] = unsortedList[i];
            unsortedList[i] = temp;

            HeapSort(Boxes, i, 0);
        }
    }
}