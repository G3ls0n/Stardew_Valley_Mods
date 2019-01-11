using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PyTK.CustomElementHandler;
using StardewValley;
using StardewValley.Objects;

namespace Revitalize.Framework.Objects.Furniture
{
    /// <summary>
    /// Object which encapsulates all of the pieces that make up a chair object in-game.
    /// </summary>
    public class ChairMultiTiledObject:MultiTiledObject
    {

        public ChairMultiTiledObject() : base()
        {

        }

        public ChairMultiTiledObject(BasicItemInformation Info) : base(Info)
        {

        }

        public ChairMultiTiledObject(BasicItemInformation Info, Vector2 TilePosition) : base(Info, TilePosition)
        {

        }

        public ChairMultiTiledObject(BasicItemInformation Info,Vector2 TilePosition,Dictionary<Vector2, MultiTiledComponent> Objects) : base(Info, TilePosition, Objects) {


        }

        /// <summary>
        /// Rotate all chair components associated with this chair object.
        /// </summary>
        public override void rotate()
        {
            Revitalize.ModCore.log("Rotate!");
            foreach(KeyValuePair<Vector2, StardewValley.Object> pair in this.objects)
            {
                (pair.Value as ChairTileComponent).rotate();
            }
            foreach (KeyValuePair<Vector2, StardewValley.Object> pair in this.objects)
            {
                (pair.Value as ChairTileComponent).checkForSpecialUpSittingAnimation();
            }

            base.rotate();
        }

        public override Item getOne()
        {
            Dictionary<Vector2, MultiTiledComponent> objs = new Dictionary<Vector2, MultiTiledComponent>();
            foreach (var pair in this.objects)
            {
                objs.Add(pair.Key, (MultiTiledComponent)pair.Value);
            }

            return new ChairMultiTiledObject(this.info, this.TileLocation, objs);
        }

        public override ICustomObject recreate(Dictionary<string, string> additionalSaveData, object replacement)
        {
            BasicItemInformation data = (BasicItemInformation)CustomObjectData.collection[additionalSaveData["id"]];

            Dictionary<Vector2, MultiTiledComponent> objs = new Dictionary<Vector2, MultiTiledComponent>();
            foreach (var pair in this.objects)
            {
                objs.Add(pair.Key, (MultiTiledComponent)pair.Value);
            }

            return new ChairMultiTiledObject(data, (replacement as Chest).TileLocation, objs);
        }

        public override bool canBePlacedHere(GameLocation l, Vector2 tile)
        {
            return base.canBePlacedHere(l, tile);
        }

    }
}