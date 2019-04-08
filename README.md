Build docker:
```sh
$ ./compose.sh
```

 Get in the container:
```sh
$ ./attach.sh
```


Once inside...

Go to the web project folder
```sh
$ cd SimpleToDo
```


Build the app
```sh
$ dotnet build
```


Update the database
```sh
$ dotnet ef database update
```


Run 
```sh
$ dotnet run
```


Access in your browser
```sh
http://localhost:8000
```


Running the unit tests
```sh
$ dotnet test SimpleToDo.Repository.UnitTest/SimpleToDo.Repository.UnitTest.csproj
```

```sh
$ dotnet test SimpleToDo.Web.UnitTest/SimpleToDo.Web.UnitTest.csproj 
```


// No testes yet, maybe for a practical exercise!?
```sh
$ dotnet test SimpleToDo.Service.UnitTest/SimpleToDo.Service.UnitTest.csproj 
```
