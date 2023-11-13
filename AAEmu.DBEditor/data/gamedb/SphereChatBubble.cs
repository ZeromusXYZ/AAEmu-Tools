using System;
using System.Collections.Generic;

namespace AAEmu.DBEditor.data.gamedb;

public partial class SphereChatBubble
{
    public long? Id { get; set; }

    public long? SphereBubbleId { get; set; }

    public byte[] IsStart { get; set; }

    public string Speech { get; set; }

    public long? NpcId { get; set; }

    public long? NpcSpawnerId { get; set; }

    public long? NextBubble { get; set; }

    public long? SoundId { get; set; }

    public long? Angle { get; set; }

    public long? ChatBubbleKindId { get; set; }

    public string Facial { get; set; }

    public long? CameraId { get; set; }

    public string ChangeSpeakerName { get; set; }
}
