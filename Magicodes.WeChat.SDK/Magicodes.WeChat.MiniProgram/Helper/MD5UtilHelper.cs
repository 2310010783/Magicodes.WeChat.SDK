// ======================================================================
//  
//          Copyright (C) 2016-2020 ����������Ϣ�Ƽ����޹�˾    
//          All rights reserved
//  
//          filename : MD5UtilHelper.cs
//          description :
//  
//          created by ����ǿ at  2018/04/10 17:10
//          Blog��http://www.cnblogs.com/codelove/
//          GitHub �� https://github.com/xin-lai
//          Home��http://xin-lai.com
//  
// ======================================================================

using System;
using System.Security.Cryptography;
using System.Text;

namespace Magicodes.WeChat.MiniProgram.Helper
{
    /// <summary>
    ///     MD5UtilHelper ��ժҪ˵����
    /// </summary>
    public class EncryUtilHelper
    {
        /// <summary>
        ///     ��ȡ��д��MD5ǩ�����
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            var m5 = new MD5CryptoServiceProvider();

            //����md5����
            byte[] inputBye;
            byte[] outputBye;

            //ʹ��GB2312���뷽ʽ���ַ���ת��Ϊ�ֽ����飮
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        /// <summary>
        /// ����΢��С����ƽ̨�ṩ�Ľ����㷨��������  
        /// </summary>
        /// <param name="encryptedData"></param>
        /// <param name="iv"></param>
        /// <param name="sessionKey"></param>
        /// <returns></returns>
        public static string MiniProgramDataDecrypt(string encryptedData, string iv, string sessionKey)
        {
            //�������������ɹ���ʵ��  
            var aes = new AesCryptoServiceProvider
            {
                //���ý���������  
                Mode = CipherMode.CBC,
                BlockSize = 128,
                Padding = PaddingMode.PKCS7
            };
            //��ʽ���������ַ���  
            var byte_encryptedData = Convert.FromBase64String(encryptedData);
            var byte_iv = Convert.FromBase64String(iv);
            var byte_sessionKey = Convert.FromBase64String(sessionKey);

            aes.IV = byte_iv;
            aes.Key = byte_sessionKey;
            //�������úõ��������ɽ�����ʵ��  
            using (var transform = aes.CreateDecryptor())
            {
                //����  
                var final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);

                //��ȡ���  
                return Encoding.UTF8.GetString(final);
            }
        }
    }
}