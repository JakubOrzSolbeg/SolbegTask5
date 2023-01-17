# SolbegTask5

Online (electronics) shop

<a href="https://ibb.co/864Knbq"><img src="https://i.ibb.co/Chs1p6g/Screenshot-from-2023-01-18-00-17-21.png" alt="Screenshot-from-2023-01-18-00-17-21" border="0"></a>
## About
Online shop, build using asp .net for BackEnd, MsSql Server for database and React.tsx v6 for Frontend.
For this Task I have decided to try using TypeScript.

### Main features

- Showing product list for all even not logged users
- Showing product details after clicking on product overview card
- Adding / Removing product from card, clearing shopping cart
- Saving login, shop cart state even after reloading page 
- Login and Register form
- Role based authentication (User, Worker, Admin)
- Search manu
- Pagination
- Worker panel, can verify and reject orders, add new products
- Improved checkout panel


### Configuration and deployment

1. Clone this repo using: `git clone https://github.com/JakubOrzSolbeg/SolbegTask4.git`
2. Start the backend server first:
    1. `cd Backend`
    2. `dotnet run`
3. Open another terminal window to prepare frontend server:
    - `cd frontend`
4. If running for the first time assuming that npm is already installed:
    - `npm install`
5. Start the development server:
    - Linux/ MacOS `npm run start`

## Project structure

<a href="https://ibb.co/68tB7hH"><img src="https://i.ibb.co/sC1HdSK/soleg-task5-schemat-glowny-drawio.png" alt="soleg-task5-schemat-glowny-drawio" border="0"></a>

### ApiResults

In order for easier data parsing every api response from server is generic object class ApiResultBase < T > 

{

   isSuccess: boolean, 

   errors: string,
   
   body?: T

}

#### Legacy code
Some componenets use older ApiRequests, new components use Universal Api request method

#### External libraries:

- reactjs-popup - library
