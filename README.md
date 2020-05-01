# Deploying a .NET Core API to Heroku | Coding Live #002

## Getting Started

These instructions is a part of a live coding video.

### Prerequisites

- .NET Core 3.1 SDK - https://dotnet.microsoft.com/download

## Example project

Create a base folder `CodingLive002`.

Create the .gitignore file based on file https://github.com/github/gitignore/blob/master/VisualStudio.gitignore

### Create an API project

```
dotnet new webapi --name MyApi
```

#### Add nuget package for SignalR

```
dotnet add package Microsoft.AspNetCore.SignalR --version 1.1.0
```

### Create a React App with typescript

```
npx create-react-app counter_page --template typescript
```

#### Add npm package for SignalR

```
yarn add @microsoft/signalr
```
