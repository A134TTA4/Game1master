using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    namespace BluePrint
    {
        public class DrawBluePrint : MonoBehaviour
        {
            Texture2D drawTexture;
            Color[] buffer;
            static private int DrawMode = 1;
            private Color DrawColor1 = Color.blue;
            private Color DrawColor2 = Color.red;
            private Color EraseColor = new Color(1f, 1f, 1f, 1f);
            private Vector2 priviousPoint;
            private Vector2 nowPoint;
            private bool Drawing = false;
            private bool Getprivious = false;
            void Start()
            {
                Texture2D mainTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;
                Color[] pixels = mainTexture.GetPixels();

                buffer = new Color[pixels.Length];
                pixels.CopyTo(buffer, 0);

                for (int x = 0; x < mainTexture.width; x++)
                {
                    for (int y = 0; y < mainTexture.height ; y++)
                    {
                        //buffer.SetValue(EraseColor, x + 256 * y);
                    }
                }
                
                drawTexture = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
                drawTexture.filterMode = FilterMode.Point;
                drawTexture.SetPixels(buffer);
                DrawMode = 1;
            }

            void Update()
            {
                if(TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == false)
                {
                    return;
                }
                
                drawTexture.Apply();
                GetComponent<Renderer>().material.mainTexture = drawTexture;

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    DrawMode = 1;
                }
                if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    DrawMode = 2;
                }
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    Drawing = true;
                    GetPosition();
                    if(DrawMode == 1)
                    {
                        DrawPrint();
                    }
                    else
                    {
                        Erase();
                    }
                    priviousPoint = nowPoint;
                    Getprivious = true;
                }
                if(Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Drawing = false;
                    Getprivious = false;
                }

            }

            private void DrawPrint()
            {
                if((nowPoint.x - priviousPoint.x) * (nowPoint.x - priviousPoint.x) + (nowPoint.y - priviousPoint.y) * (nowPoint.y - priviousPoint.y) > 4000)
                {
                    return;
                }

                if (Getprivious == true)
                {
                    if ((nowPoint.x - priviousPoint.x) * (nowPoint.x - priviousPoint.x) > (nowPoint.y - priviousPoint.y) * (nowPoint.y - priviousPoint.y))//Xの距離のほうがYの距離より大きい
                    {
                        if (nowPoint.x > priviousPoint.x)
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n+= 0.5f)
                                    {
                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) <  256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + (nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) +1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n )) +1, DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n )) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n += 0.5f)
                                    {
                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + (nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) + 1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1, DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n+=0.5f)
                                    {
                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) + 1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x)) * n) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1, DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n += 0.5f)
                                    {
                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) + 1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x)) * n) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1, DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(nowPoint.y >priviousPoint.y)
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n+=0.5f)
                                    {
                                        if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        { 
                                            drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - n) + 1 > 0 && (int)(priviousPoint.x - n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - n) - 1 > 0 && (int)(priviousPoint.x - n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1, DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n += 0.5f)
                                    {
                                        if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x - n) + 1 > 0 && (int)(priviousPoint.x - n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x - n) - 1 > 0 && (int)(priviousPoint.x - n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1, DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n+=0.5f)
                                    {
                                        if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + n) + 1 > 0 && (int)(nowPoint.x + n) + 1 < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n) + 1, (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + n) -1 > 0 && (int)(nowPoint.x + n) -1 < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n) -1, (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1, DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) -1 > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n))  -1< 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n += 0.5f)
                                    {
                                        if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + n) + 1 > 0 && (int)(nowPoint.x + n) + 1 < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n) + 1, (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + n) - 1 > 0 && (int)(nowPoint.x + n) - 1 < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n) - 1, (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1, DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (nowPoint.x > priviousPoint.x)
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(nowPoint.y - priviousPoint.y); n+=0.5f)
                                    {
                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) +1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) +1, (int)(priviousPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) -1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) -1, (int)(priviousPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) +1 > 0 && (int)(priviousPoint.y + n) +1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) + 1, DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) - 1 > 0 && (int)(priviousPoint.y + n) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(nowPoint.y - priviousPoint.y); n += 0.5f)
                                    {
                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1, (int)(priviousPoint.y + n), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1, (int)(priviousPoint.y + n), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) + 1 > 0 && (int)(priviousPoint.y + n) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) + 1, DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) - 1 > 0 && (int)(priviousPoint.y + n) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n+=0.5f)
                                    {
                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) +1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) +1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) +1, (int)(priviousPoint.y - n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) +1 > 0 && (int)(nowPoint.y + n) +1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) +1, DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n += 0.5f)
                                    {
                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n), DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) + 1 > 0 && (int)(nowPoint.y + n) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) + 1, DrawColor2);
                                        }

                                        if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(nowPoint.y - priviousPoint.y); n+=0.5f)
                                    {
                                        if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 +1 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) + 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) +1, (int)(priviousPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 - 1 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) - 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) -1, (int)(priviousPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) + 1 > 0  && (int)(priviousPoint.y + n) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) + 1, DrawColor1);
                                        }

                                        if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) - 1 > 0 && (int)(priviousPoint.y + n) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(nowPoint.y - priviousPoint.y); n+=0.5f)
                                    {
                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(nowPoint.y - n) > 0 && (int)(nowPoint.y - n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(nowPoint.y - n), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 + 1 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) + 1 < 256 && (int)(nowPoint.y - n) > 0 && (int)(nowPoint.y - n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) + 1, (int)(nowPoint.y - n), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 - 1 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) - 1 < 256 && (int)(nowPoint.y - n) > 0 && (int)(nowPoint.y - n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) - 1, (int)(nowPoint.y - n), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(nowPoint.y - n) + 1 > 0 && (int)(nowPoint.y - n) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(nowPoint.y - n) + 1, DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(nowPoint.y - n) - 1 > 0 && (int)(nowPoint.y - n) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(nowPoint.y - n) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                                {
                                    for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n+=0.5f)
                                    {
                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1, (int)(nowPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1, (int)(nowPoint.y + n), DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) +1 > 0 && (int)(nowPoint.y + n) +1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) + 1, DrawColor1);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) - 1, DrawColor1);
                                        }
                                    }
                                }
                                else
                                {
                                    for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n += 0.5f)
                                    {
                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1, (int)(nowPoint.y + n), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1, (int)(nowPoint.y + n), DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) + 1 > 0 && (int)(nowPoint.y + n) + 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) + 1, DrawColor2);
                                        }

                                        if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                        {
                                            drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) - 1, DrawColor2);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }    

            }

            private void GetPosition()
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    nowPoint = hit.textureCoord * 256;
                    if (DrawMode == 1)
                    {
                        if (PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                        {
                            if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y), DrawColor1);
                            }
                            if (nowPoint.x + 1 > 0 && nowPoint.x + 1 < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x) + 1, (int)(nowPoint.y), DrawColor1);
                            }

                            if (nowPoint.x - 1 > 0 && nowPoint.x - 1 < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x) - 1, (int)(nowPoint.y), DrawColor1);
                            }

                            if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y + 1 > 0 && nowPoint.y + 1 < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y) + 1, DrawColor1);
                            }

                            if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y - 1 > 0 && nowPoint.y - 1 < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y) - 1, DrawColor1);
                            }
                        }
                        else
                        {
                            if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y), DrawColor2);
                            }
                            if (nowPoint.x + 1 > 0 && nowPoint.x + 1 < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x) + 1, (int)(nowPoint.y), DrawColor2);
                            }

                            if (nowPoint.x - 1 > 0 && nowPoint.x - 1 < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x) - 1, (int)(nowPoint.y), DrawColor2);
                            }

                            if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y + 1 > 0 && nowPoint.y + 1 < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y) + 1, DrawColor2);
                            }

                            if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y - 1 > 0 && nowPoint.y - 1 < 256)
                            {
                                drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y) - 1, DrawColor2);
                            }
                        }
                    }
                    else
                    {
                        if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                        {
                            drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y), EraseColor);
                        }
                        if (nowPoint.x + 1 > 0 && nowPoint.x + 1 < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                        {
                            drawTexture.SetPixel((int)(nowPoint.x) + 1, (int)(nowPoint.y), EraseColor);
                        }

                        if (nowPoint.x - 1 > 0 && nowPoint.x - 1 < 256 && nowPoint.y > 0 && nowPoint.y < 256)
                        {
                            drawTexture.SetPixel((int)(nowPoint.x) - 1, (int)(nowPoint.y), EraseColor);
                        }

                        if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y + 1 > 0 && nowPoint.y + 1 < 256)
                        {
                            drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y) + 1, EraseColor);
                        }

                        if (nowPoint.x > 0 && nowPoint.x < 256 && nowPoint.y - 1 > 0 && nowPoint.y - 1 < 256)
                        {
                            drawTexture.SetPixel((int)(nowPoint.x), (int)(nowPoint.y) - 1, EraseColor);
                        }
                    }
                }
                
                //Debug.Log("nowpoint:" + nowPoint);
                //Debug.Log("priviouspoint:" + priviousPoint);
            } 

            private void Erase()
            {
                if ((nowPoint.x - priviousPoint.x) * (nowPoint.x - priviousPoint.x) + (nowPoint.y - priviousPoint.y) * (nowPoint.y - priviousPoint.y) > 4000)
                {
                    return;
                }

                if (Getprivious == true)
                {
                    if ((nowPoint.x - priviousPoint.x) * (nowPoint.x - priviousPoint.x) > (nowPoint.y - priviousPoint.y) * (nowPoint.y - priviousPoint.y))//Xの距離のほうがYの距離より大きい
                    {
                        if (nowPoint.x > priviousPoint.x)
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                
                                for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n += 0.5f)
                                {
                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + (nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) + 1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1, EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                            else
                            {
                                
                                for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n += 0.5f)
                                {
                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) + 1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x)) * n) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1, EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                        }
                        else
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n += 0.5f)
                                {
                                    if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - n) + 1 > 0 && (int)(priviousPoint.x - n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - n) - 1 > 0 && (int)(priviousPoint.x - n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1, EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                            else
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n += 0.5f)
                                {
                                    if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(nowPoint.x + n) + 1 > 0 && (int)(nowPoint.x + n) + 1 < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + n) + 1, (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(nowPoint.x + n) - 1 > 0 && (int)(nowPoint.x + n) - 1 < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + n) - 1, (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)), EraseColor);
                                    }

                                    if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1, EraseColor);
                                    }

                                    if ((int)(nowPoint.x + n) > 0 && (int)(nowPoint.x + n) < 256 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 > 0 && (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + n), (int)(nowPoint.y + ((priviousPoint.y - nowPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                        }
                    }
                    else
                    {
                        if (nowPoint.x > priviousPoint.x)
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                
                                for (float n = 0; n < (int)(nowPoint.y - priviousPoint.y); n += 0.5f)
                                {
                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1, (int)(priviousPoint.y + n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1, (int)(priviousPoint.y + n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) + 1 > 0 && (int)(priviousPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) + 1, EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) - 1 > 0 && (int)(priviousPoint.y + n) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                            else
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n += 0.5f)
                                {
                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) + 1 > 0 && (int)(nowPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) + 1, EraseColor);
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                        }
                        else
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                
                                for (float n = 0; n < (int)(nowPoint.y - priviousPoint.y); n += 0.5f)
                                {
                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - +((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 + 1 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) + 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) + 1, (int)(priviousPoint.y + n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 - 1 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) - 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) - 1, (int)(priviousPoint.y + n), EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) + 1 > 0 && (int)(priviousPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) + 1, EraseColor);
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) - 1 > 0 && (int)(priviousPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                            else
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n += 0.5f)
                                {
                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n), EraseColor);
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1, (int)(nowPoint.y + n), EraseColor);
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1, (int)(nowPoint.y + n), EraseColor);
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) + 1 > 0 && (int)(nowPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) + 1, EraseColor);
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) - 1, EraseColor);
                                    }
                                }
                                
                                
                            }
                        }
                    }
                }

                priviousPoint = nowPoint;
                Getprivious = true;
            }

            static public int InformDrawMode()
            {
                return DrawMode;
            }


        }
    }
}
