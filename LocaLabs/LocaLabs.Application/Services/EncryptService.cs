using Microsoft.Extensions.ObjectPool;
using System.Security.Cryptography;
using System.Text;

namespace LocaLabs.Application.Services
{
    internal interface IEncryptService
    {
        string GetHashFrom(string value);
    }

    internal class EncryptService : IEncryptService
    {
        ObjectPool<StringBuilder> BuilderPool { get; }

        public EncryptService(ObjectPool<StringBuilder> builderPool) =>
            BuilderPool = builderPool;

        public string GetHashFrom(string value)
        {
            using var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));

            var sBuilder = BuilderPool.Get();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            var output = sBuilder.ToString();
            BuilderPool.Return(sBuilder);

            return output;
        }
    }
}