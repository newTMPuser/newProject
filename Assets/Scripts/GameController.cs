using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class ObjectForSearch
    {
        public string name;
        public GameObject gameObject;
    }

    public List<ObjectForSearch> objectsForSearch;
    public Slider progressBar;
    public Text takeObject;
    public Text putObject;
    public GameObject winPanel;

    int startCount = 0;

    GameObject takenGameObject;

    public void ShowTipToTakeObject()
    {
        takeObject.gameObject.SetActive(true);
    }

    public void HideTipToTakeObject()
    {
        takeObject.gameObject.SetActive(false);
        takenGameObject = null;
    }

    public void SetCurrentObject(GameObject gameObject)
    {
        takenGameObject = gameObject;
    }

    public void PutObject(Vector3 newObjectPos)
    {
        if(takenGameObject)
        {
            takenGameObject.SetActive(true);

            takenGameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            takenGameObject.transform.position = newObjectPos;
            takenGameObject = null;

            putObject.gameObject.SetActive(false);
            takeObject.gameObject.SetActive(true);
        }
    }

    public void TakeObject()
    {
        if(takenGameObject)
        {
            takenGameObject.SetActive(false);
            takeObject.gameObject.SetActive(false);
            putObject.gameObject.SetActive(true);
        }
    }

    public bool IsTakenObject() => takenGameObject != null;

    private void Start()
    {
        startCount = objectsForSearch.Count;
    }

    public void RemoveObject(GameObject gameObject)
    {
        foreach(var obj in objectsForSearch)
            if(obj.gameObject.Equals(gameObject))
            {
                objectsForSearch.Remove(obj);
                break;
            }

        UpdateProgressBar();

        if (objectsForSearch.Count == 0)
            winPanel.SetActive(true);
    }

    void UpdateProgressBar()
    {
        progressBar.value = 1 - (float)objectsForSearch.Count / startCount;
    }
}
