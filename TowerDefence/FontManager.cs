using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Font
{
    public static class FontManager
    {

        public static SpriteFont UltimateFont = null;
        public static Texture2D SuperFont;

        public static SpriteFont ScoreFont = null;
        public static Texture2D ScoreF;

        public static SpriteFont StatFont = null;
        public static Texture2D StatF;

        private static List<Rectangle> glyphRect = new List<Rectangle>();
        private static List<Rectangle> croppingList = new List<Rectangle>();
        private static List<char> charList = new List<char>();
        private static List<Vector3> Vector3List = new List<Vector3>();


        public static SpriteFont InitFont(Font font, Texture2D tex)
        {

            int offset = 0;

            switch (font)
            {

                case Font.ScoreFont:

                    ScoreF = tex;

                    #region Font Constructor

                    glyphRect.Add(new Rectangle(0, 0, 5, 8));    //0
                    glyphRect.Add(new Rectangle(6, 0, 5, 8));    //1
                    glyphRect.Add(new Rectangle(12, 0, 5, 8));   //2
                    glyphRect.Add(new Rectangle(18, 0, 5, 8));   //3
                    glyphRect.Add(new Rectangle(24, 0, 5, 8));   //4
                    glyphRect.Add(new Rectangle(30, 0, 5, 8));   //5
                    glyphRect.Add(new Rectangle(36, 0, 5, 8));   //6
                    glyphRect.Add(new Rectangle(42, 0, 5, 8));   //7
                    glyphRect.Add(new Rectangle(48, 0, 5, 8));   //8
                    glyphRect.Add(new Rectangle(54, 0, 5, 8));   //9
                    
                    glyphRect.Add(new Rectangle(0, 0, 5, 8));    //...


                    charList.Add('0');
                    charList.Add('1');
                    charList.Add('2');
                    charList.Add('3');
                    charList.Add('4');
                    charList.Add('5');
                    charList.Add('6');
                    charList.Add('7');
                    charList.Add('8');
                    charList.Add('9');

                    charList.Add('§');

                    int numberCaractere = charList.Count;


                    /// NE CHANGE RIEN
                    for (int i = 0; i < numberCaractere; i++)
                        croppingList.Add(new Rectangle(0, 0, 16, 16));


                    Vector3List.Add(new Vector3(0, -4, 0));//0
                    Vector3List.Add(new Vector3(0, -4, 0));//1
                    Vector3List.Add(new Vector3(0, -4, 0));//2
                    Vector3List.Add(new Vector3(0, -4, 0));//3
                    Vector3List.Add(new Vector3(0, -4, 0));//4
                    Vector3List.Add(new Vector3(0, -4, 0));//5
                    Vector3List.Add(new Vector3(0, -4, 0));//6
                    Vector3List.Add(new Vector3(0, -4, 0));//7
                    Vector3List.Add(new Vector3(0, -4, 0));//8
                    Vector3List.Add(new Vector3(0, -4, 0));//9


                    Vector3List.Add(new Vector3(0, -4, 0));//...
                    /// NE CHANGE RIEN


                    ScoreFont = new SpriteFont(ScoreF, glyphRect, croppingList, charList, 1, 11f, Vector3List, '§');


                    for (int i = 0; i < numberCaractere; i++)
                    {
                        glyphRect.Remove(glyphRect[0]);
                        charList.Remove(charList[0]);
                        croppingList.Remove(croppingList[0]);
                        Vector3List.Remove(Vector3List[0]);
                    }

                    #endregion

                    return ScoreFont;

                case Font.StatFont:

                    StatF = tex;

                    #region Font Constructor

                    glyphRect.Add(new Rectangle(600, 0, 5, 6));     //SPACE
                    glyphRect.Add(new Rectangle(240, 0, 5, 8));     //$
                    glyphRect.Add(new Rectangle(246, 0, 5, 8));     //+
                    glyphRect.Add(new Rectangle(252, 0, 5, 8));     //-
                    glyphRect.Add(new Rectangle(258, 0, 1, 8));     //.

                    offset = 178;

                    glyphRect.Add(new Rectangle(0 + offset, 0, 5, 8));    //0
                    glyphRect.Add(new Rectangle(6 + offset, 0, 5, 8));    //1
                    glyphRect.Add(new Rectangle(12 + offset, 0, 5, 8));   //2
                    glyphRect.Add(new Rectangle(18 + offset, 0, 5, 8));   //3
                    glyphRect.Add(new Rectangle(24 + offset, 0, 5, 8));   //4
                    glyphRect.Add(new Rectangle(30 + offset, 0, 5, 8));   //5
                    glyphRect.Add(new Rectangle(36 + offset, 0, 5, 8));   //6
                    glyphRect.Add(new Rectangle(42 + offset, 0, 5, 8));   //7
                    glyphRect.Add(new Rectangle(48 + offset, 0, 5, 8));   //8
                    glyphRect.Add(new Rectangle(54 + offset, 0, 5, 8));   //9

                    glyphRect.Add(new Rectangle(238, 0, 1, 8));   //:

                    offset = -278;

                    glyphRect.Add(new Rectangle(278 + offset, 0, 6, 8));   //a
                    glyphRect.Add(new Rectangle(285 + offset, 0, 5, 8));   //b
                    glyphRect.Add(new Rectangle(291 + offset, 0, 6, 8));   //c
                    glyphRect.Add(new Rectangle(298 + offset, 0, 5, 8));   //d
                    glyphRect.Add(new Rectangle(304 + offset, 0, 5, 8));   //e
                    glyphRect.Add(new Rectangle(310 + offset, 0, 5, 8));   //f
                    glyphRect.Add(new Rectangle(316 + offset, 0, 6, 8));   //g
                    glyphRect.Add(new Rectangle(323 + offset, 0, 5, 8));   //h
                    glyphRect.Add(new Rectangle(329 + offset, 0, 5, 8));   //i
                    glyphRect.Add(new Rectangle(335 + offset, 0, 5, 8));   //j
                    glyphRect.Add(new Rectangle(341 + offset, 0, 5, 8));   //k
                    glyphRect.Add(new Rectangle(347 + offset, 0, 5, 8));   //l
                    glyphRect.Add(new Rectangle(352 + offset, 0, 7, 8));   //m
                    glyphRect.Add(new Rectangle(360 + offset, 0, 5, 8));   //n
                    glyphRect.Add(new Rectangle(366 + offset, 0, 7, 8));   //o
                    glyphRect.Add(new Rectangle(374 + offset, 0, 5, 8));   //p
                    glyphRect.Add(new Rectangle(380 + offset, 0, 7, 8));   //q
                    glyphRect.Add(new Rectangle(388 + offset, 0, 5, 8));   //r
                    glyphRect.Add(new Rectangle(394 + offset, 0, 6, 8));   //s
                    glyphRect.Add(new Rectangle(401 + offset, 0, 5, 8));   //t
                    glyphRect.Add(new Rectangle(407 + offset, 0, 7, 8));   //u
                    glyphRect.Add(new Rectangle(414 + offset, 0, 7, 8));   //v
                    glyphRect.Add(new Rectangle(422 + offset, 0, 11, 8));  //w
                    glyphRect.Add(new Rectangle(434 + offset, 0, 6, 8));   //x
                    glyphRect.Add(new Rectangle(441 + offset, 0, 7, 8));   //y
                    glyphRect.Add(new Rectangle(449 + offset, 0, 6, 8));   //z

                    glyphRect.Add(new Rectangle(600, 0, 8, 8));   //...


                    charList.Add(' ');
                    charList.Add('$');
                    charList.Add('+');
                    charList.Add('-');
                    charList.Add('.');

                    charList.Add('0');
                    charList.Add('1');
                    charList.Add('2');
                    charList.Add('3');
                    charList.Add('4');
                    charList.Add('5');
                    charList.Add('6');
                    charList.Add('7');
                    charList.Add('8');
                    charList.Add('9');

                    charList.Add(':');

                    charList.Add('a');
                    charList.Add('b');
                    charList.Add('c');
                    charList.Add('d');
                    charList.Add('e');
                    charList.Add('f');
                    charList.Add('g');
                    charList.Add('h');
                    charList.Add('i');
                    charList.Add('j');
                    charList.Add('k');
                    charList.Add('l');
                    charList.Add('m');
                    charList.Add('n');
                    charList.Add('o');
                    charList.Add('p');
                    charList.Add('q');
                    charList.Add('r');
                    charList.Add('s');
                    charList.Add('t');
                    charList.Add('u');
                    charList.Add('v');
                    charList.Add('w');
                    charList.Add('x');
                    charList.Add('y');
                    charList.Add('z');

                    charList.Add('§');

                    numberCaractere = charList.Count;


                    /// NE CHANGE RIEN
                    for (int i = 0; i < numberCaractere; i++)
                        croppingList.Add(new Rectangle(0, 0, 16, 16));

                    Vector3List.Add(new Vector3(0, -6, 0));//SPACE
                    Vector3List.Add(new Vector3(0, -3, 0));//$
                    Vector3List.Add(new Vector3(1, -3, 0));//+
                    Vector3List.Add(new Vector3(1, -3, 0));//-
                    Vector3List.Add(new Vector3(0, -8, 0));//.

                    Vector3List.Add(new Vector3(0, -4, 0));//0
                    Vector3List.Add(new Vector3(0, -4, 0));//1
                    Vector3List.Add(new Vector3(0, -4, 0));//2
                    Vector3List.Add(new Vector3(0, -4, 0));//3
                    Vector3List.Add(new Vector3(0, -4, 0));//4
                    Vector3List.Add(new Vector3(0, -4, 0));//5
                    Vector3List.Add(new Vector3(0, -4, 0));//6
                    Vector3List.Add(new Vector3(0, -4, 0));//7
                    Vector3List.Add(new Vector3(0, -4, 0));//8
                    Vector3List.Add(new Vector3(0, -4, 0));//9

                    Vector3List.Add(new Vector3(2, -6, 0));//:

                    Vector3List.Add(new Vector3(0, -3, 0));//a
                    Vector3List.Add(new Vector3(0, -4, 0));//b
                    Vector3List.Add(new Vector3(0, -3, 0));//c
                    Vector3List.Add(new Vector3(0, -4, 0));//d
                    Vector3List.Add(new Vector3(0, -4, 0));//e
                    Vector3List.Add(new Vector3(0, -4, 0));//f
                    Vector3List.Add(new Vector3(0, -3, 0));//g
                    Vector3List.Add(new Vector3(0, -4, 0));//h
                    Vector3List.Add(new Vector3(0, -4, 0));//i
                    Vector3List.Add(new Vector3(0, -4, 0));//j
                    Vector3List.Add(new Vector3(0, -4, 0));//k
                    Vector3List.Add(new Vector3(0, -5, 0));//l
                    Vector3List.Add(new Vector3(0, -2, 0));//m
                    Vector3List.Add(new Vector3(0, -4, 0));//n
                    Vector3List.Add(new Vector3(0, -2, 0));//o
                    Vector3List.Add(new Vector3(0, -4, 0));//p
                    Vector3List.Add(new Vector3(0, -2, 0));//q
                    Vector3List.Add(new Vector3(0, -4, 0));//r
                    Vector3List.Add(new Vector3(0, -3, 0));//s
                    Vector3List.Add(new Vector3(0, -4, 0));//t
                    Vector3List.Add(new Vector3(0, -3, 0));//u
                    Vector3List.Add(new Vector3(0, -2, 0));//v
                    Vector3List.Add(new Vector3(0, 2, 0));//w
                    Vector3List.Add(new Vector3(0, -3, 0));//x
                    Vector3List.Add(new Vector3(0, -2, 0));//y
                    Vector3List.Add(new Vector3(0, -3, 0));//z


                    Vector3List.Add(new Vector3(0, 0, 0));//...
                    /// NE CHANGE RIEN

                    StatFont = new SpriteFont(StatF, glyphRect, croppingList, charList, 1, 11f, Vector3List, '§');


                    for (int i = 0; i < numberCaractere; i++)
                    {
                        glyphRect.Remove(glyphRect[0]);
                        charList.Remove(charList[0]);
                        croppingList.Remove(croppingList[0]);
                        Vector3List.Remove(Vector3List[0]);
                    }

                    #endregion

                    return StatFont;


            }

            return null;

        }


        public enum Font
        {

            UltimateFont = 1,
            ScoreFont = 2,
            StatFont = 3,

        } 


    }



}
