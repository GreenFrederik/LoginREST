using System.Security.Cryptography;
using System.Text;
using System;

[Serializable]
public class User
{
	public string Name { get; set; }
	public string PasswordHash { get; set; }

	public User(){}
	
	public User(string name, string password)
	{
		Name = name;
		using (MD5 md5 = MD5.Create())
		{
			PasswordHash = Convert.ToHexString(md5.ComputeHash(Encoding.UTF8.GetBytes(password)));
		}
	}
}

public class UserDTO
{
	public string Name { get; set; }
	public string Password { get; set; }
}