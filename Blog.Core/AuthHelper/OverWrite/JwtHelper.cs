using Blog.Core.Common.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Blog.Core.AuthHelper.OverWrite
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发token
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            // 自己封装的 appsettign.json 操作类，看下文
            string iss = Appsettings.app(new string[] { "Audience", "Issuer" }); //iss: 签发人
            string aud = Appsettings.app(new string[] { "Audience", "Audience" });//aud: 受众
            string secret = Appsettings.app(new string[] { "Audience", "Secret" });

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.Uid.ToString()),//jti: 编号
                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()}"),//Iat 签发时间
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()}"),//nbf: 生效时间
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeMilliseconds()}"),//exp: 过期时间
                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud)
            };
            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(tokenModel.Role.Split(',').Select(e=>new Claim(ClaimTypes.Role,e)));
            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(iss, aud, claims, signingCredentials:creds);
            return  new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// 解析token
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler=new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                Uid = (jwtToken.Id).ObjToInt(),
                Role = role != null ? role.ObjToString() : "",
            };
            return tm;
        }
    }

    public class TokenModelJwt
    {
        /// <summary>
        /// id
        /// </summary>
        public  long Uid { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }
    }
}
