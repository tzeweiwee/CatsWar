using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Project.Hamsters;
using Project.Cats;

namespace Project
{
    class Items : Game1
    {
        protected MouseState mouse = new MouseState();
        protected KeyboardState keyboard = new KeyboardState();

        public static List<Obj> objList = new List<Obj>();
        public static Obj[,] tileArray = new Obj[4,4];
        public static List<Obj> hamsterList = new List<Obj>();
        public static List<Obj> catList = new List<Obj>();


        public static void Reset()
        {
            foreach(Obj o in objList)
            {
                o.alive = false; //setting all to NOT alive
            }

            foreach (Obj o in tileArray)
            {
                o.alive = false; //setting all to NOT alive
            }

            foreach (Obj o in hamsterList)
            {
                o.alive = false; //setting all to NOT alive
            }

            foreach (Obj o in catList)
            {
                o.alive = false; //setting all to NOT alive
            }
        }

        public static void Initialize()
        {
            //For Tiles & Lanes
            int posX = 100;
            int posY = 200;  
            int incr = 64;

            for(int i=0; i<4; i++)
            {
                for(int j=0; j<4; j++)
                {
                    Obj o = new Tile(new Vector2(posX, posY), i, j, 0);
                    tileArray[i,j] = o;

                    posX += incr;

                   
                }

                for (int k = 0; k < 2; k++)
                {
                    Obj o = new Lane(new Vector2(posX, posY), i);
                    objList.Add(o);
                    posX += incr;
                }

                posX = 100;
                posY += incr;
            }

            //Timer Panel
            posX = 100;
            posY = 50;

            for(int i = 0; i < 6; i++)
            {
                if (i == 3)
                {

                    posX = 100;
                    posY += incr;
                }

                Obj o = new Obj(new Vector2(posX, posY));
                o.spriteName = "panel";
                objList.Add(o);

                posX += incr;
            }

            //Add Hamster

            Obj o1 = new hamAttacker(tileArray[0, 0].position);
            hamsterList.Add(o1);
            Obj o2 = new hamDefender(tileArray[1, 1].position);
            hamsterList.Add(o2);
            Obj o3 = new hamTrap(tileArray[2, 2].position);
            hamsterList.Add(o3);

            Obj c1 = new catSpecial(tileArray[0, 3].position + new Vector2((64*2), 0));
            catList.Add(c1);

            Obj c2 = new catSpecial(tileArray[1, 3].position + new Vector2((64 * 2), 0));
            catList.Add(c2);

            Obj c3 = new catSpecial(tileArray[2, 3].position + new Vector2((64 * 2), 0));
            catList.Add(c3);

            Obj c4 = new catSpecial(tileArray[3, 3].position + new Vector2((64 * 2), 0));
            catList.Add(c4);


        }


    }
}
