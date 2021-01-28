using System.Collections.Generic;

public static class Tags
{
    public enum Tag
    {
        MAP_BORDER = 0, 
        SAND = 1, 
        PLAYER = 2, 
        CACTUS = 3, 
        SPIKE = 4
    }

    public static Dictionary<Tag, string> tags = new Dictionary<Tag, string>()
    {
        { Tag.MAP_BORDER, "MapBorder"},
        { Tag.SAND, "Sand" },
        { Tag.PLAYER, "Player" },
        { Tag.CACTUS, "Cactus" },
        { Tag.SPIKE, "Spike" }
    };
}
