using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAZERNODE : MonoBehaviour
{
    public List<Vector3> ValidPositionFIRE = new List<Vector3>();
    public List<Vector3> ValidPositionICE = new List<Vector3>();
    public List<Vector3> ValidPositionNORMAL = new List<Vector3>();
    public List<Vector3> InvalidPosition = new List<Vector3>();

    public Vector2 startXYpos;
    public Vector2 finXYpos;
    public Vector2 LAZERNODEdetail;


    public void Scan()
    {
        StartCoroutine(SCAN());
    }


    public void bucketfull()
    {
        for (int i = 0; i < ValidPositionFIRE.Count; i++)
        {
            transform.parent.gameObject.GetComponent<slimeSpawner>().ValidPositionsFIRE.Add(ValidPositionFIRE[i]);
        }
        for (int i = 0; i < ValidPositionICE.Count; i++)
        {
            transform.parent.gameObject.GetComponent<slimeSpawner>().ValidPositionsICE.Add(ValidPositionICE[i]);
        }
        for (int i = 0; i < ValidPositionNORMAL.Count; i++)
        {
            transform.parent.gameObject.GetComponent<slimeSpawner>().ValidPositionsNORMAL.Add(ValidPositionNORMAL[i]);
        }
        for (int i = 0; i < InvalidPosition.Count; i++)
        {
            transform.parent.gameObject.GetComponent<slimeSpawner>().InvalidPositions.Add(InvalidPosition[i]);
        }

        Destroy(this.gameObject);

    }

    IEnumerator SCAN()
    {

        for (float i = startXYpos.x; i < finXYpos.x; i += LAZERNODEdetail.x)
        {

            RaycastHit hit;

            Vector3[] points = new Vector3[2];
            points[0] = gameObject.transform.position;
            points[1] = new Vector3(gameObject.transform.position.x, -100.0f, gameObject.transform.position.z);


            GetComponent<LineRenderer>().SetPositions(points);
            GetComponent<LineRenderer>().enabled = true;
            if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity))
            {

                if (hit.collider.tag == "FIRE")
                {

                    ValidPositionFIRE.Add(hit.transform.position);
                }
                else if (hit.collider.tag == "ICE")
                {

                    ValidPositionICE.Add(hit.transform.position);
                }
                else if (hit.collider.tag == "NORMAL")
                {

                    ValidPositionNORMAL.Add(hit.transform.position);
                }
                else
                {

                    InvalidPosition.Add(new Vector3(gameObject.transform.localPosition.x, 0.0f, gameObject.transform.localPosition.z));
                }

            }
            else
            {
                InvalidPosition.Add(new Vector3(this.gameObject.transform.position.x, 0.0f, this.gameObject.transform.position.z));
            }
            //GetComponent<LineRenderer>().enabled = false;

            yield return new WaitForSeconds(0.01f);

            transform.localPosition += new Vector3(LAZERNODEdetail.x, 0.0f, 0.0f);


        }

        bucketfull();

        yield return null;



    }
}
