using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public GameObject[] pages;
    public Slider pageSlider;
    [SerializeField] private int currentPage = 0;

    public void NextPage()
    {
        if (currentPage == pages.Length - 1) currentPage = 0;
        else currentPage++;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        pages[currentPage].SetActive(true);
        pageSlider.value = currentPage;
    }

    public void PreviousPage()
    {
        if (currentPage == 0) currentPage = pages.Length - 1;
        else currentPage--;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        pages[currentPage].SetActive(true);
        pageSlider.value = currentPage;
    }
}
