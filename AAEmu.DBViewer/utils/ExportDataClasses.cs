namespace AAEmu.DBViewer.utils
{
    public class NpcExportDataPosition
    {
        public float X = 0f;
        public float Y = 0f;
        public float Z = 0f;
        public sbyte RotationX = 0;
        public sbyte RotationY = 0;
        public sbyte RotationZ = 0;
    }

    public class NpcExportData
    {
        public long Id = 0;
        public long Count = 1;
        public long UnitId = 0;
        public string Title = string.Empty;
        public NpcExportDataPosition Position = new();
        public float Scale = 0f;
        public long Zone = 0;
    }

    public class DoodadExportData
    {
        public long Id = 0;
        public long UnitId = 0;
        public string Title = string.Empty;
        public NpcExportDataPosition Position = new();
        public float Scale = 0f;
        public long FuncGroupId = 0;
        public long Zone = 0;
    }
}
