using HazelCommon.HazelExt;

namespace HazelCommon.Packet
{
    /// <summary>
    /// テスト用パケット
    /// </summary>
    public class PacketTest : PacketBase
    {
        /// <summary>
        /// int型データ
        /// </summary>
        public int IntData = 0;

        /// <summary>
        /// short型データ
        /// </summary>
        public short ShortData = 0;

        /// <summary>
        /// float型データ
        /// </summary>
        public float FloatData = 0.0f;

        /// <summary>
        /// string型データ
        /// </summary>
        public string StringData = "";

        /// <summary>
        /// パケットＩＤ
        /// </summary>
        public override ushort PacketID { get { return 1; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PacketTest()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="IntData">int型データ</param>
        /// <param name="ShortData">short型データ</param>
        /// <param name="FloatData">float型データ</param>
        /// <param name="StringData">string型データ</param>
        public PacketTest(int IntData, short ShortData, float FloatData, string StringData)
        {
            this.IntData = IntData;
            this.ShortData = ShortData;
            this.FloatData = FloatData;
            this.StringData = StringData;
        }

        /// <summary>
        /// パケット本体のシリアライズ
        /// </summary>
        /// <param name="Sr">シリアライザ</param>
        protected override void SerializeBody(Serializer Sr)
        {
            Sr.Serialize(ref IntData);
            Sr.Serialize(ref ShortData);
            Sr.Serialize(ref FloatData);
            Sr.Serialize(ref StringData);
        }
    }
}
