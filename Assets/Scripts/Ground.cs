using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteShapeController shapeController;
    public float offset = 0;
    [SerializeField]
    private GameObject flag;

    void Start()
    {
        shapeController = GetComponent<SpriteShapeController>();
        
        shapeController.spline.SetPosition(3, AddOffset(3));
        shapeController.spline.SetPosition(4, AddOffset(4));

        flag.transform.position = GetPosition(3) + gameObject.transform.position;
    }

    private Vector3 AddOffset(int index)
    {
        Vector3 pos = GetPosition(index);
        return new Vector3(pos.x, pos.y + offset, 0);
    }

    private Vector3 GetPosition(int index)
    {
        return shapeController.spline.GetPosition(index);
    }

}
