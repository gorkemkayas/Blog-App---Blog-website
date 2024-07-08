Project Description: Technology News Blog Platform
The goal of the project:
This project aims to create an interactive blog platform where technology-related news is shared and users can follow these news, comment and share their own posts.

Used technologies:
ASP.NET Core MVC: Used to create the basic structure of the project.
SQLite Database: Used to store data on the website. With the development of the project, various migrations were made and the database structure was updated.
Layout: Ensured that the blog page had a certain template and that all pages had a consistent appearance.
Interfaces: Used to reduce project dependency. For example, configurations such as IPostRepository and EfPostRepository were made.
ViewModels: A safer and more user-friendly structure was created by avoiding taking all properties of Entities as input in the form application.
ViewComponents: Created to avoid code duplication and increase reusability.
Authorization: A structure that requires users to log in to create posts and comment is used.
JQuery: It was used to add comments to the post and to instantly add the image uploaded to the page during registration.
Features of the Project:
User Registration and Login:

Users can quickly register by filling in information such as "Username", "Email", "Name and Surname", "Password", "Confirm Password" and "Title".
Once the user is logged in, they can access features such as creating posts and commenting.
Post Creation and Management:

Users can create posts with the form application and these posts are saved in the database.
Dynamic pages have been created for a detailed view of the posts.
By ensuring that posts have a unique URL structure, URLs are associated with topic headings and optimized.
Do not comment:

Users can comment on posts and these comments are instantly displayed on the page.
Post Views:

Blog posts are displayed through a specific template and users can follow the posts.
Deficiencies and Areas of Improvement:
Post Deletion: The feature that allows users to delete their own posts has not been added yet.
Like, Dislike and Save: These interaction features have not been activated yet.
Conclusion:
This project aims to create an interactive blog platform where users can share, follow and comment on technology news. It will be made even more user-friendly and interactive with some features currently under development. Once the missing aspects of the project are completed, the platform will be moved to a further level.
