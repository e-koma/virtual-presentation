using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paroxe.PdfRenderer;
using SFB;

public class PDFCustomRenderer : MonoBehaviour
{
    private PDFDocument pdfDocument;
    private PDFPage page;
    private int p = 0;
    private Texture2D tex;
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
        GetComponent<MeshRenderer>().material.mainTexture = pdfPageTextures[p];
    }

    private void LoadPDFPages()
    {
        string pdfFileName = LoadPDFFile(); // for debug: "Assets/MyAssets/PDFs/2021_happybirthday.pdf"
        pdfDocument = new PDFDocument(pdfFileName, "");
        pageNum = pdfDocument.GetPageCount();
        pdfPageTextures = new Dictionary<int, Texture2D>();

        for(int i = 0; i <= pageNum; i++)
        {
            page = new PDFPage(pdfDocument, i);
            pdfPageTextures.Add(i, pdfDocument.Renderer.RenderPageToTexture(page, 960, 540));
            if (page != null)
            {
                page.Dispose();
            }
        }
    }

    private string LoadPDFFile()
    {
        ExtensionFilter[] extensions = new[] {
            new ExtensionFilter("PDF File", "pdf"),
        };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        return paths[0];
    }
}
