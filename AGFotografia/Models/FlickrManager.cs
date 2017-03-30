using FlickrNet;
using System;
using System.Configuration;
using System.Web;

namespace AGFotografia.Models
{
    public class FlickrManager
    {
        public static string ApiKey = ConfigurationManager.AppSettings["FlickrApiKey"];
        public static string SharedSecret = ConfigurationManager.AppSettings["FlickrSharedSecret"];
        //public const string ApiKey = "58c6a912a57adb58e8a0809e4ac15082";
        //public const string SharedSecret = "45b0004c58c60c7e";
        //TODO: pedir la llaves

        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        public static Flickr GetAuthInstance()
        {
            var f = new Flickr(ApiKey, SharedSecret);
            if (OAuthToken != null)
            {
                f.OAuthAccessToken = OAuthToken.Token;
                f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;
            }
            return f;
        }

        public static OAuthAccessToken OAuthToken
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["OAuthToken"] == null)
                {
                    return null;
                }
                var values = HttpContext.Current.Request.Cookies["OAuthToken"].Values;
                return new OAuthAccessToken
                {
                    FullName = values["FullName"],
                    Token = values["Token"],
                    TokenSecret = values["TokenSecret"],
                    UserId = values["UserId"],
                    Username = values["Username"]
                };
            }
            set
            {
                //TODO: ver el tema de cookies
                // Stores the authentication token in a cookie which will expire in 1 hour
                var cookie = new HttpCookie("OAuthToken")
                {
                    Expires = DateTime.UtcNow.AddHours(48), //dura 2 dias
                };
                cookie.Values["FullName"] = value.FullName;
                cookie.Values["Token"] = value.Token;
                cookie.Values["TokenSecret"] = value.TokenSecret;
                cookie.Values["UserId"] = value.UserId;
                cookie.Values["Username"] = value.Username;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
    }
}
