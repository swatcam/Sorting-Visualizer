using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSortMenu : MonoBehaviour
{
    public SortingScript SelectionSort;
    private SortingScript ActiveSorter;
    public TMPro.TMP_InputField InputNumberOfBoxes;
    public TMPro.TMP_InputField speed;
    public TMPro.TMP_Dropdown algorithm;

    public void StartSort()
    {
        if (GameObject.FindGameObjectsWithTag("Box").Length > 0)
        {
            DestroyBoxes();
        }

        ActiveSorter = Instantiate(SelectionSort);
        ActiveSorter.numberOfBoxes = Convert.ToInt32(InputNumberOfBoxes.text);
        ActiveSorter.algorithm = algorithm.value;
        ActiveSorter.speed = float.Parse(speed.text);
        ActiveSorter.StartSort();
    }

    public void ResetSort()
    {
        DestroyBoxes();
        Invoke("StartSort", 0);
    }

    void DestroyBoxes()
    {
        foreach (GameObject box in GameObject.FindGameObjectsWithTag("Box"))
        {
            Destroy(box.gameObject);
        }

        Destroy(ActiveSorter.gameObject);
    }
}
