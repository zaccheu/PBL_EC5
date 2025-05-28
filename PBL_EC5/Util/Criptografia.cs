using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace PBL_EC5.Util
{
    public class Criptografia
    {
        /// <summary>
        /// Vetor de bytes utilizados para a criptografia (Chave Externa)
        /// </summary>
        private static readonly byte[] bIV = {
                0x50, 0x08, 0xF1, 0xDD,
                0xDE, 0x3C, 0xF2, 0x18,
                0x44, 0x74, 0x19, 0x2C,
                0x53, 0x49, 0xAB, 0xBC
            };

        /// <summary>
        /// Representação de valor em base 64 (Chave Interna).
        /// Deve decodificar para 32 bytes para AES-256.
        /// </summary>
        private const string cryptoKey = "QEJpcHRvZEJhZm1hcyBjb2OgUmlu4mRhZWwgLyXIRVX=";
        // O Valor acima representa a transformação para base64 de
        // um conjunto de 32 caracteres (8 * 32 = 256bits)

        /// <summary>
        /// Método de criptografia de valor utilizando AES-256.
        /// </summary>
        /// <param name="text">Valor a ser criptografado.</param>
        /// <returns>Valor criptografado em base64.</returns>
        public static string Encrypt(string text)
        {
            try
            {
                // Se a string não está vazia, executa a criptografia
                if (!string.IsNullOrEmpty(text))
                {
                    // Converte a chave de Base64 para bytes
                    byte[] bKey = Convert.FromBase64String(cryptoKey);

                    // Converte o texto para bytes usando UTF8
                    byte[] bText = Encoding.UTF8.GetBytes(text);

                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.KeySize = 256; // AES-256
                        aesAlg.BlockSize = 128; // Bloco de 128 bits
                        aesAlg.Mode = CipherMode.CBC;
                        aesAlg.Padding = PaddingMode.PKCS7;

                        aesAlg.Key = bKey;
                        aesAlg.IV = bIV;

                        // Cria o encryptor
                        using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                        using (MemoryStream msEncrypt = new MemoryStream())
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(bText, 0, bText.Length);
                            csEncrypt.FlushFinalBlock();
                            byte[] encrypted = msEncrypt.ToArray();
                            return Convert.ToBase64String(encrypted);
                        }
                    }
                }
                else
                {
                    // Se a string for vazia, retorna nulo
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Se algum erro ocorrer, dispara a exceção
                throw new ApplicationException("Erro ao criptografar", ex);
            }
        }

        /// <summary>
        /// Método de descriptografia utilizando AES-256.
        /// </summary>
        /// <param name="text">Texto criptografado em base64.</param>
        /// <returns>Valor descriptografado.</returns>
        public static string Decrypt(string text)
        {
            try
            {
                // Substitui espaços por '+' caso tenha sido passado via URL
                text = text.Replace(" ", "+");

                // Se a string não está vazia, executa a descriptografia
                if (!string.IsNullOrEmpty(text))
                {
                    // Converte a chave de Base64 para bytes
                    byte[] bKey = Convert.FromBase64String(cryptoKey);

                    // Converte o texto criptografado de Base64 para bytes
                    byte[] bText = Convert.FromBase64String(text);

                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.KeySize = 256; // AES-256
                        aesAlg.BlockSize = 128; // Bloco de 128 bits
                        aesAlg.Mode = CipherMode.CBC;
                        aesAlg.Padding = PaddingMode.PKCS7;

                        aesAlg.Key = bKey;
                        aesAlg.IV = bIV;

                        // Cria o decryptor
                        using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                        using (MemoryStream msDecrypt = new MemoryStream())
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                        {
                            csDecrypt.Write(bText, 0, bText.Length);
                            csDecrypt.FlushFinalBlock();
                            byte[] decrypted = msDecrypt.ToArray();
                            return Encoding.UTF8.GetString(decrypted);
                        }
                    }
                }
                else
                {
                    // Se a string for vazia, retorna nulo
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Se algum erro ocorrer, dispara a exceção
                throw new ApplicationException("Erro ao descriptografar", ex);
            }
        }

        /// <summary>
        /// Método para codificar a indiferença de caixa (case insensitivity).
        /// </summary>
        /// <param name="originalText">Texto original.</param>
        /// <param name="markerChar">Caractere marcador.</param>
        /// <returns>Texto codificado.</returns>
        public static string EncodeCasingIndifference(string originalText, char markerChar)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in originalText)
            {
                if (char.IsUpper(c))
                    sb.Append(markerChar);

                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Método para decodificar a indiferença de caixa (case insensitivity).
        /// </summary>
        /// <param name="encryptedText">Texto codificado.</param>
        /// <param name="markerChar">Caractere marcador.</param>
        /// <returns>Texto decodificado.</returns>
        public static string DecodeCasingIndifference(string encryptedText, char markerChar)
        {
            StringBuilder sb = new StringBuilder();
            bool nextCharIsUpper = false;

            foreach (char c in encryptedText)
            {
                if (c == markerChar)
                {
                    nextCharIsUpper = true;
                    continue;
                }

                if (nextCharIsUpper)
                    sb.Append(char.ToUpperInvariant(c));
                else
                    sb.Append(char.ToLowerInvariant(c));

                nextCharIsUpper = false;
            }

            return sb.ToString();
        }
    }
}
