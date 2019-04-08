./compose.sh to build docker images 

./attach.sh and you're in the container 

Once inside the container

// build
dotnet build

// change to the project folter 
cd SimpleToDo

// update the database 
dotnet ef database update


// run 
dotnet run

// access
http://localhost:8000/



// running unit tests 
dotnet test SimpleToDo.Repository.UnitTest/SimpleToDo.Repository.UnitTest.csproj

dotnet test SimpleToDo.Service.UnitTest/SimpleToDo.Service.UnitTest.csproj // No testes yet, maybe for a practical exercise 

dotnet test SimpleToDo.Web.UnitTest/SimpleToDo.Web.UnitTest.csproj 

