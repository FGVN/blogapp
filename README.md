# blogapp

# MySQL Database Branch for blogapp

This branch of the blogapp repository uses a MySQL 8.0 database to store user information and content. Follow the steps below to set up the application with the MySQL database.

## Setup

### Requirements
- dotnet 6
- MySQL 8.0

### Instructions

1. **Clone Repository**: Clone this repository and switch to the MySQL branch.

2. **Install MySQL**: Ensure you have MySQL 8.0 installed on your system.

3. **Create Database**: Create a new MySQL database for the blogapp using MySQL command line or a GUI tool.

4. **Import Database Structure**: Run the `database.sql` script located in the project root folder to set up required tables and schema for the blogapp.

   ```bash
   mysql -u your_username -p your_database_name < database.sql
   
Replace your_username with your MySQL username and your_database_name with your database name.

5. **Configure Connection:** Update the appsettings.json file in the project with your MySQL connection details. Modify the DefaultConnection section to match your MySQL server, username, password, and database name.

6, **Run Application:** Navigate to the project solution file in your terminal and run:
`dotnet run`
7. **Access Website:** Open the website in your browser.

8. **Register User:** Register a new user using the provided registration functionality.

9. **Promote User to Admin:** After registering, access the MySQL command line or a GUI tool to connect to the MySQL database.

Run SQL Command: Run the following MySQL command to promote the registered user to an admin. Replace username with the actual username of the user you registered.

`USE blogapp;
UPDATE loged SET isAdmin = true WHERE username = 'username';`

**Ready to Use:** You're all set! You can now manage administrators and use the site's admin features.

Feel free to explore and contribute to this MySQL database branch. For any issues or questions, please create an issue on the repository.

Happy blogging with the MySQL-powered blogapp!

### MySQL Database Branch
If you prefer to use JSON database instead of MySQL, you can check out the MySQL branch of this repository:
[JSON Branch]([https://github.com/FGVN/blogapp](https://github.com/FGVN/blogapp/edit/JsonData)https://github.com/FGVN/blogapp/edit/JsonData)






