using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class ModubusASCIIFrame
    {

        #region Frame

        public string StartCharacter { get; set; }

        public short Address { get; set; }

        public short FunctionCode { get; set; }

        public byte[] Data { get; set; }

        public short LRC { get; set; }

        public string StopCharacters { get; set; }

        #endregion

        #region Methods
        public bool IsValid()
        {
            if(CalculateLRC(this.Data) == this.LRC)
                return true;
            return false;
        }
        public static short CalculateLRC(byte[] bytes)
        {
            int LRC = 0;
            for(int i = 0; i < bytes.Length; i++)
            {
                LRC -= bytes[i];
            }
            byte t = (byte)LRC;
            if(t > 127)
            {
                t = 127;
            }
            else if(t < 0)
            {
                t = 0;
            }
            return (short)( (ushort)( t ) );
        }

        public byte[] GetFrame()
        {
            List<byte> returnList = new List<byte>();

            Encoding.ASCII.GetBytes(this.StartCharacter).ToList().ForEach(x => returnList.Add(x));
            if(this.Address > 127)
            {
                returnList.Add(1);
                returnList.Add((byte)( 255 - (byte)this.Address ));
            }
            else
            {
                BitConverter.GetBytes(this.Address).ToList().ForEach(x => returnList.Add(x));
            }

            BitConverter.GetBytes(this.FunctionCode).ToList().ForEach(x => returnList.Add(x));
            this.Data.ToList().ForEach(x => returnList.Add(x));
            BitConverter.GetBytes(this.LRC).ToList().ForEach(x => returnList.Add(x));
            Encoding.ASCII.GetBytes(this.StopCharacters).ToList().ForEach(x => returnList.Add(x));

            return returnList.ToArray();
        }
        #endregion

        #region Ctors
        public ModubusASCIIFrame(short address, short functionCode, string data)
        {
            this.StartCharacter = ":";
            this.StopCharacters = "\r\n";
            this.Address = address;
            this.FunctionCode = functionCode;
            this.Data = Encoding.ASCII.GetBytes(data);
            this.LRC = CalculateLRC(this.Data);
        }

        public ModubusASCIIFrame(byte[] bytes, bool AreStopCharactes = false)
        {
            this.StartCharacter = ":";
            if(bytes[1] == 1)
            {
                this.Address = (short)(255 - bytes[2]);
            }
            else
            {
                this.Address = BitConverter.ToInt16(bytes, 1);
            }
            this.FunctionCode = BitConverter.ToInt16(bytes, 1 + 2);
            int amount = AreStopCharactes ? 4 : 2;
            this.Data = bytes.Skip(5).Take(bytes.Length - amount - 5).ToArray();
            this.LRC = BitConverter.ToInt16(bytes, 1 + 2 + 2 + this.Data.Length);
            this.StopCharacters = "\r\n";
        }
        #endregion


        public static void Main(string[] args)
        {
            var t1 = CalculateLRC(Encoding.ASCII.GetBytes(""));
            BitConverter.GetBytes(t1);
            short tmp = BitConverter.ToInt16(BitConverter.GetBytes(t1), 0);
            var str = Encoding.ASCII.GetString(new ModubusASCIIFrame(1, 2, "").GetFrame(), 0, new ModubusASCIIFrame(1, 2, "").GetFrame().Length);
            var f = new ModubusASCIIFrame(Encoding.ASCII.GetBytes(str.Substring(0, str.Length - 2)), false);

            if(t1 == f.LRC)
            {
                Console.WriteLine(true);
            }
            else
            {

            }

        }


    }


}


