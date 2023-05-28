using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AAEmu.DBEditor.Models.Game
{
    [Table("localized_texts")]
    internal class LocalizedText
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("tbl_name")]
        public string TblName { get; set; }

        [Column("tbl_column_name")]
        public string TblColumnName { get; set; }

        [Column("idx")]
        public int Idx { get; set; }

        [Column("ko"), DefaultValue("")]
        public string Ko { get; set; }
        [Column("ko_ver"), DefaultValue(0)]
        public int KoVer { get; set; }

        [Column("us_en"), DefaultValue("")]
        public string UsEn { get; set; }
        [Column("us_en_ver"), DefaultValue(0)]
        public int UsEnVer { get; set; }

        [Column("zh_cn"), DefaultValue("")]
        public string ZhCn { get; set; }
        [Column("zh_cn_ver"), DefaultValue(0)]
        public int ZhCnVer { get; set; }

        [Column("ja"), DefaultValue("")]
        public string Ja { get; set; }
        [Column("ja_ver"), DefaultValue(0)]
        public int JaVer { get; set; }

        [Column("ru"), DefaultValue("")]
        public string Ru { get; set; }
        [Column("ru_ver"), DefaultValue(0)]
        public int RuVer { get; set; }

        [Column("zh_tw"), DefaultValue("")]
        public string ZhTw { get; set; }
        [Column("zh_tw_ver"), DefaultValue(0)]
        public int ZhTwVer { get; set; }

        [Column("de"), DefaultValue("")]
        public string De { get; set; }
        [Column("de_ver"), DefaultValue(0)]
        public int DeVer { get; set; }

        [Column("fr"), DefaultValue("")]
        public string Fr { get; set; }
        [Column("fr_ver"), DefaultValue(0)]
        public int FrVer { get; set; }
    }
}
