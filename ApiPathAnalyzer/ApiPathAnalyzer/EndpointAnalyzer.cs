using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ApiPathAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class EndpointAnalyzer : DiagnosticAnalyzer
    {
        private const string DiagnosticId = "EP0001";
        private static readonly LocalizableString Title = "Invalid endpoint path";
        private static readonly LocalizableString MessageFormat = "Endpoint path must start with '/api' and must not contain underscores.";
        private static readonly LocalizableString Description = "Checks that endpoints always start with '/api' and do not contain underscores.";
        private const string Category = "Usage";

        private static readonly DiagnosticDescriptor Rule = new(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics 
            => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            // Register a syntax node action to look for attribute usages, such as [Route(...)] or [HttpGet(...)].
            context.RegisterSyntaxNodeAction(AnalyzeAttributeSyntax, SyntaxKind.Attribute);
        }

        private static void AnalyzeAttributeSyntax(SyntaxNodeAnalysisContext context)
        {
            if (context.Node is not AttributeSyntax attributeSyntax)
                return;

            // We'll look for attributes that are typically used for endpoints, e.g., Route, HttpGet, etc.
            var attributeName = attributeSyntax.Name.ToString().ToLowerInvariant();
            if (!IsEndpointAttribute(attributeName))
                return;

            // If the attribute has no arguments (e.g., just [HttpGet]), then there's no path to analyze.
            if (attributeSyntax.ArgumentList == null || attributeSyntax.ArgumentList.Arguments.Count == 0)
                return;

            // We're assuming the first argument is the route (e.g., [Route("/api/example")]).
            // In more complex scenarios, you might need additional logic to handle named parameters, etc.
            var routeArgument = attributeSyntax.ArgumentList.Arguments[0];
            if (routeArgument.Expression is not LiteralExpressionSyntax literalExpr ||
                literalExpr.Token.Kind() != SyntaxKind.StringLiteralToken)
            {
                return;
            }

            var routeText = literalExpr.Token.ValueText; 
            if (string.IsNullOrEmpty(routeText))
                return;

            // Perform checks:
            // 1. Must start with "/api".
            // 2. Must not contain underscores.
            if (!routeText.StartsWith("/api") || routeText.Contains("_"))
            {
                var diagnostic = Diagnostic.Create(Rule, literalExpr.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static bool IsEndpointAttribute(string attributeName)
        {
            // You can add or remove attributes as needed.
            // Checking a few common attribute names:
            return attributeName.StartsWith("route")
                   || attributeName.StartsWith("httpget")
                   || attributeName.StartsWith("httppost")
                   || attributeName.StartsWith("httpput")
                   || attributeName.StartsWith("httpdelete")
                   || attributeName.StartsWith("httppatch");
        }
    }
}