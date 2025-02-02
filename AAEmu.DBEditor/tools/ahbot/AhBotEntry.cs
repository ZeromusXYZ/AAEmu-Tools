using AAEmu.DBEditor.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAEmu.DBEditor.tools.ahbot
{
    public class AhBotEntry
    {
        /// <summary>
        /// ItemId of listed item
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int ItemId { get; set; }

        /// <summary>
        /// ItemId of listed item
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public long ItemEntryCount { get; set; }

        /// <summary>
        /// GradeId of listed item
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public byte GradeId { get; set; }

        /// <summary>
        /// Number of items per entry of this item
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Buy-out price of the item
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public long Price { get; set; }

        /// <summary>
        /// Starting bid for this item if it's larger than zero
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public long StartBid { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public string Comment { get; set; } = string.Empty;

        public override string ToString()
        {
            var itemNamePart = Data.Server?.LocalizedText?.GetValueOrDefault(("items", "name", ItemId)) ?? $"<item:{ItemId}>";
            var countPart = Quantity > 1 ? $" x {Quantity}" : "";
            var gradePart = (Data.Server?.LocalizedText?.GetValueOrDefault(("item_grades", "name", GradeId)) ?? $"<grade:{GradeId}>") + " ";
            return gradePart + itemNamePart + countPart;
        }
    }
}
