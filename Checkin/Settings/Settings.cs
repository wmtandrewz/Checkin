// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace Checkin
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings {
			get {
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string UsernameKey = "username_key";
		private static readonly string UsernameDefault = string.Empty;

		private const string PasswordKey = "password_key";
		private static readonly string PasswordDefault = string.Empty;

		private const string AccessTokenKey = "access_key";
		private static readonly string AccessTokenDefault = string.Empty;

		private const string ExpiresTimeKey = "expires_key";
		private static readonly string ExpiresTimeDefault = string.Empty;

		private const string HotelCodeKey = "hotel_code";
		private static readonly string HotelCodeDefault = string.Empty;

		private const string SAPURL = "SAPURL";
		private static readonly string SettingsSAPURLDefault = string.Empty;

		private const string SAPHanaURL = "SAPHanaURL";
		private static readonly string SettingsSAPHanaURLDefault = string.Empty;

		private const string SAPCookie = "SAPCookie";
		private static readonly string SettingsSAPCookieDefault = string.Empty;

		private const string User = "User";
		private static readonly string SettingsUser = string.Empty;




		#endregion

		public static string SettingsUserName
		{
			get
			{
				return AppSettings.GetValueOrDefault(User, SettingsUser);
			}
			set
			{
				AppSettings.AddOrUpdateValue(User, value);
			}
		}


		public static string SettingsSAPCookie
		{
			get
			{
				return AppSettings.GetValueOrDefault(SAPCookie, SettingsSAPCookieDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SAPCookie, value);
			}
		}

		public static string SettingsSAPHanaURL
		{
			get
			{
				return AppSettings.GetValueOrDefault(SAPHanaURL, SettingsSAPHanaURLDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SAPHanaURL, value);
			}
		}

		public static string SettingsSAPURL
		{
			get
			{
				return AppSettings.GetValueOrDefault(SAPURL, SettingsSAPURLDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SAPURL, value);
			}
		}
		public static string Username {
			get {
				return AppSettings.GetValueOrDefault(UsernameKey, UsernameDefault);
			}
			set {
				AppSettings.AddOrUpdateValue(UsernameKey, value);
			}
		}

		public static string Password {
			get {
				return AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault);
			}
			set {
				AppSettings.AddOrUpdateValue(PasswordKey, value);
			}
		}

		public static string AccessToken {
			get {
				return AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault);
			}
			set {
				AppSettings.AddOrUpdateValue(AccessTokenKey, value);
			}
		}

		public static string ExpiresTime {
			get {
				return AppSettings.GetValueOrDefault(ExpiresTimeKey, ExpiresTimeDefault);
			}
			set {
				AppSettings.AddOrUpdateValue(ExpiresTimeKey, value);
			}
		}
		public static string HotelCode
		{
			get
			{
				return AppSettings.GetValueOrDefault(HotelCodeKey, ExpiresTimeDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(HotelCodeKey, value);
			}
		}
	}
}