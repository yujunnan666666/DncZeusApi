using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace ZDClass
{
    class OperateIni
    {
        //默认密钥
        private static string AESKey = "[45/*YUIdse..e;]";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        

        public string ReadIni(string vs_section, string vs_key, string vs_filePath)

        {

            StringBuilder temp = new StringBuilder();
            GetPrivateProfileString(vs_section, vs_key, "", temp, 255, vs_filePath);
            string ls_value = temp.ToString();
            return ls_value;
        }

        public string ReadIni_Database(string vs_section, string vs_path)

        {

            StringBuilder temp = new StringBuilder();

            GetPrivateProfileString(vs_section, "DBSource", "", temp, 255, vs_path);
            string source = temp.ToString();

            GetPrivateProfileString(vs_section, "DBUid", "", temp, 255, vs_path);
            string uid = temp.ToString();

            GetPrivateProfileString(vs_section, "DBPswd", "", temp, 255, vs_path);
            string pwd = temp.ToString();

            GetPrivateProfileString(vs_section, "DBName", "", temp, 255, vs_path);
            string name = temp.ToString();

            pwd = AESDecrypt(pwd); //解密

            string connectionStr = @"server=" + source + ";database=" + name + ";uid=" + uid + ";password=" + pwd + ";pooling=true;min pool size=1;max pool size=200;connect timeout = 60;";

            return connectionStr;
        }



        /// <summary> 
        /// AES加密 
        /// </summary>
        public string AESEncrypt(string value, string _aeskey = null)
        {
            if (string.IsNullOrEmpty(_aeskey))
            {
                _aeskey = AESKey;
            }

            byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(value);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary> 
        /// AES解密 
        /// </summary>
        public string AESDecrypt(string value, string _aeskey = null)
        {
            try
            {
                if (string.IsNullOrEmpty(_aeskey))
                {
                    _aeskey = AESKey;
                }
                byte[] keyArray = Encoding.UTF8.GetBytes(_aeskey);
                byte[] toEncryptArray = Convert.FromBase64String(value);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
