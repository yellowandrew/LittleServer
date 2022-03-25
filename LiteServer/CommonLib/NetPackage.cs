using System;
using System.IO;

namespace CommonLib
{
    public abstract class NetPackage
    {
        public UInt32 id;
        public string netid="-1";
        public NetPackage(UInt32 id) { this.id = id; }
        public abstract void Encode(BinaryWriter writer);
        public abstract void Decode(BinaryReader reader);
    }

}
