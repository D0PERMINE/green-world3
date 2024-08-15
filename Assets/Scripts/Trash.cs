using UnityEngine;
using TMPro;

public class Trash : MonoBehaviour
{
    public TrashType trash;
    public string trashType;
    [SerializeField] GameObject title;

    private void Start()
    {
        title.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            title.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            title.SetActive(false);
        }
    }

}
