namespace Magicodes.WeChat.SDK.Core.ServerMessages.From
{
    /// <summary>
    ///     ������Ϣ
    /// </summary>
    public class FromLinkMessage : FromMessageBase
    {
        /// <summary>
        ///     ��Ϣ����
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     ��Ϣ����
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     ��Ϣ����
        /// </summary>
        public string Url { get; set; }
    }
}