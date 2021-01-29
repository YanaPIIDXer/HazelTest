using HazelCommon.HazelExt;

namespace HazelCommon.Packet
{
    /// <summary>
    /// パケット基底クラス
    /// </summary>
    public abstract class PacketBase
    {
        /// <summary>
        /// パケットＩＤ
        /// </summary>
        public abstract ushort PacketID { get; }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Sr">シリアライザ</param>
        public void Serialize(Serializer Sr)
        {
            if (Sr.IsWriteMode)
            {
                // 書き込み時はパケットＩＤを書き込む
                ushort Id = PacketID;
                Sr.Serialize(ref Id);
            }

            SerializeBody(Sr);
        }

        /// <summary>
        /// パケット本体のシリアライズ
        /// </summary>
        /// <param name="Sr">シリアライザ</param>
        protected abstract void SerializeBody(Serializer Sr);
    }
}
