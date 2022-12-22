# SolbegTask5

Online (electronics) shop

<a href="https://ibb.co/nwg2RL6"><img src="https://i.ibb.co/wd0HYzB/Screenshot-from-2022-12-22-14-11-48.png" alt="Screenshot-from-2022-12-22-14-11-48" border="0"></a>

## About
Online shop, build using asp .net for BackEnd, MsSql Server for database and React.tsx v6 for Frontend.
For this Task I have decided to try using TypeScript.

### Main features

- Showing product list for all even not logged users
- Showing product details after clicking on product overview card
- Adding / Removing product from card, clearing shopping cart
- Saving login, shop cart state even afer reloading page 
- Login form
- Role based authentication (User, Worker, Admin)
- Editing product list (only for Worker+) implemented only on server side yet

### Features to be added

- Search bar 
- Brand filter
- Pagination
- Worker panel (front end)
- Adding product panel (for worker)
- Registration panel
- Improved checkout panel
- More detailed userPanel
- Icons

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


#### External libraries:

- reactjs-popup - library
