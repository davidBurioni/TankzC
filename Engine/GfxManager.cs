using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using OpenTK;
namespace TankzC
{
    static class GfxManager
    {
        static Dictionary<string, Texture> textures;

        public static Texture AddTexture(string name, string filePath)
        {
            Texture t = new Texture(filePath);
            textures.Add(name, t);

            return t;
        }

        //public static Texture GetTexture(string name)
        //{
        //    if (textures.ContainsKey(name))
        //    {
        //        return textures[name];
        //    }
        //    return null;
        //}

        //public static void RemoveAll()
        //{
        //    textures.Clear();
        //}

        public static void RemoveAll()
        {
            //textures.Clear();

            spritesheets.Clear();
        }

        //file copiato

        static Dictionary<string, Tuple<Texture, List<Animation>>> spritesheets;

        static GfxManager()
        {
            spritesheets = new Dictionary<string, Tuple<Texture, List<Animation>>>();
        }

        private static Animation LoadAnimation(
            XmlNode animationNode, int width, int height)
        {
            XmlNode currNode = animationNode.FirstChild;
            bool loop = bool.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            float fps = float.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int rows = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int cols = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startX = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startY = int.Parse(currNode.InnerText);

            return new Animation(width, height, cols, rows, fps, loop, startX, startY);
        }

        static void LoadSpritesheet(XmlNode spritesheetNode)
        {
            XmlNode nameNode = spritesheetNode.FirstChild;

            string name = nameNode.InnerText;
            XmlNode filenameNode = nameNode.NextSibling;
            Texture texture = new Texture(filenameNode.InnerText);

            XmlNode frameNode = filenameNode.NextSibling;


            if (frameNode.HasChildNodes)
            {
                List<Animation> animations = new List<Animation>();
                int width = int.Parse(frameNode.FirstChild.InnerText);
                int height = int.Parse(frameNode.LastChild.InnerText);
                XmlNode animationsNode = frameNode.NextSibling;

                foreach (XmlNode animationNode in animationsNode.ChildNodes)
                {
                    animations.Add(LoadAnimation(animationNode, width, height));
                }

                AddSpritesheet(name, texture, animations);

            }
            else
            {
                AddSpritesheet(name, texture);
            }
            //AddSpritesheet(name, texture, animations);
        }

        public static void Load()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Assets/Asset.xml");

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode spritesheetNode in root.ChildNodes)
            {
                LoadSpritesheet(spritesheetNode);
            }
        }

        public static List<string> LoadTiledSet(string filename)
        {
            List<string> tile = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load("Assets/Map/env_lv.tsx");

            XmlNodeList imageNodes = doc.SelectNodes("//image");

            foreach (XmlNode nodo in imageNodes)
            {
                string source = nodo.Attributes["source"].Value;

                Texture texture = new Texture("Assets/Map/" + source);
                string name = Path.GetFileNameWithoutExtension(source);

                AddSpritesheet(name, texture);

                tile.Add(name);
            }
            return tile;
        }

        public static void LoadTiledMap(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode root = doc.DocumentElement;
            int cols = int.Parse(root.Attributes["width"].Value);
            int rows = int.Parse(root.Attributes["height"].Value);
            int tileWidth = int.Parse(root.Attributes["tilewidth"].Value);
            int tileHeight = int.Parse(root.Attributes["tileheight"].Value);

            XmlNode tileset = root.FirstChild;
            string source = tileset.Attributes["source"].Value;

            List<string> tileNames = LoadTiledSet("Assets/Map/" + source);
            XmlNode data = tileset.NextSibling.FirstChild;

            string map = data.InnerText;

            string[] mapIndexes = map.Split(',');

            for (int i = mapIndexes.Length - 1; i > 0; i--)
            {
                int index = int.Parse(mapIndexes[i]);
                if (index > 0)
                {
                    --index;
                    string tileName = tileNames[index];
                    Vector2 pos = new Vector2(
                        tileWidth * (i % cols),
                        tileHeight * (i / cols)
                        );
                    if (mapIndexes[i] == "1")
                    {
                        Tile tile = new Tile(pos, tileName);
                    }
                    else
                    {
                        Stone stone = new Stone(pos, tileName);
                    }
                }
            }
        }

        public static void AddSpritesheet(string name, Texture t)
        {
            List<Animation> a = new List<Animation>();
            a.Add(new Animation(t.Width, t.Height));
            AddSpritesheet(name, t, a);
            //AddSpritesheet.Add(name, t, a);
        }

        public static void AddSpritesheet(string name, Texture t, List<Animation> a)
        {
            spritesheets.Add(name, new Tuple<Texture, List<Animation>>(t, a));
        }

        public static Tuple<Texture, List<Animation>> GetSpritesheet(string name)
        {
            if (spritesheets.ContainsKey(name))
            {
                return spritesheets[name];
            }
            return null;
        }
    }
}
