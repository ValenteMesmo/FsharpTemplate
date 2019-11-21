namespace Echo.Common

open Microsoft.Xna.Framework.Content
open System.Collections.Generic
open Microsoft.Xna.Framework.Graphics

type IContentLoader =
   abstract member LoadTextures: ContentManager -> Dictionary<string, Texture2D>