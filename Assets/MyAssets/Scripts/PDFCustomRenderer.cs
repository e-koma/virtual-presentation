using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paroxe.PdfRenderer;


public class PDFCustomRenderer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PDFDocument pdfDocument = new PDFDocument("Assets/MyAssets/PDFs/game_serverside.pdf", "");
        PDFPage page = new PDFPage(pdfDocument, 0);
        Texture2D tex = pdfDocument.Renderer.RenderPageToTexture(page, 960, 540);
        GetComponent<MeshRenderer>().material.mainTexture = tex;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
