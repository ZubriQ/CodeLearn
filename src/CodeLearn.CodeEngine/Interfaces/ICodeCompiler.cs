using CodeLearn.Domain.Common.Result;

namespace CodeLearn.CodeEngine.Interfaces;

public interface ICodeCompiler
{
    Result Compile(string formattedCode);
}