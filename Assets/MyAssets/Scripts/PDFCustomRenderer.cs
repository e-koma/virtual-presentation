using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
        if (isInputLeft())
        {
            p = Mathf.Max(--p, 0);
            RenderPDF(p);
        }
        else if (isInputRight() && pageNum > p)
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
            pdfFileName = "Assets/MyAssets/PDFs/current_presen.pdf";
        }

        PDFDocument pdfDocument = new PDFDocument(pdfFileName, "");
        PDFPage page;
        pageNum = pdfDocument.GetPageCount();
        pdfPageTextures = new Dictionary<int, Texture2D>();

        for(int i = 0; i <= pageNum; i++)
        {
            page = new PDFPage(pdfDocument, i);
            pdfPageTextures.Add(i, pdfDocument.Renderer.RenderPageToTexture(page, 3940, 2160));
            if (page != null)
            {
                page.Dispose();
            }
        }
    }

    private bool isInputLeft()
    {
        return Input.GetKeyUp("left") || (Gamepad.current != null && Gamepad.current.dpad.left.wasReleasedThisFrame);
    }

    private bool isInputRight()
    {
        return Input.GetKeyUp("right") || (Gamepad.current != null && Gamepad.current.dpad.right.wasReleasedThisFrame);
    }
}
