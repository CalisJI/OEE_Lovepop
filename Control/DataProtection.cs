using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OEE_dotNET.Control
{
    public static class DataProtection
    {
        static byte[] s_additionalEntropy = { 2, 0, 0, 3, 9, 9 };

        public static int EncryptDataToStream(byte[] Buffer, byte[] Entropy, DataProtectionScope Scope, Stream S)
        {
            if (Buffer == null)
                throw new ArgumentNullException(nameof(Buffer));
            if (Buffer.Length <= 0)
                throw new ArgumentException("The buffer length was 0.", nameof(Buffer));
            if (Entropy == null)
                throw new ArgumentNullException(nameof(Entropy));
            if (Entropy.Length <= 0)
                throw new ArgumentException("The entropy length was 0.", nameof(Entropy));
            if (S == null)
                throw new ArgumentNullException(nameof(S));

            int length = 0;

            // Encrypt the data and store the result in a new byte array. The original data remains unchanged.
            byte[] encryptedData = ProtectedData.Protect(Buffer, Entropy, Scope);

            // Write the encrypted data to a stream.
            if (S.CanWrite && encryptedData != null)
            {
                S.Write(encryptedData, 0, encryptedData.Length);

                length = encryptedData.Length;
            }

            // Return the length that was written to the stream.
            return length;
        }

        public static byte[] DecryptDataFromStream(byte[] Entropy, DataProtectionScope Scope, Stream S, int Length)
        {
            if (S == null)
                throw new ArgumentNullException(nameof(S));
            if (Length <= 0)
                throw new ArgumentException("The given length was 0.", nameof(Length));
            if (Entropy == null)
                throw new ArgumentNullException(nameof(Entropy));
            if (Entropy.Length <= 0)
                throw new ArgumentException("The entropy length was 0.", nameof(Entropy));

            byte[] inBuffer = new byte[Length];
            byte[] outBuffer;

            // Read the encrypted data from a stream.
            if (S.CanRead)
            {
                S.Read(inBuffer, 0, Length);

                outBuffer = ProtectedData.Unprotect(inBuffer, Entropy, Scope);
            }
            else
            {
                throw new IOException("Could not read the stream.");
            }

            // Return the decrypted data
            return outBuffer;
        }


        public static void WriteData(string data) 
        {
            byte[] toEncrypt = Encoding.ASCII.GetBytes(data);
            FileStream fStream = new FileStream(ApplicationConfig.Datapath, FileMode.OpenOrCreate);
            ApplicationConfig.SettingParameter.DecrypLength = EncryptDataToStream(toEncrypt, s_additionalEntropy, DataProtectionScope.CurrentUser, fStream);
            ApplicationConfig.UpdateConfig(ApplicationConfig.SettingParameter);
            fStream.Close();
            //Console.WriteLine(bytesWritten);
        }

        public static string ReadProtectData() 
        {
            if (File.Exists(ApplicationConfig.Datapath)) 
            {
                FileStream fStream = new FileStream(ApplicationConfig.Datapath, FileMode.Open);

                // Read from the stream and decrypt the data.
                byte[] decryptData = DecryptDataFromStream(s_additionalEntropy, DataProtectionScope.CurrentUser, fStream, ApplicationConfig.SettingParameter.DecrypLength);
                fStream.Close();
                return Encoding.ASCII.GetString(decryptData);
            }
            else 
            {
                return ":";
            }
           
        }
    }
}
