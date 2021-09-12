using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paroxe.PdfRenderer;
using SFB;

public class PDFCustomRenderer : MonoBehaviour
{
    private int p = 0;
    private int pageNum = 0;
    Dictionary<int, Texture2D> pdfPageTextures;

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
            RenderPDF(++p);
        }

        yield return null;
    }

    private void RenderPDF(int p)
    {
        this.GetComponent<MeshRenderer>().material.mainTexture = pdfPageTextures[p];
    }

    private void LoadPDFPages()
    {
        string pdfFileName = StartSceneButtonManager.pdfFileName;
        if (pdfFileName == null)
        {
            pdfFileName = "Assets/MyAssets/PDFs/AkatsukiDevMeetup2021Komai.pdf";
        }

        PDFDocument pdfDocument = new PDFDocument(pdfFileName, "");
        PDFPage page;
        pageNum = pdfDocument.GetPageCount();
        pdfPageTextures = new Dictionary<int, Texture2D>();

        for(int i = 0; i <= pageNum; i++)
        {
            page = new PDFPage(pdfDocument, i);
            pdfPageTextures.Add(i, pdfDocument.Renderer.RenderPageToTexture(page, 1980, 1080));
            if (page != null)
            {
                page.Dispose();
            }
        }
    }
}
