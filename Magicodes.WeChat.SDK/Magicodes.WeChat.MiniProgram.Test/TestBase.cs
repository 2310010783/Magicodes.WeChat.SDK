﻿using System;
using Magicodes.Pay.WeChat.Builder;
using Magicodes.WeChat.MiniProgram.Builder;
using Magicodes.WeChat.MiniProgram.Test.Helper;

namespace Magicodes.WeChat.MiniProgram.Test
{
    public class TestBase
    {
        static TestBase()
        {
            MiniProgramSDKBuilder.Create()
            //设置日志记录
            .WithLoggerAction((tag, message) =>
            {
                Console.WriteLine(string.Format("Tag:{0}\tMessage:{1}", tag, message));
            }).RegisterGetKeyFunc(() =>
            {
                //推荐使用APPID作为Key
                return "1";
            }).RegisterGetConfigByKeyFunc((key) =>
            {
                //如果是一个项目多个配置,请使用key来获取相关配置
                return ConfigHelper.LoadConfig();
            }).Build();

            WeChatPayBuilder.Create()
            //设置日志记录
            .WithLoggerAction((tag, message) =>
            {
                Console.WriteLine(string.Format("Tag:{0}\tMessage:{1}", tag, message));
            }).RegisterGetPayConfigFunc(() =>
            {
                //如果是一个项目多个配置,请使用key来获取相关配置
                return ConfigHelper.LoadConfig();
            }).Build();
        }
    }
}
