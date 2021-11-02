using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public Camera camera;
    private float height;
    private float width;
    public Tweener tweener;
    private float timer;
    public GameObject cherry;
    private GameObject bonusCherry;
    // Start is called before the first frame update
    void Start()
    {
        height = camera.orthographicSize + 1;
        width = height * Screen.width / Screen.height + 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            SpawnCherry((int)Random.Range(1, 5));
            timer = 0;
        }
    }

    private void SpawnCherry(int side)
    {
        float randomValue;
        switch (side)
        {
            case 1:
                randomValue = Random.Range(width * (-1), width);
                bonusCherry = Instantiate(cherry, new Vector3(randomValue, height, 0), Quaternion.identity);
                tweener.AddTween(bonusCherry.transform, (Vector2)bonusCherry.transform.position, new Vector2(randomValue * (-1), height * (-1)), 7f, true);
                break;
            case 2:
                randomValue = Random.Range(width * (-1), width);
                bonusCherry = Instantiate(cherry, new Vector3(randomValue, height*(-1f), 0), Quaternion.identity);
                tweener.AddTween(bonusCherry.transform, (Vector2)bonusCherry.transform.position, new Vector2(randomValue * (-1), height), 7f, true);
                break;
            case 3:
                randomValue = Random.Range(height * (-1f), height);
                bonusCherry = Instantiate(cherry, new Vector3(width, randomValue, 0), Quaternion.identity);
                tweener.AddTween(bonusCherry.transform, (Vector2)bonusCherry.transform.position, new Vector2(width * (-1), randomValue * (-1)), 7f, true);
                break;
            case 4:
                randomValue = Random.Range(height * (-1f), height);
                bonusCherry = Instantiate(cherry, new Vector3(width * (-1f), randomValue, 0), Quaternion.identity);
                tweener.AddTween(bonusCherry.transform, (Vector2)bonusCherry.transform.position, new Vector2(width, randomValue * (-1)), 7f, true);
                break;
        }
    }
}
