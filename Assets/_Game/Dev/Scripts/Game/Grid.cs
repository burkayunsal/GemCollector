using UnityEngine;


public class Grid : MonoBehaviour
{
    private int i, j;
    public int id;

    public void Init(int i, int rowCount,int j, int columnCount)
    {
        name = "(" + i + " , " + j + ")";
        this.i = i;
        this.j = j;
        Position(rowCount, columnCount);
    }
   
    void Position(int rowCount, int colCount)
    {
        float x = -i + (rowCount - 1) / 2f;
        float z = j + (colCount - 1) / -2f;
        transform.localPosition = new Vector3(z * transform.localScale.z * 1.5f, 0, x * transform.localScale.x * 1.5f); 
    }

    public void SpawnGem()
    {
        GemManager.I.StartGemGrowth(this);
    }
}