using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeImage : MonoBehaviour
{
    [SerializeField] private Image unitImage;
    [SerializeField] private Image unitCircle;
    [SerializeField] private float duration = 0;
    private float current = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(duration == -1)
        {
            return;
        }
        current += Time.deltaTime;
        unitCircle.fillAmount = (current / duration);
        if(current >= duration)
        {
            Destroy(gameObject);
        }
    }

    public void NodeSetUp(Sprite img, float dur)
    {
        unitImage.sprite = img;
        duration = dur;
    }
}
