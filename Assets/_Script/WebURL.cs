using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebURL : MonoBehaviour
{
    public string webToOpen;
    public void OpenWebsite()
    {
        Application.OpenURL(webToOpen);
    }
}
