﻿// ======================================================================
//  
//          Copyright (C) 2016-2020 湖南心莱信息科技有限公司    
//          All rights reserved
//  
//          filename : SelfMenuInfo.cs
//          description :
//  
//          created by 李文强 at  2016/09/23 17:10
//          Blog：http://www.cnblogs.com/codelove/
//          GitHub ： https://github.com/xin-lai
//          Home：http://xin-lai.com
//  
// ======================================================================

using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Magicodes.WeChat.SDK.Apis.Menu
{
    /// <summary>
    ///     自定义菜单信息
    /// </summary>
    public class SelfMenuInfo
    {

        [JsonProperty("button")]
        public List<SelfMenuButtonBase> Buttons { get; set; }
    }
}