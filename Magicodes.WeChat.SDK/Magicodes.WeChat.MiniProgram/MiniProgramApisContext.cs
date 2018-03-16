﻿// ======================================================================
//  
//          Copyright (C) 2016-2020 湖南心莱信息科技有限公司    
//          All rights reserved
//  
//          filename : WeChatApisContext.cs
//          description :
//  
//          created by 李文强 at  2018/03/23 17:10
//          Blog：http://www.cnblogs.com/codelove/
//          GitHub ： https://github.com/xin-lai
//          Home：http://xin-lai.com
//  
// ======================================================================

using System;
using Magicodes.WeChat.MiniProgram.Apis.Token;

namespace Magicodes.WeChat.MiniProgram
{
    /// <summary>
    ///     接口上下文对象
    /// </summary>
    public class MiniProgramApisContext
    {
        private static readonly Lazy<MiniProgramApisContext> Lazy =
            new Lazy<MiniProgramApisContext>(() => new MiniProgramApisContext());

        /// <summary>
        ///     用户组相关操作API
        /// </summary>
        public TokenApi TokenApi = new TokenApi();


        public static MiniProgramApisContext Current => Lazy.Value;
    }
}