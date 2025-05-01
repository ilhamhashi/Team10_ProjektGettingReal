using System;

abstract public class User
{
	private string _name {  get; set; }
	private string _email { get; set; }
	private string _password { get; set; }
	private string _userName { get; set; }
	private bool _isAdmin { get; set; }
	public bool isLoggedIn	{ get; set; }
	public List<User> users;

	public User(string name, string email, string password, string userName, bool isAdmin)
	{
		_name = name; 
		_email = email;
		_password = password;
		_userName = userName;
		_isAdmin = isAdmin;
		this.isLoggedIn = false;
	}

	public abstract string GetUserInformation();
	public abstract bool IsUserLoggedIn();
}
