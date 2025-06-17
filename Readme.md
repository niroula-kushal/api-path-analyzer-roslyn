# ApiPathAnalyzer

ApiPathAnalyzer is a .NET project aimed at analyzing and aiding in the inspection of API endpoints and their associated routing attributes. By integrating this analyzer into your .NET projects, you can quickly identify any potential inconsistencies or improvements related to your routes, ultimately helping you maintain a cleaner and safer codebase.

## Features

- **Route Attribute Inspection**: Identifies and analyzes custom or built-in attributes used for declaring API routes.  
- **Consistent API Structure**: Encourages consistent naming and usage of routes across your codebase.

## Getting Started

1. **Create a New Analyzer Project**  
   - In your existing .NET solution, create a new .NET Analyzer project (for example, by selecting a “C# Analyzer” project template if available, or a standard .NET class library project that references the `Microsoft.CodeAnalysis` NuGet packages as needed).  
   - Place your analyzer logic within this project.

2. **Add the Analyzer Project to Your Solution**  
   - Reference your newly created analyzer project in any other project(s) you want to be analyzed.  

3. **Build the Analyzer**  
   - Use the .NET CLI or your preferred IDE to build your solution:
     ```bash
     dotnet build
     ```
   - Any issues or recommendations identified by ApiPathAnalyzer will appear as warnings or suggestions in your IDE’s code analysis results.

4. **Use the Analyzer in Your IDE**  
   - Review any warnings or suggestions provided, and make the necessary changes to ensure a consistent API structure.

## Note
This project is just a showcase of what can be done, and the logic can be changed in **EndpointAnalyzer.cs** (or your custom file) to suit your specific requirements or scenarios.

## Contributing

- **Issues**: If you encounter bugs, have ideas for improvements, or would like new features, open an issue to start a discussion.  
- **Pull Requests**: Contributions are welcome! Feel free to submit a pull request with relevant changes or fixes accompanied by a clear explanation.

---

We hope this helps you quickly get started using ApiPathAnalyzer. If you have any questions, feel free to open an issue or reach out to maintainers. Happy coding!