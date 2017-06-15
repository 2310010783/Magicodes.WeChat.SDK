using System;
using System.Xml.Serialization;

namespace Magicodes.WeChat.SDK.Core.ServerMessages.From
{
    /// <summary>
    ///     �����ı���Ϣ
    /// </summary>
    [XmlRoot("xml")]
    [Serializable]
    public class FromTextMessage : FromMessageBase
    {
        /// <summary>
        ///     �ı���Ϣ����
        /// </summary>
        public string Content { get; set; }
    }
}