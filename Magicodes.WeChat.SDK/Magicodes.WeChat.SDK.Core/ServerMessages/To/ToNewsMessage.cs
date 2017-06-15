using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Magicodes.WeChat.SDK.Core.ServerMessages.To
{
    /// <summary>
    ///     �ظ�ͼ����Ϣ
    /// </summary>
    [XmlRoot(ElementName = "xml")]
    public class ToNewsMessage : ToMessageBase
    {
        public ToNewsMessage()
        {
            Type = ToMessageTypes.news;
            Articles = new List<ArticleInfo>();
        }

        /// <summary>
        ///     ͼ����Ϣ����������Ϊ10������
        /// </summary>
        [XmlElement("ArticleCount")]
        public int ArticleCount { get; set; }

        /// <summary>
        ///     ����ͼ����Ϣ��Ϣ��Ĭ�ϵ�һ��itemΪ��ͼ,ע�⣬���ͼ��������10���򽫻�����Ӧ
        /// </summary>
        [XmlArrayItem("Item")]
        public List<ArticleInfo> Articles { get; set; }

        [Serializable]
        public class ArticleInfo
        {
            /// <summary>
            ///     ͼ����Ϣ����
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            ///     ͼ����Ϣ����
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            ///     ͼƬ���ӣ�֧��JPG��PNG��ʽ���Ϻõ�Ч��Ϊ��ͼ360*200��Сͼ200*200
            /// </summary>
            public string PicUrl { get; set; }

            /// <summary>
            ///     ���ͼ����Ϣ��ת����
            /// </summary>
            public string Url { get; set; }
        }
    }
}