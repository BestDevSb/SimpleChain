using System;
using System.Text;
using System.Security.Cryptography;

namespace SimpleBlockChain.Core.Model
{
    public class Block
    {
        public int Index { get; set; }

        public DateTime Timestamp { get; set; }

        public int Nonce { get; set; }

        public string Hash { get; set; }

        public string PreviousHash { get; set; }

        public string Data { get; set; }

        public string GetHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] data = Encoding.UTF8.GetBytes($"{Timestamp}-{PreviousHash}-{Data}-{Nonce}");
            data = sha256.ComputeHash(data);
            return Convert.ToBase64String(data);
        }
    }
}
