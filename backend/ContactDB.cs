using Microsoft.Data.Sqlite;

public class ContactDB
{
    private readonly string dbFileName;

    public ContactDB(string dbFilePath = "contact.db")
    {
        dbFileName = $"Data Source={dbFilePath}";
        InitializeDatabase();
    }

    public void InitializeDatabase()
    {
        using (var connection = new SqliteConnection(dbFileName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Contacts (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    title TEXT,
                    firstName TEXT,
                    middleName TEXT,
                    SurName TEXT,
                    PhoneNumber TEXT,
                    email TEXT,
                    Acquaintance TEXT
                );
            ";
            command.ExecuteNonQuery();
            }
        }
    }

    public void InsertContact(Contact contact)
    {
        using(var connection = new SqliteConnection(dbFileName))
        {
            connection.Open();

        using (var insertCommand = connection.CreateCommand())
        {
            insertCommand.CommandText = @"
                INSERT INTO Contacts (
                    title,
                    firstName,
                    middleName,
                    SurName,
                    PhoneNumber,
                    email,
                    Acquaintance
                ) VALUES (
                    @title,
                    @firstName,
                    @middleName,
                    @SurName,
                    @PhoneNumber,
                    @email,
                    @Acquaintance
                );
            ";
            insertCommand.Parameters.AddWithValue("@title", contact.Title);
            insertCommand.Parameters.AddWithValue("@firstName", contact.FirstName);
            insertCommand.Parameters.AddWithValue("@middleName", contact.MiddleName);
            insertCommand.Parameters.AddWithValue("@SurName", contact.SurName);
            insertCommand.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
            insertCommand.Parameters.AddWithValue("@email", contact.Email);
            insertCommand.Parameters.AddWithValue("@Acquaintance", contact.Acquaintance);
            insertCommand.ExecuteNonQuery();
        }
    }
}

public List<Contact> GetAllContacts()
{
    var contacts = new List<Contact>();
    using (var connection = new SqliteConnection(dbFileName))
    {
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Contacts;";
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    contacts.Add(new Contact(
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7)
                    ));
                }
            }
        }
    }
    return contacts;
    }
}