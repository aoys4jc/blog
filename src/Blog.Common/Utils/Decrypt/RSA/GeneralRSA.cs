using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Common
{
    public class RSAKey
    {
        public String PublicKey { get; set; }
        public String PrivateKey { get; set; }

        public RSAParameters Parameters { get; set; }
    }
    public class GeneralRSA
    {
        public const int DWKEYSIZE = 1024;
        /// <summary>
        /// 生成.net framework公私钥
        /// </summary>
        /// <param name="PrivateKeyPath"></param>
        /// <param name="PublicKeyPath"></param>
        public static RSAKey GetRASKey()
        {
            RSACryptoServiceProvider.UseMachineKeyStore = true;
            //声明一个指定大小的RSA容器
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(DWKEYSIZE);
            //取得RSA容易里的各种参数
            
            RSAParameters p = rsaProvider.ExportParameters(true);
            
            return new RSAKey()
            {
                Parameters=p,
                PublicKey = $"<RSAKeyValue><Modulus>{Convert.ToBase64String(p.Modulus)}</Modulus><Exponent>{Convert.ToBase64String(p.Exponent)}</Exponent></RSAKeyValue> ",
                PrivateKey =$"<RSAKeyValue><Modulus>{Convert.ToBase64String(p.Modulus)}</Modulus>" +
                            $"<Exponent>{Convert.ToBase64String(p.Exponent)}</Exponent>" +
                            $"<P>{Convert.ToBase64String(p.P)}</P><Q>{Convert.ToBase64String(p.Q)}</Q>" +
                            $"<DP>{Convert.ToBase64String(p.DP)}</DP><DQ>{Convert.ToBase64String(p.DQ)}</DQ><InverseQ>{Convert.ToBase64String(p.InverseQ)}</InverseQ></RSAKeyValue> "
            };
        }

        /// <summary>
        /// 生成js用的公钥
        /// </summary>
        /// <param name="exponent"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static String RendJSPublicKey(String exponent,String modules)
        {
            var e = Convert.FromBase64String(exponent);
            var m = Convert.FromBase64String(modules);
            string pubtemp = "{0}-{1}";
            return string.Format(
                   pubtemp,
                   BinaryHelper.BinaryToHexString(ref e, 0, e.Length),
                   BinaryHelper.BinaryToHexString(ref m, 0, m.Length));
        }
        
        public static String Decrypt(String source,String privateKey)
        {
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.ImportCspBlob(Convert.FromBase64String(privateKey));
           var buffer= rsaProvider.Decrypt(Encoding.UTF8.GetBytes(source),false);

            return Encoding.UTF8.GetString(buffer);
        }

        public static String Encrypt(String source, String publicKey)
        {
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
           
            rsaProvider.ImportCspBlob(Convert.FromBase64String(publicKey));
            var buffer = rsaProvider.Encrypt(Encoding.UTF8.GetBytes(source), false);
            return Encoding.UTF8.GetString(buffer);
            // sbyte[] key = BigInteger.Parse(keyString, NumberStyles.HexNumber).ToByteArray().Reverse().Select(x => (sbyte)x).ToArray();
        }

        #region .net core 标准的rsa加密
        public static string RSA_Decrypt(string encryptedText, string privateKey)
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);

            rsaProvider.ImportCspBlob(Convert.FromBase64String(privateKey));
            //OoyAXypi3VxumRgM0+gClBpp8uZj6XHrytBK3RTk7iC/XlbiRkWR8aUUIZl7j+CFnPZXQ4Uo3P+mpiy6YdBcXriJQn+35Fx4x7V4EtxzixbrTgBXlqy6hYqerppUbmDGw1vlL/te631rgiMfJP8NB/1vy1K7UI5KKZTTHxcHDIU=
            var buffer = Convert.FromBase64String(encryptedText);

            byte[] plainBytes = rsaProvider.Decrypt(buffer, false);

            string plainText = Encoding.UTF8.GetString(plainBytes, 0, plainBytes.Length);

            return plainText;
        }

        public static string RSA_Encrypt(string data, string publicKey)
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(cspParams);

            rsaProvider.ImportCspBlob(Convert.FromBase64String(publicKey));

            byte[] plainBytes = Encoding.UTF8.GetBytes(data);
            byte[] encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static Tuple<string, string> CreateKeyPair()
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 /* PROV_RSA_FULL */ };

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(2048, cspParams);

            string publicKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            string privateKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));

            return new Tuple<string, string>(privateKey, publicKey);
        }
        #endregion
    }
}
