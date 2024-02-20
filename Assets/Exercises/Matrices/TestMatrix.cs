using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMatrix : MonoBehaviour
{
    private HMatrix2D mat = new HMatrix2D();

    void Question2()
    {
        HMatrix2D mat1 = new HMatrix2D(3,3,3,3,3,3,3,3,3);
        HMatrix2D mat2 = new HMatrix2D ();
        mat2.setIdentity();
        HMatrix2D resultMat;
        HVector2D resultVec;
        HVector2D vec1 = new HVector2D(5, 0);

        resultMat = mat1 * mat2;
        resultMat.Print();
    }
    // Start is called before the first frame update
    void Start()
    {
        mat.setIdentity();
        mat.Print();
        Question2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
