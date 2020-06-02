# _RecipeBox Recipe Organizer Application_

#### _Epicodus Project June 2-3, 2020_

#### By _**Julia Seidman & Steven Ly**_


## Description

_A project to introduce many-to-many databases with MySQL in integration with the .NET Core framework with ASP.NET CORE MVC and MSBuild.  The project also serves as an introduction to user authentication with Identity, creating a user-specific data set. The application is a recipe organizer with recipe creation, categorization, and search functionality. This lesson serves as a reference for configuring, building, and launching web applications in C# with a SQL database backend. Dynamic sites using forms and views are explored with a web utility allowing users to create and modify recipes and search within their recipe collection._

## Setup/Installation Requirements

1. Clone this repository from GitHub.
2. Install MySQL on your computer.
3. Open the downloaded directory in a text editor of your choice. (VSCode, Atom, etc.)
4. In your terminal, navigate to the project directory and run the commands dotnet restore and dotnet build to download dependencies and build the configuration.
5. To run MySQL migrations and create a database in your MySQL installation, enter the following command in your terminal: ```dotnet ef database update```.
6. To launch the application in your browser, from the project directory in your terminal, enter ```dotnet run``` and open a browser page at localhost:5000.
7. For demo purposes, user accounts may be created with a mockup email and a single-character password. 


## Known Bugs

There are no known bugs at the time of this update, but the website is very ugly.

## Support and contact details

_Have a bug or an issue with this application? [Open a new issue] here on GitHub._

## Technologies Used

* C#
* .NET Core
* ASP.NET Core MVC
* MySQL
* Identity
* Entity Framework
* Razor / HTML Helpers
* MSBuild
* Git and GitHub

### Specs
| Spec | Input | Output |
| :------------- | :------------- | :------------- |
| **Site allows User to store a recipe** | User Input: "Name: Pepe Pizza, Prep Time: 30, Instructions: Make sure cheese is melted, Ingredients: Pizza, Rating: 5, MadeIt: Yes"    | Output: "Name: Pepe Pizza, Prep Time: 30 Minutes, Instructions: Make sure cheese is melted Ingredients: Pizza, Rating: 5, MadeIt: Yes" |
| **Site allows User to select a recipe and see details, including a list of tags associated with the recipe** | User Input: "Sesame Chicken" | Output: "Recipe: Sesame Chicken Tags: Asian"|
| **Site allows User to create new Tags** | User Input: Mexican | Output: "Tags: Mexican"|
| **Site allows User to assign a tag to a recipe** | User Input: Recipe: Tacos, Tag: Mexican  | Output: "Recipe: Tacos Tags: Mexican" |
| **Site allows User to edit recipe or tags** | User Input: "Name: Pepe Supreme Pizza, Prep Time: 25, Instructions: Make sure cheese is melted, Ingredients: Pizza, Rating: 4, MadeIt: Yes" | User Input: "Name: Pepe Supreme Pizza, Prep Time: 25, Instructions: Make sure cheese is melted, Ingredients: Pizza, Rating: 4, MadeIt: Yes" |
| **Site allows User to search recipes by tag** | User Search: "Mexican" | Output: "Pepe's Mexican Pizza, Gio's Guac, Margaritas" |




### License
This software is licensed under the MIT license.

Copyright (c) 2020 **_Julia Seidman & Steven Ly_**