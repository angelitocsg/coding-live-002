# Deploying a .NET Core API to Heroku | Coding Live #002

## Getting Started

These instructions is a part of a live coding video.

### Prerequisites

- .NET Core 3.1 SDK - https://dotnet.microsoft.com/download
- Heroku CLI - https://devcenter.heroku.com/articles/heroku-cli

## Example project

Create a base folder `CodingLive002`.

Create the .gitignore file based on file https://github.com/github/gitignore/blob/master/VisualStudio.gitignore

### Create an API project

```bash
dotnet new webapi --name MyApi
```

#### Add nuget package for SignalR

```bash
dotnet add package Microsoft.AspNetCore.SignalR --version 1.1.0
```

### Create a React App with typescript

```bash
npx create-react-app counter_page --template typescript
```

#### Add npm package for SignalR

```bash
yarn add @microsoft/signalr
```

### Deploying a container to Heroku

```bash
# Build React.js
yarn build

# Build container `webapi`
docker-compose build

# Heroku deploy process
heroku login
heroku apps:create myapp-name
heroku container:login
heroku container:push web -a myapp-name
heroku container:release web -a myapp-name
```