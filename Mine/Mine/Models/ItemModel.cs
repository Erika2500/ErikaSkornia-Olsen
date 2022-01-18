using System;

namespace Mine.Models
{
    /// <summary>
    /// Items for the characters and monsters to use
    /// </summary>
    public class ItemModel
    {
        // ID for the item
        public string Id { get; set; }
        // Display text for the item
        public string Text { get; set; }
        // Description for the item
        public string Description { get; set; }
        // Value attribute of the item is +9 damage
        public int Value { get; set; } = 0;
    }
}