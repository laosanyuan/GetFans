﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LoginParameter
    {
        public readonly string PUBKEY = "EB2A38568661887FA180BDDB5CABD5F21C7BFD59C090CB2D245A87AC253062882729293E5506350508E7F9AA3BB77F4333231490F915F6D63C55FE2F08A49B353F444AD3993CACC02DB784ABBB8E42A9B1BBFFFB38BE18D78E87A0E41B9B8F73A928EE0CCEE1F6739884B9777E4FE9E88A1BBE495927AC4A799B3181D6442443";
        public readonly string RSAKV = "1330428213";

        public string su; //加密后的用户名
        public string servertime; //预登录参数1（时间戳）
        public string pcid; //预登录参数2
        public string nonce; //预登录参数3（随机数）
        public string showpin; //预登录参数4（是否需要验证码）

        public bool IsForcedPin;//强制验证码
    }
}
