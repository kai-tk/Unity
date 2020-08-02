namespace OpenCvSharp
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CVCamController : MonoBehaviour
    {
        int width = 1280;
        int height = 960;
        int fps = 30;
        WebCamTexture webcamTexture;
        Texture2D texture;
        Color32[] colors = null;

        public byte range = 10;
        public byte bws = 25;
        public byte blackv = 75;
        public byte whitev = 210;
        bool flag = false;

        Mat mat;
        Mat hsvMat;
        Mat changedHsvMat;
        Mat changedMat;
        Vec3b gbgr;
        Vec3b ghsv;
        Vec3b shsv;
        Color gc;
        Color sc;

        public GameObject get;
        public GameObject save;
        Texture2D originTex;
        Texture2D changedTex;
        bool whiteflag;
        bool blackflag;

        public bool debugmode=false;

        // Start is called before the first frame update
        void Start()
        {
            webcamTexture = new WebCamTexture(width, height, fps);
            webcamTexture.Play();
            StartCoroutine(CVCor());
            mat = new Mat();
            hsvMat = new Mat();
            changedHsvMat = new Mat();
            changedMat = new Mat();
        }

        IEnumerator CVCor()
        {
            while (true)
            {
                if (webcamTexture.width > 16 && webcamTexture.height > 16 && colors == null)
                {
                    width = webcamTexture.width;
                    height = webcamTexture.height;
                    flag = true;
                    colors = new Color32[webcamTexture.width * webcamTexture.height];
                    texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGBA32, false);
                    if (debugmode)
                    {
                        texture = ToTexture2D(GetComponent<RawImage>().texture);
                    }
                    break;
                }
                yield return null;
            }
        }

        Texture2D ToTexture2D(Texture tex)
        {
            var sw = tex.width;
            var sh = tex.height;
            var format = TextureFormat.RGBA32;
            var result = new Texture2D(sw, sh, format, false);
            var currentRT = RenderTexture.active;
            var rt = new RenderTexture(sw, sh, 32);
            Graphics.Blit(tex, rt);
            RenderTexture.active = rt;
            UnityEngine.Rect source = new UnityEngine.Rect(0, 0, rt.width, rt.height);
            result.ReadPixels(source, 0, 0);
            result.Apply();
            RenderTexture.active = currentRT;
            return result;
        }

        // Update is called once per frame
        void Update()
        {
            if (colors != null && webcamTexture.isPlaying)
            {
                webcamTexture.GetPixels32(colors);
                if (!debugmode)
                {
                    texture.SetPixels32(colors);
                }
                mat = Unity.TextureToMat(texture);
                Cv2.CvtColor(mat, hsvMat, ColorConversionCodes.BGR2HSV);
                changedTex = Unity.MatToTexture(mat);
                GetComponent<RawImage>().texture = changedTex;
            }
        }

        public void GetColor(Vector2Int pos)
        {
            gbgr = mat.At<Vec3b>(pos.y, pos.x);
            ghsv = hsvMat.At<Vec3b>(pos.y, pos.x);
            Debug.Log("(" + ghsv[0] + " " + ghsv[1] + " " + ghsv[2] + ")");
            gc = new Color((float)gbgr[2] / 255, (float)gbgr[1] / 255, (float)gbgr[0] / 255, 1.0f);
            get.GetComponent<RawImage>().color = gc;
        }
        
        public void SaveColor()
        {
            sc = new Color((float)gbgr[2] / 255, (float)gbgr[1] / 255, (float)gbgr[0] / 255, 1.0f);
            shsv = new Vec3b(ghsv[0], ghsv[1], ghsv[2]);
            save.GetComponent<RawImage>().color = sc;
        }

        public bool Getflag()
        {
            return flag;
        }

        public void changeColor()
        {
            whiteflag = false;
            blackflag = false;
            if (ghsv[1]<bws && ghsv[2] > whitev) whiteflag = true;
            if (ghsv[2] < blackv) blackflag = true;

            changedHsvMat = hsvMat.Clone();

            //Vector3 psv = new Vector3(0, (float)shsv[1] / (ghsv[1] + 1), (float)shsv[2] / (ghsv[2] + 1));
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Vec3b hsv = hsvMat.At<Vec3b>(i, j);
                    Vec3b bgr = mat.At<Vec3b>(i, j);

                    if ((whiteflag && hsv[1]<bws) || blackflag)
                    {
                        if (System.Math.Sqrt(System.Math.Pow((gbgr[0] - bgr[0]) * 0.11, 2) + System.Math.Pow((gbgr[1] - bgr[1]) * 0.59, 2) + System.Math.Pow((gbgr[2] - bgr[2]) * 0.30, 2)) < range*3)
                        {
                            changedHsvMat.Set(i, j, new Vec3b(shsv[0], (byte)((hsv[1] + shsv[1] * 2) / 3), (byte)((hsv[2] + shsv[2] * 2) / 3)));
                        }
                    }
                    else if(!whiteflag)
                    {
                        if ((System.Math.Abs(ghsv[0] - hsv[0]) < range || System.Math.Abs(ghsv[0] - hsv[0]) > 180 - range) /*&& System.Math.Abs(ghsv[1] - hsv[1]) < range*/ && !(hsv[1] < bws && hsv[2] > whitev) && hsv[2]>blackv)
                        {
                            changedHsvMat.Set(i, j, new Vec3b(shsv[0], (byte)((hsv[1] + shsv[1] * 2) / 3), (byte)((hsv[2] + shsv[2] * 2) / 3)));
                        }
                    }
                }
            }
            Cv2.CvtColor(changedHsvMat, changedMat, ColorConversionCodes.HSV2BGR);
            changedTex = Unity.MatToTexture(changedMat);
            GetComponent<RawImage>().texture = changedTex;
        }

        public void cancelChangeColor()
        {
            GetComponent<RawImage>().texture = originTex;
        }

        public void pauseCam()
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Pause();
                mat = Unity.TextureToMat(texture);
                Cv2.CvtColor(mat, hsvMat, ColorConversionCodes.BGR2HSV);
                originTex = Unity.MatToTexture(mat);
            }
            else
            {
                webcamTexture.Play();
            }
        }
    }
}