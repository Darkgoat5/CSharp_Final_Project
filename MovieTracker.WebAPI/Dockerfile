# gets the full SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# sets the base working dir
WORKDIR /src

# copies the csporj file into /src/movietracker.webapi folder
COPY ["MovieTracker.WebAPI/MovieTracker.WebAPI.csproj", "MovieTracker.WebAPI/"]

# downloads the nuget dependencies
RUN dotnet restore "MovieTracker.WebAPI/MovieTracker.WebAPI.csproj"

# copy the rest of the project
COPY . .

# CD into the project directory
WORKDIR "/src/MovieTracker.WebAPI"

# publish the project into app/movietracker folder
RUN dotnet publish "MovieTracker.WebAPI.csproj" -c Release -o /app/movietracker

# use smaller runtime asp image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# sets working dir
WORKDIR /app

# container will listen on 8080
EXPOSE 8080

# copies published files from build stage
COPY --from=build /app/movietracker .

# command to run the application
ENTRYPOINT ["dotnet", "MovieTracker.WebAPI.dll"]
