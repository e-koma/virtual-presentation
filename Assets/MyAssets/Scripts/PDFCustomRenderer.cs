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
    Dictionary<int, PDFPage> pdfPages;

    void Start()
    {
        LoadPDFPages();
        RenderPDF(p);
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
            RenderPDF(p);
        }
        else if (Input.GetKeyUp("right") && pageNum > p)
        {
            p++;
            RenderPDF(p);
        }

        yield return null;
    }

    private void RenderPDF(int p)
    {
        page = pdfPages[p];
        tex = pdfDocument.Renderer.RenderPageToTexture(page, 960, 540);
        GetComponent<MeshRenderer>().material.mainTexture = tex;
    }

    private void LoadPDFPages()
    {
        pdfDocument = new PDFDocument("Assets/MyAssets/PDFs/2021_happybirthday.pdf", "");
        pageNum = pdfDocument.GetPageCount();
        pdfPages = new Dictionary<int, PDFPage>();

        for(int i = 0; i < pageNum; i++)
        {
            pdfPages.Add(i, new PDFPage(pdfDocument, i));
        }
    }
}
