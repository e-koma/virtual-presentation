using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paroxe.PdfRenderer;

public class PDFCustomRenderer : MonoBehaviour
{
    private PDFDocument pdfDocument;
    private PDFPage page;
    private int p = 0;
    private Texture2D tex;
    private int pageNum = 0;

    void Start()
    {
        pdfDocument = new PDFDocument("Assets/MyAssets/PDFs/game_serverside.pdf", "");
        pageNum = pdfDocument.GetPageCount();
    }

    void LateUpdate()
    {
        StartCoroutine("PressTheButton");
    }

    private IEnumerator PressTheButton()
    {
        if (Input.GetKeyUp("left"))
        {
            p = Mathf.Max(--p, 0);
        }
        else if (Input.GetKeyUp("right") && pageNum > p)
        {
            p++;
        }
        RenderPDF(p);

        yield return null;
    }

    private void RenderPDF(int p)
    {
        page = new PDFPage(pdfDocument, p);
        tex = pdfDocument.Renderer.RenderPageToTexture(page, 960, 540);
        GetComponent<MeshRenderer>().material.mainTexture = tex;
    }
}
