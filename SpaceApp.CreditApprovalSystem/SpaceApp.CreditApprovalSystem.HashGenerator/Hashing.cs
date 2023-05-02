using SpaceApp.CreditApprovalSystem.HashGeneratorContracts;
using System.Security.Cryptography;
using System.Text;

namespace SpaceApp.CreditApprovalSystem.HashGenerator
{
    public class Hashing : IHashing
    {
        public byte[] GetHash(string message)
        {
            UnicodeEncoding ue = new UnicodeEncoding();
            if (string.IsNullOrEmpty(message))
            {
                return new byte[0];
            }

            byte[] messageBytes = ue.GetBytes(message);
            SHA256 shHash = SHA256.Create();
            return shHash.ComputeHash(messageBytes);
        }

        public bool CompareHash(byte[] sentHashValue, string messageString)
        {
            byte[] compareHashValue;

            UnicodeEncoding ue = new UnicodeEncoding();

            byte[] messageBytes = ue.GetBytes(messageString);

            SHA256 shHash = SHA256.Create();
            compareHashValue = shHash.ComputeHash(messageBytes);

            for (int x = 0; x < sentHashValue.Length; x++)
            {
                if (sentHashValue[x] != compareHashValue[x])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
