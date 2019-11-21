using System.Collections.Generic;
using System.IO;
using Android.Content.Res;
using Echo.Common;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Echo.Android
{
    class ContentLoader : IContentLoader
    {
        private readonly AssetManager AssetManager;

        public ContentLoader(AssetManager AssetManager)
        {
            this.AssetManager = AssetManager;
        }

        public Dictionary<string, Texture2D> LoadTextures(ContentManager value)
        {
            var result = new Dictionary<string, Texture2D>();

            var textures = AssetManager.List("Content/Textures");

            foreach (var texture in textures)
            {
                var key = Path.GetFileNameWithoutExtension(texture);
                result.Add(key, value.Load<Texture2D>($"Textures/{key}"));
            }            

            return result;
        }
    }
}