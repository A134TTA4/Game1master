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
            static private int[] inkLeft = new int[4];
            private int[,] mapLinst = new int[256,256];
            static private int PlayerState = 0;
            [SerializeField]
            private GameObject Player1;
            private Vector2 Player1Position;
            [SerializeField]
            private GameObject Player2;
            private Vector2 Player2Position;
            void Start()
            {
                Texture2D mainTexture = (Texture2D)GetComponent<Renderer>().material.mainTexture;
                Color[] pixels = mainTexture.GetPixels();

                inkLeft[1] = 1000;
                inkLeft[2] = 1000;
                inkLeft[3] = 1000;

                buffer = new Color[pixels.Length];
                pixels.CopyTo(buffer, 0);

                for (int x = 0; x < mainTexture.width; x++)
                {
                    for (int y = 0; y < mainTexture.height ; y++)
                    {
                        mapLinst[x, y] = 0;
                    }
                }
                
                
                drawTexture = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
                drawTexture.filterMode = FilterMode.Point;
                drawTexture.SetPixels(buffer);
                DrawMode = 1;
            }

            void Update()
            {
                GetPlayerPosition();
                PlayerState = 0;
                int Px = 0;
                int Py = 0;

                if(PhotonScriptor.ConnectingScript.informPlayerID() == 1)
                {
                    Px = (int)(Player1Position.x);
                    Py = (int)(Player1Position.y);
                }
                if (PhotonScriptor.ConnectingScript.informPlayerID() == 2)
                {
                    Px = (int)(Player2Position.x);
                    Py = (int)(Player2Position.y);
                }
                for (int x = -4; x < 4; x++)
                {
                    for (int y = -4; y < 4; y++)
                    {
                        if((Px + x) > 0 && (Py + y) > 0 && (Px + x) < 256 && (Py + y) < 256)
                        {
                            if (mapLinst[Px+x,Py+y] != 0)
                            {
                                PlayerState = mapLinst[Px + x, Py + y];
                                //Debug.Log(PlayerState);
                            }
                        }
                        
                    }
                }

                if(TimeManager.PreParationTime.InformPreparationState() == true)
                {
                    PlayerState = 0;
                }
                
                if (TimeManager.BluePrint.BruePrintPhaze.InformBluePrintState() == false)
                {
                    return;
                }

                drawTexture.Apply();
                GetComponent<Renderer>().material.mainTexture = drawTexture;
                if(DrawMode == 1 && inkLeft[1] < 0)
                {
                    DrawMode = 0;
                }
                if (DrawMode == 2 && inkLeft[2] < 0)
                {
                    DrawMode = 0;
                }
                if (DrawMode == 3 && inkLeft[3] < 0)
                {
                    DrawMode = 0;
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    DrawMode = 1;
                    DrawColor1 = Color.blue;
                }
                if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    DrawMode = 2;
                    DrawColor1 = Color.yellow;
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    DrawMode = 3;
                    DrawColor1 = Color.green;
                }

                if (Input.GetKey(KeyCode.Mouse0) && DrawMode != 0)
                {
                    Drawing = true;
                    GetPosition();

                    DrawPrint();
                    
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
                                for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n+= 0.5f)
                                {
                                    inkLeft[DrawMode] -= 5;
                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) <  256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + (nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n), (int)(priviousPoint.y + (nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) +1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n )) +1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n )) - 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1] = DrawMode;
                                    }
                                }
                            }
                            else
                            {
                                for (float n = 0; n < (int)(nowPoint.x - priviousPoint.x); n+=0.5f)
                                {
                                    inkLeft[DrawMode] -= 5;
                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) + 1 > 0 && (int)(priviousPoint.x + n) + 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x)) * n) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n) + 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) - 1 > 0 && (int)(priviousPoint.x + n) - 1 < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n) - 1, (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) + 1] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + n) > 0 && (int)(priviousPoint.x + n) < 256 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) /(nowPoint.x - priviousPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + n), (int)(priviousPoint.y - ((priviousPoint.y - nowPoint.y) / (nowPoint.x - priviousPoint.x) * n)) - 1] = DrawMode;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(nowPoint.y >priviousPoint.y)
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n+=0.5f)
                                {
                                    inkLeft[DrawMode] -= 5;
                                    if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    { 
                                        drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - n) + 1 > 0 && (int)(priviousPoint.x - n) + 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - n) + 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - n) - 1 > 0 && (int)(priviousPoint.x - n) - 1 < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - n) - 1, (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n))] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) + 1] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - n) > 0 && (int)(priviousPoint.x - n) < 256 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 > 0 && (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - n), (int)(priviousPoint.y + ((nowPoint.y - priviousPoint.y) / (priviousPoint.x - nowPoint.x) * n)) - 1] = DrawMode;
                                    }
                                } 
                            }
                            else
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.x - nowPoint.x); n+=0.5f)
                                {
                                    inkLeft[DrawMode] -= 5;
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
                                    inkLeft[DrawMode] -= 5;
                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1, (int)(priviousPoint.y + n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) + 1, (int)(priviousPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1, (int)(priviousPoint.y + n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) - 1, (int)(priviousPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) + 1 > 0 && (int)(priviousPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) + 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) + 1] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))) < 256 && (int)(priviousPoint.y + n) - 1 > 0 && (int)(priviousPoint.y + n) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) - 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (nowPoint.y - priviousPoint.y))), (int)(priviousPoint.y + n) - 1] = DrawMode;
                                    }
                                }
                            }
                            else
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n+=0.5f)
                                {
                                    inkLeft[DrawMode] -= 5;
                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) +1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) +1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) +1, (int)(priviousPoint.y - n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1 < 256 && (int)(priviousPoint.y - n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) + 1, (int)(priviousPoint.y - n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) +1 > 0 && (int)(nowPoint.y + n) +1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) +1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) + 1] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) > 0 && (int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))) < 256 && (int)(priviousPoint.y - n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) - 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x + ((nowPoint.x - priviousPoint.x) * n / (priviousPoint.y - nowPoint.y))), (int)(priviousPoint.y - n) - 1] = DrawMode;
                                    }
                                }
                                
                            }
                        }
                        else
                        {
                            if (nowPoint.y > priviousPoint.y)
                            {
                                
                                for (float n = 0; n < (int)(nowPoint.y - priviousPoint.y); n+=0.5f)
                                {
                                    inkLeft[DrawMode] -= 5;
                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - + ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 +1 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) + 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) +1, (int)(priviousPoint.y + n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) + 1, (int)(priviousPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 - 1 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) - 1 < 256 && (int)(priviousPoint.y + n) > 0 && (int)(priviousPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) -1, (int)(priviousPoint.y + n), DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) - 1, (int)(priviousPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) + 1 > 0  && (int)(priviousPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) + 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) + 1] = DrawMode;
                                    }

                                    if ((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) > 0 && (int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)) < 256 && (int)(priviousPoint.y + n) - 1 > 0 && (int)(priviousPoint.y + n) + 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) - 1, DrawColor1);
                                        mapLinst[(int)(priviousPoint.x - ((priviousPoint.x - nowPoint.x) / (nowPoint.y - priviousPoint.y) * n)), (int)(priviousPoint.y + n) - 1] = DrawMode;
                                    }
                                }
                                
                                
                            }
                            else
                            {
                                
                                for (float n = 0; n < (int)(priviousPoint.y - nowPoint.y); n+=0.5f)
                                {
                                    inkLeft[DrawMode] -= 5;
                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n), DrawColor1);
                                        mapLinst[(int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1, (int)(nowPoint.y + n), DrawColor1);
                                        mapLinst[(int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) + 1, (int)(nowPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1 < 256 && (int)(nowPoint.y + n) > 0 && (int)(nowPoint.y + n) < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1, (int)(nowPoint.y + n), DrawColor1);
                                        mapLinst[(int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) - 1, (int)(nowPoint.y + n)] = DrawMode;
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) +1 > 0 && (int)(nowPoint.y + n) +1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) + 1, DrawColor1);
                                        mapLinst[(int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) + 1] = DrawMode;
                                    }

                                    if ((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) > 0 && (int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)) < 256 && (int)(nowPoint.y + n) - 1 > 0 && (int)(nowPoint.y + n) - 1 < 256)
                                    {
                                        drawTexture.SetPixel((int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) - 1, DrawColor1);
                                        mapLinst[(int)(nowPoint.x + ((priviousPoint.x - nowPoint.x) / (priviousPoint.y - nowPoint.y) * n)), (int)(nowPoint.y + n) - 1] = DrawMode;
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

            private void GetPlayerPosition()
            {
                Player1Position = new Vector2((int)((Player1.transform.position.x+20)/40*256) , (int)((Player1.transform.position.z+20)/40*256));
                Player2Position = new Vector2((int)((Player2.transform.position.x+20)/40*256) , (int)((Player2.transform.position.z+20)/40*256));
            }

            static public int InformDrawMode()
            {
                return DrawMode;
            }

            static public int InformPlayerState()
            {
                return PlayerState;
            }

            static public int InformBlueLeft()
            {
                return inkLeft[1];
            }

            static public int InfromYellowLeft()
            {
                return inkLeft[2];
            }

            static public int InformGreenLeft()
            {
                return inkLeft[3];
            }

        }
    }
}
