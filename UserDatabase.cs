using System.Text;
using Newtonsoft.Json;

namespace LoginREST;

public static class UserDatabase
{
	private const string FilePath = "users.json";
	private static Dictionary<string, User> users = new();
	
	public static void Initialize()
	{
		LoadDictionary();
	}
	
	public static bool IsNameValid(string name)
	{
		int min = 1;
		int max = 32;
		int length = name.Length;
		return length >= min && length <= max;
	}
	
	public static bool AddUser(User user)
	{
		string key = user.Name;
		if (!users.ContainsKey(key))
		{
			users.Add(key, user);
			UpdateFile();
			return true;
		}

		return false;
	}
	
	private static void UpdateFile()
	{
		try
		{
			if (!File.Exists(FilePath))
				File.Create(FilePath);
			
			using (FileStream stream = new FileStream(FilePath, FileMode.Truncate))
			{
				using (StreamWriter writer = new(stream))
				{
					string json = JsonConvert.SerializeObject(users, Formatting.Indented);
					if (string.IsNullOrEmpty(json))
						throw new Exception("Failed to serialize dictionary to json :(");

					writer.Write(json);
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Failed to update file: " + ex.Message);
		}
	}
	
	private static void LoadDictionary()
	{
		try
		{
			using (FileStream stream = new FileStream(FilePath, FileMode.Open))
			{
				using (StreamReader reader = new(stream))
				{
					string json = reader.ReadToEnd();
					users = JsonConvert.DeserializeObject<Dictionary<string, User>>(json);
					if (users == null)
						throw new Exception("Failed to load users from json.");
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Failed to load dictionary: " + ex.Message);
			users = new();
		}

	}
}