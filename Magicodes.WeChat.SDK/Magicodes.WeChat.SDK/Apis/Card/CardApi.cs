// ======================================================================
//  
//          Copyright (C) 2016-2020 ����������Ϣ�Ƽ����޹�˾    
//          All rights reserved
//  
//          filename : CardApi.cs
//          description :
//  
//          created by ����ǿ at  2016/10/13 17:10
//          Blog��http://www.cnblogs.com/codelove/
//          GitHub��https://github.com/xin-lai
//          Home��http://xin-lai.com
//  
// ======================================================================

using Magicodes.WeChat.SDK.Apis.Card.Request;
using Magicodes.WeChat.SDK.Apis.Card.Result;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Magicodes.WeChat.SDK.Apis.Card
{
    /// <summary>
    ///     ��ȯ�ӿ�
    /// </summary>
    public class CardApi : ApiBase
    {
        private const string ApiName = "card";

        #region ���ݿ�ȯJSON�ṹ�ַ�����ȡ��ȯ��Ϣ
        /// <summary>
        ///     ���ݿ�ȯJSON�ṹ�ַ�����ȡ��ȯ��Ϣ
        /// </summary>
        /// <returns></returns>
        public CardInfo GetCardInfoByJson(string cardInfoJson)
        {
            return JsonConvert.DeserializeObject<CardInfo>(cardInfoJson, new CardInfoCustomConverter(), new DateInfoCustomConverter());
        }
        #endregion

        #region ����

        #region ��ӿ���

        /// <summary>
        ///     ��ӿ���
        /// </summary>
        /// <param name="cardInfo">��ȯ�ṹ����</param>
        /// <returns></returns>
        public AddCardApiResult Add(CardInfo cardInfo)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("create", ApiName, "https://api.weixin.qq.com");
            var data = new
            {
                card = cardInfo
            };
            var result = Post<AddCardApiResult>(url, data);
            return result;
        }

        /// <summary>
        ///     ��ӿ���
        /// </summary>
        /// <param name="cardInfoJson">��ȯJSON�ṹ�ַ���</param>
        /// <returns></returns>
        public AddCardApiResult AddByJson(string cardInfoJson)
        {
            return Add(GetCardInfoByJson(cardInfoJson));
        }

        #endregion

        #region ������ȯ

        #region ���º���
        /// <summary>
        /// ��ѯCode�ӿ�
        /// 1.�̶�ʱ����Ч�ڻ�����û�ʵ����ȡʱ��ת�������û�2013��10��1����ȡ���̶�ʱ����Ч��Ϊ90�죬����Чʱ��Ϊ2013��10��1��-12��29����Ч��
        /// 2.����check_consume��д����true����false,��codeδ����ӻ���code��ת����ȡ��ͳһ����invalid serial code
        /// </summary>
        /// <param name="cardId">��ȯID����һ�࿨ȯ���Զ���code��ȯ���</param>
        /// <param name="code">���ſ�ȯ��Ψһ��׼��</param>
        /// <param name="checkConsume">�Ƿ�У��code����״̬������true��falseʱ��code�쳣״̬�������ݲ�ͬ��</param>
        /// <returns></returns>
        public CodeStatusResult GetCodeStatus(string cardId, string code, bool checkConsume)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("get", "card/code", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
                code = code,
                check_consume = checkConsume
            };
            var result = Post<CodeStatusResult>(url, data);
            return result;
        }
        /// <summary>
        /// ��������
        /// ע��
        /// 1.��֧�ֺ�����Ч״̬�Ŀ�ȯ������ȯ�����쳣״̬�������ɺ��������쳣״̬��������ȯɾ����δ��Ч�����ڡ�ת���С�ת���˻ء�ʧЧ��
        /// 2.�Զ���Code�루use_custom_codeΪtrue�����Ż�ȯ����code������ʱ��������ô˽ӿڡ����ڽ��û��ͻ��˵�code״̬������Զ���code�Ŀ�ȯ���ýӿ�ʱ�� post�����������card_id������invalid serial code�����Զ���code�����ϱ���
        /// </summary>
        /// <returns></returns>
        public ConsumeCardResult ConsumeCard(string code, string cardId)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("consume", "card/code", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
                code = code
            };
            var result = Post<ConsumeCardResult>(url, data);
            return result;
        }

        #endregion


        #endregion

        /// <summary>
        /// ������ѯ��ȯ�б�
        /// </summary>
        /// <param name="statusList">��ȯ״̬�б�</param>
        /// <param name="offSet">��ѯ���б����ʼƫ��������0��ʼ����offset: 5��ָ�Ӵ��б���ĵ�������ʼ��ȡ��</param>
        /// <param name="count">��Ҫ��ѯ�Ŀ�Ƭ���������������50����</param>
        /// <returns></returns>
        public GetBatchCardListResult GetBatchCardList(List<string> statusList = null, int offSet = 0, int count = 50)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("batchget", ApiName, "https://api.weixin.qq.com");
            var data = new
            {
                offset = offSet,
                count = count,
                status_list = statusList
            };
            var result = Post<GetBatchCardListResult>(url, data);
            return result;
        }

        /// <summary>
        /// �鿴��ȯ����
        /// �����߿��Ե��øýӿڲ�ѯĳ��card_id�Ĵ�����Ϣ�����״̬�Լ����������
        /// </summary>
        /// <returns></returns>
        public CardDetailResult GetCardDetail(string card_id)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("get", ApiName, "https://api.weixin.qq.com");
            var data = new
            {
                card_id = card_id
            };
            var result = Post<CardDetailResult>(url, data);
            return result;
        }
        /// <summary>
        ///  Code����ӿ�
        /// </summary>
        /// <param name="encrypt_code">�������ܵ�Code�롣</param>
        /// <returns></returns>
        public DecrCodeApiResult DecryptCode(string encrypt_code)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("code/decrypt", ApiName, "https://api.weixin.qq.com");
            var data = new
            {
                encrypt_code = encrypt_code
            };
            var result = Post<DecrCodeApiResult>(url, data);
            return result;
        }

        #endregion

        #region �ϴ�ͼƬ
        /// <summary>
        ///     �ϴ���ȯͼƬ
        /// </summary>
        /// <param name="fileName">�ļ���</param>
        /// <param name="fileStream">�ļ���</param>
        /// <returns></returns>
        public UploadImageApiResult UploadImage(string fileName, Stream fileStream)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("uploadimg", "media");
            return Post<UploadImageApiResult>(url, fileName, fileStream);
        }
        #endregion

        #region ������Ա��

        #region �����Ա��
        /// <summary>
        /// �ӿڼ����Ա��
        /// ���ʽ˵��
        /// �ӿڼ���ͨ����Ҫ�����߿����û���д���ϵ���ҳ��ͨ�������ּ������̣�
        /// 1. �û���������д���Ϻ�����쿨���쿨�󿪷��ߵ��ü���ӿ�Ϊ�û������Ա����
        /// 2. ���û���������ȡ��Ա������������Ա����ת�����������õ�������дҳ�棬��д��ɺ󿪷��ߵ��ü���ӿ�Ϊ�û������Ա����
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResult AcitveMember(ActivateMemberCardRequest model)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("activate", "card/membercard", "https://api.weixin.qq.com");
            var result = Post<ApiResult>(url, model);
            return result;
        }

        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ�����Ա����Ϣ
        /// </summary>
        /// <param name="activateInfo"></param>
        /// <returns></returns>
        public ActivateMemberCardRequest GetActivateMemberCardByJson(string activateInfo)
        {
            return JsonConvert.DeserializeObject<ActivateMemberCardRequest>(activateInfo);
        }

        /// <summary>
        /// ��ͨһ�������Ա��
        /// ����һ���ڴ����ӿ�����wx_activate�ֶ�
        /// ����������ÿ����ֶνӿ�
        /// �����������ջ�Ա��Ϣ�¼�֪ͨ
        /// �����ģ�ͬ����Ա����
        /// �����壺��ȡ��Ա��Ϣ�ӿ�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ApiResult ActivateMemberUserform(ActivateMemberCardRequest model)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("activateuserform", "card/membercard", "https://api.weixin.qq.com");
            var result = Post<ApiResult>(url, model);
            return result;
        }

        /// <summary>
        /// ��ת��һ������֧���û����ύ��Ա�������Ϻ���ת���̻��Զ������ҳ��
        /// ��ͬ����ͨһ�������ת��һ������ļ����Ա���������̻���ɣ��̻���������ת������ҳ�����������������������жϵ��߼���ͬʱҲ��֤�˿�����ʵʱ�ԣ��ʺ�ʹ���Զ��忨�ŵ��̻�ʹ�á�
        /// ����һ���ڴ���/���½ӿ�������ת��һ����������ֶ�
        /// ����������ÿ����ֶνӿ�
        /// ����������ȡ�û��ύ����
        /// �����ģ����ýӿڼ����Ա��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActivateMemberTempInfoResult Activatetempinfo(ActivateMemberTempInfoRequest model)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("activatetempinfo", "card/membercard", "https://api.weixin.qq.com");
            var result = Post<ActivateMemberTempInfoResult>(url, model);
            return result;
        }
        /// <summary>
        /// ����Json�ṹ�ַ�����ȡת��һ������
        /// </summary>
        /// <param name="activateInfo"></param>
        /// <returns></returns>
        public ActivateMemberTempInfoRequest GetActivateMemberTempInfoByJson(string activateMmberInfo)
        {
            return JsonConvert.DeserializeObject<ActivateMemberTempInfoRequest>(activateMmberInfo);
        }

        /// <summary>
        /// ���ÿ����ֶνӿ�
        /// �������ڴ���ʱ����wx_activate�ֶκ���Ҫ���øýӿ������û�����ʱ��Ҫ��д��ѡ�����һ���������ò���Ч��
        /// </summary>
        /// <returns></returns>
        public ApiResult ActivateUserForm(ActivateMemberUserformRequest setActivateInfo)
        {
            var url = GetAccessApiUrl("activateuserform/set", "card/membercard", "https://api.weixin.qq.com");
            var result = Post<ApiResult>(url, setActivateInfo);
            return result;
        }
        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ���ÿ����ֶ���Ϣ
        /// </summary>
        public ActivateMemberUserformRequest GetActivateUserFormInfoByJson(string userFormInfo)
        {
            return JsonConvert.DeserializeObject<ActivateMemberUserformRequest>(userFormInfo);
        }


        #endregion

        #region ��ȡ��Ա��Ϣ�����ֲ�ѯ���ӿ�
        /// <summary>
        /// ��ȡ��Ա��Ϣ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public GetUserInfoResult GetUserInfo(GetUserInfoRequest model)
        {
            var url = GetAccessApiUrl("userinfo/get", "card/membercard", "https://api.weixin.qq.com");
            var result = Post<GetUserInfoResult>(url, model);
            return result;
        }

        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ��Ա��Ϣ�����ֲ�ѯ���ӿ�
        /// </summary>
        public GetUserInfoRequest GetUserInfoByJson(string userinfo)
        {
            return JsonConvert.DeserializeObject<GetUserInfoRequest>(userinfo);
        }
        #endregion

        #region ���»�Ա��Ϣ
        /// <summary>
        ///  ����Ա�ֿ����Ѻ�֧�ֿ����ߵ��øýӿڸ��»�Ա��Ϣ����Ա�����׺��ÿ����Ϣ�����ͨ���ýӿ�֪ͨ΢�ţ����ں�����Ϣ֪ͨ��������չ���ܡ�
        ///  ֵ��ע����ǣ����������������ʵʱͬ�����֡������΢�Ŷˣ�����ǿ�ҽ��鿪���߿�����ÿ��Ĺ̶�ʱ��������֣�һ�첻�������Ρ�������Ļ���ֵ��֮ǰ�ޱ仯ʱ�������bonus=ԭ����bonus���������л��ֱ䶯֪ͨ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UpdateUserResult UpdateUser(UpdateUserRequest model)
        {
            //��ȡapi����url
            var url = GetAccessApiUrl("updateuser", "card/membercard", "https://api.weixin.qq.com");
            var result = Post<UpdateUserResult>(url, model);
            return result;
        }
        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ���»�Ա��Ϣ
        /// </summary>
        public UpdateUserRequest GetUpdateUserInfoByJson(string updateuserinfo)
        {
            return JsonConvert.DeserializeObject<UpdateUserRequest>(updateuserinfo);
        }
        #endregion

        #region ���»�Ա����Ϣ
        public ApiResult UpdateMemberCard(UpdateCardRequest model)
        {
            var url = GetAccessApiUrl("update", ApiName, "https://api.weixin.qq.com");
            var result = Post<ApiResult>(url, model);
            return result;

        }
        /// <summary>
        /// ���ݿ�ȯJSON�ṹ�ַ�����ȡ���»�Ա����Ϣ
        /// </summary>
        /// <returns></returns>
        public UpdateCardRequest GetUpdateMemnberInfoByJson(string updateJson)
        {
            return JsonConvert.DeserializeObject<UpdateCardRequest>(updateJson, new DateInfoCustomConverter());
        }
        #endregion

        #region �Զ���Code
        /// <summary>
        /// �����Զ���CODE
        /// 1�����ε��ýӿڴ���code����������Ϊ100����
        /// 2��ÿһ�� code ������Ϊ�մ���
        /// 3�����������ϵͳ���Զ��ж��ṩ�����ÿ����ʵ�ʵ���code�����Ƿ�һ�¡�
        /// 4������ʧ��֧���ظ����룬��ʾ�ɹ�Ϊֹ��
        /// </summary>
        /// <param name="cardId">�������</param>
        /// <param name="codeList">�Զ���Code�б�</param>
        /// <returns></returns>
        public DepositCustomCodeResult DepositCustomCode(string cardId, List<string> codeList)
        {
            var url = GetAccessApiUrl("deposit", "card/code", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
                code = codeList
            };
            var result = Post<DepositCustomCodeResult>(url, data);
            return result;
        }

        /// <summary>
        /// ��ѯ����code��Ŀ�ӿ�,��ѯcode����΢�ź�̨�ɹ�����Ŀ��
        /// </summary>
        /// <returns></returns>
        public GetDepositCountResult GetDepositCount(string cardId)
        {
            var url = GetAccessApiUrl("getdepositcount", "card/code", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
            };
            var result = Post<GetDepositCountResult>(url, data);
            return result;
        }

        /// <summary>
        /// Ϊ�˱�����ֵ�����ǿ�ҽ��鿪�����ڲ�ѯ��code��Ŀ��ʱ��˲�code�ӿ�У��code����΢�ź�̨�������
        /// </summary>
        /// <param name="cardId">���е���code�Ŀ�ȯID</param>
        /// <param name="codeList">�Ѿ�����΢�ſ�ȯ��̨���Զ���code������Ϊ100����</param>
        /// <returns></returns>
        public CheckCodeResult CheckCode(string cardId, List<string> codeList)
        {
            var url = GetAccessApiUrl("checkcode", "card/code", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
                code = codeList
            };
            var result = Post<CheckCodeResult>(url, data);
            return result;
        }

        #endregion

        #endregion

        #region ��Ա������
        /// <summary>
        /// �޸Ŀ�ȯ���
        /// </summary>
        /// <returns></returns>
        public ApiResult ModifyStock(ModifyStockRequest model)
        {
            var url = GetAccessApiUrl("modifystock", ApiName, "https://api.weixin.qq.com");
            var result = Post<ApiResult>(url, model);
            return result;
        }

        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ�޸Ŀ�ȯ���
        /// </summary>
        public GetUserInfoRequest GetModifyStockInfoByJson(string modifystock)
        {
            return JsonConvert.DeserializeObject<GetUserInfoRequest>(modifystock);
        }

        /// <summary>
        /// ɾ����ȯ
        /// </summary>
        public ApiResult Delete(string cardId)
        {
            var url = GetAccessApiUrl("delete", ApiName, "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId
            };
            var result = Post<ApiResult>(url, data);
            return result;
        }

        /// <summary>
        /// Ϊȷ��ת����İ�ȫ�ԣ�΢�������Զ���Code���̻������·���code���и��ġ�
        /// ע��Ϊ�����û��ɻ󣬽�����ڷ���ת����Ϊ�󣨷���ת����΢�Ż�ͨ���¼����͵ķ�ʽ��֪�̻���ת���Ŀ�ȯCode�����û���Code���и��ġ�
        /// </summary>
        /// <param name="cardId">��ȯID���Զ���Code�뿨ȯΪ���</param>
        /// <param name="code">������Code�롣</param>
        /// <param name="newCode">��������ЧCode�롣</param>
        /// <returns></returns>
        public ApiResult UpdateCode(string cardId, string code, string newCode)
        {
            var url = GetAccessApiUrl("update", "card/code", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
                code = code,
                new_code = newCode
            };
            var result = Post<ApiResult>(url, data);
            return result;
        }


        /// <summary>
        /// ���ÿ�ȯʧЧ�ӿ�
        /// Ϊ�����Ʊ���˿���쳣������ɵ��ÿ�ȯʧЧ�ӿڽ��û��Ŀ�ȯ����ΪʧЧ״̬�� 
        /// 1.���ÿ�ȯʧЧ�Ĳ��������棬���޷�������ΪʧЧ�Ŀ�ȯ������Ч״̬���̼������ص��øýӿڡ�
        /// 2.�̻�����ʧЧ�ӿ�ǰ����˿����ȸ�֪��ȡ��ͬ�⣬������˴����Ĺ˿�Ͷ�ߣ�΢�Ž��ᰴ�ա�΢����Ӫ�������򡷽��д�����
        /// </summary>
        /// <param name="cardId">��Ա�����룬���Զ���Code���Բ���</param>
        /// <param name="code">��Ա��Code</param>
        /// <returns></returns>
        public ApiResult Unavailable(string code, string cardId)
        {
            var url = GetAccessApiUrl("unavailable", "card/code", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
                code = code
            };
            var result = Post<ApiResult>(url, data);
            return result;
        }
        /// <summary>
        /// ��ȡ���Ż�Ա�����ݽӿ�
        /// ��ȡAPI�����Ļ�Ա���������
        /// </summary>
        /// <param name="begin_date">��ѯ���ݵ���ʼʱ�䣬��ʽ��2015-06-15</param>
        /// <param name="end_date">��ѯ���ݵĽ���ʱ�䣬��ʽ��2015-07-15</param>
        /// <param name="card_id">��ȯid</param>
        /// <returns></returns>
        public CardBizuinInfoResult GetCardBizuinInfoByCardId(string begin_date, string end_date, string cardId)
        {
            var url = GetAccessApiUrl("getcardmembercarddetail", "datacube", "https://api.weixin.qq.com");
            var data = new
            {
                begin_date = begin_date,
                end_date = end_date,
                card_id = cardId
            };
            var result = Post<CardBizuinInfoResult>(url, data);
            return result;
        }


        #endregion

        #region Ͷ�ſ�ȯ
        /// <summary>
        /// ������ά��ӿ�
        /// </summary>
        public CreateQRCodeResult CreateQRCode(CreareQRCodeRequest model)
        {
            var url = GetAccessApiUrl("create", "card/qrcode", "https://api.weixin.qq.com");
            var result = Post<CreateQRCodeResult>(url, model);
            return result;
        }
        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ������ά����Ϣ
        /// </summary>
        public CreareQRCodeRequest GetCreateQRCodeInfoByJson(string createqrcode)
        {
            return JsonConvert.DeserializeObject<CreareQRCodeRequest>(createqrcode);
        }
        /// <summary>
        /// ��ȯ����֧�ֿ�����ͨ�����ýӿ�����һ����ȯ��ȡH5ҳ�棬����ȡҳ�����ӣ����п�ȯͶ�Ŷ�����
        /// Ŀǰ��ȯ���ܽ�֧�ַ��Զ���code�Ŀ�ȯ���Զ���code�Ŀ�ȯ���ȵ��õ���code�ӿڽ�code�����������ʹ�á�
        /// ��������ʱ����дͶ��·���ĳ����ֶ�
        /// </summary>
        public CreateLandingPageResult CreateLandingPage(CreateLandingPageRequest model)
        {
            var url = GetAccessApiUrl("create", "card/landingpage", "https://api.weixin.qq.com");
            var result = Post<CreateLandingPageResult>(url, model);
            return result;
        }
        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ������ά����Ϣ
        /// </summary>
        public CreateLandingPageResult GetLandingPageInfoByJson(string landingPageInfo)
        {
            return JsonConvert.DeserializeObject<CreateLandingPageResult>(landingPageInfo);
        }
        #endregion

        #region ֧������Ա
        /// <summary>
        /// ��ѯĳ���̻����Ƿ�֧��֧������Ա����
        /// </summary>
        public GetPayGiftMemberResult GetPayGiftMember(string mchId)
        {
            var url = GetAccessApiUrl("get", "card/paygiftmembercard", "https://api.weixin.qq.com");
            var data = new
            {
                mchid = mchId
            };
            var result = Post<GetPayGiftMemberResult>(url, data);
            return result;
        }
        /// <summary>
        /// ֧���̻�����֧������Ա�Ĺ��򣬿�������ʱ��κͽ�����䷢��Ա����
        /// </summary>
        public AddPayGiftMemberResult AddPayGiftMember(AddPayGiftMemberRequest model)
        {
            var url = GetAccessApiUrl("add", "card/paygiftmembercard", "https://api.weixin.qq.com");
            var result = Post<AddPayGiftMemberResult>(url, model);
            return result;
        }

        /// <summary>
        /// ����Json�ṹ�ַ�����ȡ����֧������Ա�Ĺ���
        /// </summary>
        public AddPayGiftMemberResult GetPayGiftMemberByJson(string payGiftMemberInfo)
        {
            return JsonConvert.DeserializeObject<AddPayGiftMemberResult>(payGiftMemberInfo);
        }
        /// <summary>
        /// ɾ��֮ǰ�Ѿ����õ�֧������Ա����
        /// </summary>
        public AddPayGiftMemberResult DeletePayGiftMember(string cardId, List<string> mchidList)
        {
            var url = GetAccessApiUrl("delete", "card/paygiftmembercard", "https://api.weixin.qq.com");
            var data = new
            {
                card_id = cardId,
                mchid_list = mchidList
            };
            var result = Post<AddPayGiftMemberResult>(url, data);
            return result;
        }
        #endregion
    }
}