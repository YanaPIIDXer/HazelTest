using Hazel;

namespace HazelCommon.HazelExt
{
    /// <summary>
    /// シリアライザ
    /// HazelについているMessageReader / MessageWriterの両方に対応するオーバーロードをいちいち書かなくてもいいようにするためのもの
    /// </summary>
    public class Serializer
    {
        /// <summary>
        /// 書き込みモードか？
        /// </summary>
        private bool IsWriteMode { get { return (Reader == null); } }

        /// <summary>
        /// MessageReader
        /// </summary>
        private MessageReader Reader = null;

        /// <summary>
        /// MessageWriter
        /// </summary>
        private MessageWriter Writer = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Reader">MessageReader</param>
        public Serializer(MessageReader Reader)
        {
            this.Reader = Reader;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="Writer">MessageWriter</param>
        public Serializer(MessageWriter Writer)
        {
            this.Writer = Writer;
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref int Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadInt32();
            }
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref uint Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadUInt32();
            }
        }
        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref short Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadInt16();
            }
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref ushort Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadUInt16();
            }
        }
        
        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref sbyte Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadSByte();
            }
        }

        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref byte Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadByte();
            }
        }
        
        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref bool Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadBoolean();
            }
        }
        
        /// <summary>
        /// シリアライズ
        /// </summary>
        /// <param name="Data">データ</param>
        public void Serialize(ref string Data)
        {
            if (IsWriteMode)
            {
                Writer.Write(Data);
            }
            else
            {
                Data = Reader.ReadString();
            }
        }
    }
}
