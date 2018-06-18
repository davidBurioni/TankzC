using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzC
{
    static class FontManager
    {
        static Dictionary<string,Font> fonts;

        public static void Init()
        {
            fonts = new Dictionary<string, Font>();
        }

        public static void AddFont(string textureName, string texturePath, int numColumns, int firstCharacterASCIIvalue, int charWidth, int charHeight)
        {
            Font f = new Font(textureName, texturePath, numColumns, firstCharacterASCIIvalue, charWidth, charHeight);

            fonts.Add(textureName, f);
        }

        public static Font GetFont(string textureName)
        {
            if (fonts.ContainsKey(textureName))
            {
                return fonts[textureName];
            }
            return null;
        }
    }
}
