using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace AroLibraries.ExtensionMethods.Cryptography
{
    public static class X509Certificate2Ext
    {
        public static bool IsVerified(this X509Certificate2 iCertificate, byte[] iSignature)
        {
            SignedCms verifyCms = new SignedCms();
            verifyCms.Decode(iSignature);
            try
            {
                verifyCms.CheckSignature(new X509Certificate2Collection(iCertificate), false);
                return true;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }
    }
}