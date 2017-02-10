using System;
using Nancy.Authentication.Stateless;
using Nancy.Security;
using Nancy;
using AuctionHouse.Models;

namespace AuctionHouse.Auth{    // http://thesenilecoder.blogspot.no/2016/11/nancyfx-stateless-auth-example.html
    public class StatelessAuthConfigurationFactory : IStatelessAuthConfigurationFactory
    {
        private readonly IUserMapper _userMapper;
        private IJwtKeyProvider _keyProvider;

        public StatelessAuthConfigurationFactory(IJwtKeyProvider keyProvider, IUserMapper userMapper)
        {
            _userMapper = userMapper;
            _keyProvider = keyProvider;
        }
        public StatelessAuthenticationConfiguration Create()
        {            return new StatelessAuthenticationConfiguration(VerifyToken);
        }
        private IUserIdentity VerifyToken(NancyContext ctx)
        {
            var jwtToken = ctx.Request.Headers.Authorization;
            if (ThereIsNoToken(jwtToken))                return null;

            // Decode encrypted Token
            var jwt = Jose.JWT.Decode<JwtToken>(jwtToken, _keyProvider.Key, Jose.JwsAlgorithm.HS256);
            if (TheTokenIsExpired(jwt))                return null;
            if (TheAudienceDoesNotMatch(_keyProvider, jwt))                return null;
            if (TheIssuerDoesNotMatch(_keyProvider, jwt))                return null;

            //if (TheUserIsNotValid(jwt))
            //    return null;

            return new User(jwt);
        }

        //private bool TheUserIsNotValid(JwtToken jwt)
        //{
        //    return !_userMapper.IsUserValid(jwt.Name);
        //}

        private static bool TheIssuerDoesNotMatch(IJwtKeyProvider provider, JwtToken jwt)        {            return !provider.Issuer.Equals(jwt.Issuer);        }
        private static bool TheAudienceDoesNotMatch(IJwtKeyProvider provider, JwtToken jwt)        {            return !provider.Audience.Equals(jwt.Audience);        }
        private static bool ThereIsNoToken(string encryptedToken)        {            return string.IsNullOrWhiteSpace(encryptedToken);        }
        private static bool TheTokenIsExpired(JwtToken jwt)        {            return jwt.Expiration < DateTime.UtcNow;        }    }}
