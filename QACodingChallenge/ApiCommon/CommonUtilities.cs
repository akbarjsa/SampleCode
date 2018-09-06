using System;

namespace QACodingChallenge.ApiCommon
{
    public static class CommonUtilities
    {
        public static string RepoParamFomart(this string value, RepositoryParam param)
        {
            const string and = "&";
            switch (param)
            {
                case RepositoryParam.Q:
                    return "?q=" + value;
                case RepositoryParam.Page:
                    return and + "page=" + value;
                case RepositoryParam.PerPage:
                    return and + "per_page=" + value;
                case RepositoryParam.Sort:
                    return and + "sort=" + value;
                case RepositoryParam.Order:
                    return and + "order=" + value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(param), param, null);
            }
        }
    }
}
