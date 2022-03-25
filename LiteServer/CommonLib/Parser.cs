using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CommonLib
{
    public interface IParser
    {
        byte[] WritePackageToBuffer(NetPackage package);
        NetPackage ReadPackageFromBuffer(byte[] buf);
    }
    public class Parser : IParser
    {
        Dictionary<UInt32, Type> packagetypes = new Dictionary<UInt32, Type>();
        public Parser(Assembly asm = null)
        {
            if (asm == null)
            {
                AssemblyName[] assemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies();
                foreach (var dll in assemblies)
                {
                    GetPackage(Assembly.Load(dll));
                }
            }
            else {
                GetPackage(asm);
            }
            

            foreach (var item in packagetypes.Values)
            {
                Logger.Info($"FInd Package: {item.Name }");
            }
        }

        void GetPackage(Assembly asm)
        {
            var packageClasses = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(NetPackage)));
            foreach (var cs in packageClasses)
            {
                var attr = cs.GetCustomAttributes(typeof(PackageTypeAttribute), false);
                if (attr.FirstOrDefault() is PackageTypeAttribute packageTypeAttribute)
                {
                    packagetypes.Add(packageTypeAttribute.id, cs);
                }
            }
        }

        public NetPackage ReadPackageFromBuffer(byte[] buf)
        {
            using (MemoryStream ms = new MemoryStream(buf))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    var packageType = br.ReadUInt32();
                    if (packagetypes.TryGetValue(packageType, out var type))
                    {
                        var package = Activator.CreateInstance(type) as NetPackage;
                        package.Decode(br);
                        Logger.Info($"Read Package: {package.GetType()}");
                        return package;
                    }
                }
            }
            throw new InvalidOperationException("Unkown Package !!");

        }

        public byte[] WritePackageToBuffer(NetPackage package)
        {
            Logger.Info($"Write Package: {package.GetType()}");
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    package.Encode(bw);
                    bw.Flush();
                    byte[] byteArray = new byte[(int)ms.Length];
                    System.Buffer.BlockCopy(ms.GetBuffer(), 0, byteArray, 0, (int)ms.Length);
                    return byteArray;
                }
            }
        }

    }

}
