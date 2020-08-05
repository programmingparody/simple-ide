# Simple IDE

## Idea:

Compile and execute code through simple interfaces. Ideally these interfaces should be easy enough to implement a multitude of Providers. 

## Motivation
- Provide IDE's to users as simple as possible to help with learning and improving. For example, a programming tutorial site with embeds to a Provider to provide students a quick way to modify and play with code samples

- Refresh on my C# knowledge

### Ideas for Provider implementations
- Docker Provider for quick virtual machines. Good for public use 
- Local hardware Provider for 

---

## Keywords / Domain names

### IProject
- Represents a Directory of code 
- Exposes:
    - `GetFile(string path)` returns `Stream`
    - `SaveFile(string path, Stream)` returns success flag `bool`
- Concreate implementations created through `Provider.CreateNewProject`

### Project Settings
- Property of Project: `Project.Settings`
- Defines attributes of a Project that are useful to a `Provider` when creating an `Environment`. Such as:
    - `Language`
        - Programming Language
    - `Version`
        - Language Version
    - `Meta`
        - Key/Value pair of additional language specific data

### Compiler
- Interface to anything that can build a `Project` into a `Build`
    - `Compiler.Build(Project)` returns `Build`

### Runtime
- Interface for anything that can take a `Build` and return a `BuildResult`

### Environment
- Parent class that contains `Compiler` and `Runtime`
    - `Compiler` and `Runtime` are accessible through Getters

### Environment Command
- Represents a shell command that is ran on an `Environment`

### Environment Events / Environment Events Handlers
- Events fired during stages of environment execution steps
- Handlers are a list of `Environment Commands`

### Build 
- Result of a compiled `Project`
- Getters for varialbes:
    - `ExecutablePath`
        - Filepath of the executable, read by `Runtime` when executing
    - `TimeStamp`
        - Time of when the `Compiler` completed
    - `CompileTimeStamp`
        - Time of when the `Compiler` started building
- Returned by `Compiler.Build(Project)`
- Executed by `Runtime.Execute(Build)`

### IProvider
- High level interface that manages and creates many of `Environment` and `Project`
- `Provider.CreateNewProjectFromData(byte[] rawProjectData, ProjectSettings)`
    - `rawProjectData` could be from file upload, something fetched from a remote source, local file, etc
    - Used for users that want to start a new project with existing code
    - Returns `Project` with `Project.Settings` applied from the 2nd parameter
- `Provider.CreateNewProject(ProjectSettings)`
    - `rawProjectData` could be from file upload, something fetched from a remote source, local file, etc
    - Returns `Project` with `Project.Settings` applied from the 1st parameter

### Environment Events (In Detail)
- `BeforeCompile`
    - `Environment Commands` ran before an *Environment* compiles a *Project*
- `AfterCompile`
    - `Environment Commands` ran after an *Environment* compiles a *Project*
- `BeforeExecute`
    - `Environment Commands` ran before an *Environment* executes a *Project*
- `AfterExecute`
    - `Environment Commands` ran after an *Environment* executes a *Project*

---
## Sample usecase: Run Golang code
[View usecase test](core.tests/usecases/RunGolangCodeTest)

1. User creates an emtpy project directory from a `Provider` with specified settings *IE: Language = Golang*
    - `Provider.CreateNewProject(new ProjectSettings{Language = "Golang"})`
    - Returns `Project` (**P**)
2. Through any concerete interface, user write's a hello world app
    ``` c#
    P.SaveFile("main.go", @"
        package main
        import ""fmt""
        func main() {
            fmt.Print(""hello golang"")
        }
    ");
    ```
3. Create an `Environment` for **P**
    - `Provider.CreateNewEnvironment(P)`
    - Returns concrete `IEnvironment` (**E**)

4. User run's their project and gets results
    - `E.Run(P)`
        - Returns `BuildResult` (**BR**)

5. User Reviews their results
    - `BR.StandardOut == "hello golang" //Should be true`

