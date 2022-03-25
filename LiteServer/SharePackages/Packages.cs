using System;
using System.IO;
using CommonLib;

namespace SharePackages
{
    [PackageType(PackageType.ERROR)]
    public class ErrorPackage : NetPackage
    {
        public int ErrorType { get; set; }
        public string ErrorMsg { get; set; }
        public ErrorPackage() : base(PackageType.ERROR)
        {

        }

        public override void Encode(BinaryWriter writer)
        {
            writer.Write((UInt32)id);
            writer.Write(ErrorType);
            writer.Write(ErrorMsg);
            writer.Write(netid);

        }
        public override void Decode(BinaryReader reader)
        {
            ErrorType = reader.ReadInt32();
            ErrorMsg = reader.ReadString();
            netid = reader.ReadString();
        }
    }

    [PackageType(PackageType.LOGIN_C2S)]
    public class LoginRequestPackage : NetPackage
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginRequestPackage() : base(PackageType.LOGIN_C2S)
        {

        }

        public override void Encode(BinaryWriter writer)
        {
            writer.Write((UInt32)id);
            writer.Write(Username);
            writer.Write(Password);
            writer.Write(netid);

        }
        public override void Decode(BinaryReader reader)
        {
            Username = reader.ReadString();
            Password = reader.ReadString();
            netid = reader.ReadString();
        }
    }

    [PackageType(PackageType.LOGIN_S2C)]
    public class LoginResponePackage : NetPackage
    {

        public string msg { get; set; }

        public LoginResponePackage() : base(PackageType.LOGIN_S2C)
        {

        }

        public override void Encode(BinaryWriter writer)
        {
            writer.Write((UInt32)id);
            writer.Write(msg);
            writer.Write(netid);
        }

        public override void Decode(BinaryReader reader)
        {
            msg = reader.ReadString();
            netid = reader.ReadString();
        }
    }

    [PackageType(PackageType.FIGHT_C2S)]
    public class FightRequestPackage : NetPackage
    {
        public string Map { get; set; }
        public FightRequestPackage() : base(PackageType.FIGHT_C2S)
        {

        }

        public override void Encode(BinaryWriter writer)
        {
            writer.Write((UInt32)id);
            writer.Write(Map);
            writer.Write(netid);

        }
        public override void Decode(BinaryReader reader)
        {
            Map = reader.ReadString();
            netid = reader.ReadString();
        }
    }

    [PackageType(PackageType.FIGHT_S2C)]
    public class FightResponePackage : NetPackage
    {

        public string msg { get; set; }

        public FightResponePackage() : base(PackageType.FIGHT_S2C)
        {

        }

        public override void Encode(BinaryWriter writer)
        {
            writer.Write((UInt32)id);
            writer.Write(msg);
            writer.Write(netid);
        }

        public override void Decode(BinaryReader reader)
        {
            msg = reader.ReadString();
            netid = reader.ReadString();
        }
    }

    [PackageType(PackageType.SERVICE)]
    public class ServicePackage : NetPackage
    {

        public string worklist { get; set; }
        public ServicePackage() : base(PackageType.SERVICE)
        {

        }

        public override void Encode(BinaryWriter writer)
        {
            writer.Write((UInt32)id);
            writer.Write(worklist);
            writer.Write(netid);
        }
        public override void Decode(BinaryReader reader)
        {
            worklist = reader.ReadString();
            netid = reader.ReadString();
        }
    }

}
