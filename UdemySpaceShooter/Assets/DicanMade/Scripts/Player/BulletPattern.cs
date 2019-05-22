using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    [Header("Pattron bullets")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Vector3 startPoint;
    [SerializeField] int numberOfProjectile;
    private const float radius = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startPoint = transform.position;
            SpawnProjectile(numberOfProjectile);
        }    
    }
    private void SpawnProjectile(int _numberOfProjectile)
    {
        
        /*
        Tambien hay que hace pruebas al mover al objeto de donde se emiten, aparecen extraños resultados
         */
            StartCoroutine(asdasd(_numberOfProjectile));    
        
        

    }
    IEnumerator asdasd(int _numberOfProjectile)
        {
            float angleStep = 360f / _numberOfProjectile;
        float angle = 0f;
        //How many times will calculate the directions
        for (int a = 0; a < 2; a++)
        {
            //Direcion Calculations
            for (int i = 0; i < _numberOfProjectile; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projetileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projetileVector - startPoint).normalized;

            GameObject tmpObj = Instantiate(projectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileDirXPosition, projectileDirYPosition, 0) * 1f;
            
            /*
                +=, to the right
                -=, to the left
             */
            angle += angleStep;
            yield return new WaitForSeconds(.07f);
            
        }    
        }
        
            
        }
}
