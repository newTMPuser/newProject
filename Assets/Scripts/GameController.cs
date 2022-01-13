using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public List<ObjectForSearch> objectsForSearch;
    public Slider progressBar;
    public Text takeObject;
    public Text putObject;
    public GameObject winPanel;

    public string taskFormat = "Find {0}";
    public Text taskText;

    int startCount = 0;

    GameObject takenGameObject;

    private void Start()
    {
        var rnd = new System.Random();
        objectsForSearch = objectsForSearch.OrderBy(item => rnd.Next()).ToList();

        taskText.text = string.Format(taskFormat, objectsForSearch[0].name);

        startCount = objectsForSearch.Count;
    }

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

            taskText.text = string.Format(taskFormat, objectsForSearch[0].name);
        }
    }

    public void TakeObject()
    {
        if(takenGameObject)
        {
            takenGameObject.SetActive(false);
            takeObject.gameObject.SetActive(false);
            putObject.gameObject.SetActive(true);

            if(takenGameObject.GetComponent<ObjectForSearch>().name == objectsForSearch[0].gameObject.GetComponent<ObjectForSearch>().name)
            {
                taskText.text = "Put object in box";
            }
        }
    }

    public bool IsTakenObject() => takenGameObject != null;

    public void RemoveObject(GameObject gameObject)
    {
        gameObject = gameObject.transform.parent.gameObject;

        if (objectsForSearch[0]._name == gameObject.GetComponent<ObjectForSearch>()._name)
            objectsForSearch.RemoveAt(0);
        else
            return;

        UpdateProgressBar();

        if (objectsForSearch.Count == 0)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
            return;
        }

        taskText.text = string.Format(taskFormat, objectsForSearch[0].name);
    }

    void UpdateProgressBar()
    {
        progressBar.value = 1 - (float)objectsForSearch.Count / startCount;
    }
}
