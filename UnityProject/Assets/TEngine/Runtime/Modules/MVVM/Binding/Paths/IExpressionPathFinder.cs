

using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Binding.Paths
{
    public interface IExpressionPathFinder
    {
        List<Path> FindPaths(LambdaExpression expression);
        
    }
}
