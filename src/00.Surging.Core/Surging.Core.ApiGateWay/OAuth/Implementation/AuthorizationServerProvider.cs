﻿using Newtonsoft.Json;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Routing;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Surging.Core.Caching;
using System.Text.RegularExpressions;
using Surging.Core.CPlatform.Cache;
using Newtonsoft.Json.Linq;

namespace Surging.Core.ApiGateWay.OAuth
{
    /// <summary>
    /// 授权服务提供者
    /// </summary>
    public class AuthorizationServerProvider: IAuthorizationServerProvider
    {
        private readonly IServiceProxyProvider _serviceProxyProvider;
        private readonly IServiceRouteProvider _serviceRouteProvider;
        private readonly CPlatformContainer _serviceProvider;
        private readonly ICacheProvider _cacheProvider;
        public AuthorizationServerProvider(IServiceProxyProvider serviceProxyProvider
           ,IServiceRouteProvider serviceRouteProvider
            , CPlatformContainer serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceProxyProvider = serviceProxyProvider;
            _serviceRouteProvider = serviceRouteProvider;
            _cacheProvider = CacheContainer.GetService<ICacheProvider>(AppConfig.CacheMode);
        }

        public async Task<OAuthUser> GenerateTokenCredential(Dictionary<string, object> parameters)
        {
            OAuthUser oAuthUser = new OAuthUser() { IsSucceed = false };

            string result = null;
            var payload = await _serviceProxyProvider.Invoke<object>(parameters, AppConfig.AuthorizationRoutePath, AppConfig.AuthorizationServiceKey);
            IList<string> routePaths = null;
            if (payload != null && !payload.Equals("null"))
            {
                oAuthUser = JsonConvert.DeserializeObject<OAuthUser>(payload.ToString());
                if (oAuthUser.IsSucceed)
                {
                    var jObject = JObject.Parse(payload.ToString());
                    jObject["RoutePaths"] = null;
                    var jsonPayload = JsonConvert.SerializeObject(oAuthUser);
                   var jwtHeader = JsonConvert.SerializeObject(new JWTSecureDataHeader() { TimeStamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") });
                    var base64Payload = ConverBase64String(JsonConvert.SerializeObject(jObject));
                    var encodedString = $"{ConverBase64String(jwtHeader)}.{base64Payload}";
                    var route = await _serviceRouteProvider.GetRouteByPath(AppConfig.AuthorizationRoutePath);
                    var signature = HMACSHA256(encodedString, route.ServiceDescriptor.Token);
                    result = $"{encodedString}.{signature}";
                    oAuthUser.AccessToken = result;
                    oAuthUser.ExpireTime = AppConfig.AccessTokenExpireTimeSpan.TotalSeconds;
                    _cacheProvider.Add(base64Payload, result, AppConfig.AccessTokenExpireTimeSpan);
                    _cacheProvider.Add(result, oAuthUser.RoutePaths, AppConfig.AccessTokenExpireTimeSpan);
                }
            }
            return oAuthUser;
        }
        public async Task<OAuthUser> GenerateAppSecretTokenCredential(Dictionary<string, object> parameters)
        {
            OAuthUser oAuthUser = new OAuthUser() { IsSucceed = false };

            string result = null;
            var payload = await _serviceProxyProvider.Invoke<object>(parameters, AppConfig.AppSecretRoutePath,
                AppConfig.AppSecretServiceKey);

            if (payload != null && !payload.Equals("null"))
            {
                oAuthUser = JsonConvert.DeserializeObject<OAuthUser>(payload.ToString());
                if (oAuthUser.IsSucceed)
                {
                    var jwtHeader = JsonConvert.SerializeObject(new JWTSecureDataHeader() { TimeStamp = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") });
                    var base64Payload = ConverBase64String(JsonConvert.SerializeObject(payload));
                    var encodedString = $"{ConverBase64String(jwtHeader)}.{base64Payload}";
                    var route = await _serviceRouteProvider.GetRouteByPath(AppConfig.AppSecretRoutePath);
                    var signature = HMACSHA256(encodedString, route.ServiceDescriptor.Token);
                    result = $"{encodedString}.{signature}";
                    oAuthUser.AccessToken = result;
                    oAuthUser.ExpireTime = AppConfig.AppSercretTokenExpireTimeSpan.TotalSeconds;
                    _cacheProvider.Add(base64Payload, result, AppConfig.AppSercretTokenExpireTimeSpan);
                }
            }
            return oAuthUser;
        }
        public string GetPayloadString(string token)
        {
            string  result = null;
            var jwtToken = token.Split('.');
            if (jwtToken.Length == 3)
            {

                result =  Encoding.UTF8.GetString(Convert.FromBase64String(jwtToken[1]));
            }
            return result;
        }

        public async Task<bool> ValidateClientAuthentication(string token)
        {
            bool isSuccess = false;
            var jwtToken = token.Split('.');
            if (jwtToken.Length == 3)
            {
                isSuccess = await _cacheProvider.GetAsync<string>(jwtToken[1]) == token;
            }
            return isSuccess;
        }

        public async Task<bool> RefreshToken(string token)
        {
            bool isSuccess = false;
            var jwtToken = token.Split('.');
            if (jwtToken.Length == 3)
            {
                var  value = await _cacheProvider.GetAsync<string>(jwtToken[1]);
                if (!string.IsNullOrEmpty(value))
                {
                    _cacheProvider.Add(jwtToken[1], value, AppConfig.AccessTokenExpireTimeSpan);
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        private string ConverBase64String(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        private string HMACSHA256(string message, string secret)
        {
            secret = secret ?? ""; 
            byte[] keyByte = Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
    }
}