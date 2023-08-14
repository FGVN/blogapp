# blogapp

## Deploy

### Requirements
- dotnet 6

### Instructions
1. Clone this repository.

2. Navigate to the project solution file in your terminal.

3. Run the following command to start the application:
'dotnet run'

4. Access the website in your browser.

5. Register a new user using the provided registration functionality.

6. After registering, from the project solution folder, navigate to:

'wwwroot/data/loged.json'

7. Open the `loged.json` file and locate the user entry you just registered.

8. Change the value in the `isAdmin` field from `false` to `true` to promote the user to an admin.

9. Save the changes to the `loged.json` file.

10. Now you're all set. You can add new administrators using the site's admin features.

### MySQL Database Branch
If you prefer to use MySQL database instead of JSON, you can check out the MySQL branch of this repository:
[MySQL Branch](https://github.com/FGVN/blogapp)

Feel free to explore and contribute!

For any issues or questions, please create an issue on the repository.

Happy blogging!



