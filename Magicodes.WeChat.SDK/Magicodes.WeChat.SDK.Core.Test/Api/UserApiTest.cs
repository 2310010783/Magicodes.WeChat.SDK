﻿// ======================================================================
//  
//          Copyright (C) 2016-2020 湖南心莱信息科技有限公司    
//          All rights reserved
//  
//          filename : UserApiTest.cs
//          description :
//  
//          created by 李文强 at  2016/09/23 17:10
//          Blog：http://www.cnblogs.com/codelove/
//          GitHub ： https://github.com/xin-lai
//          Home：http://xin-lai.com
//  
// ======================================================================

using System;
using Magicodes.WeChat.SDK.Apis.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Magicodes.WeChat.SDK.Test.Api
{
    /// <summary>
    ///     用户组API测试
    /// </summary>
    [TestClass]
    public class UserApiTest : ApiTestBase
    {
        private readonly UserApi api = WeChatApisContext.Current.UserApi;

        [TestMethod]
        public void UserApiTest_ALL()
        {
            #region 获取帐号的关注者列表
            var openIdsResult = api.GetOpenIdList();
            if (!openIdsResult.IsSuccess())
                Assert.Fail("获取帐号的关注者列表失败，返回结果如下：" + openIdsResult.DetailResult);
            #endregion

            //获取测试用户
            var testUsersOpenIds = openIdsResult.Data.OpenIds.ToList();
            if (testUsersOpenIds.Count == 0)
                Assert.Fail("测试失败，没有可用于测试的用户！");

            #region 添加备注

            foreach (var item in testUsersOpenIds)
            {
                var result = api.SetRemark(item, "Test");
                if (!result.IsSuccess())
                    Assert.Fail("更新备注信息失败，返回结果如下：" + result.DetailResult);
            }

            #endregion

            #region 获取用户信息

            foreach (var item in testUsersOpenIds)
            {
                var result = api.Get(item);
                if (!result.IsSuccess())
                    Assert.Fail("获取用户信息失败，返回结果如下：" + result.DetailResult);
                if (result.SubscribeTime == default(DateTime))
                    Assert.Fail("获取用户关注时间失败，返回结果如下：" + result.DetailResult);
            }

            #endregion

            #region 批量获取

            var batchResult = api.Get(testUsersOpenIds.ToArray());
            if (!batchResult.IsSuccess())
                Assert.Fail("批量获取用户失败，返回结果如下：" + batchResult.DetailResult);
            foreach (var item in batchResult.UserInfoList)
                if (item.SubscribeTime == default(DateTime))
                    Assert.Fail("获取用户关注时间失败，返回结果如下：" + batchResult.DetailResult);

            #endregion;

            
        }
    }
}