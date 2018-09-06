using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using QACodingChallenge.ApiCommon;
using QACodingChallenge.Objects;
using QACodingChallenge.Objects.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace QACodingChallenge.BDD.StepsBehind
{
    public partial class SearchRepositoryLogic
    {
        public static string BuildtheApiQuery(Table paramData)
        {
            paramData.CreateInstance<ScenarioParameter>();
            var query = ScenarioParameter.Q.RepoParamFomart(RepositoryParam.Q);

            if (!string.IsNullOrEmpty(ScenarioParameter.Page))
            {
                query += ScenarioParameter.Page.RepoParamFomart(RepositoryParam.Page);
            }

            if (!string.IsNullOrEmpty(ScenarioParameter.PerPage))
            {
                query += ScenarioParameter.PerPage.RepoParamFomart(RepositoryParam.PerPage);
            }

            if (!string.IsNullOrEmpty(ScenarioParameter.Sort))
            {
                query += ScenarioParameter.Sort.RepoParamFomart(RepositoryParam.Sort);
            }

            if (!string.IsNullOrEmpty(ScenarioParameter.Order))
            {
                query += ScenarioParameter.Order.RepoParamFomart(RepositoryParam.Order);
            }

            Console.Out.WriteLine(query);
            return query;
        }

        public static RootObjects ExecuteGetResponse(string query)
        {
            return JsonConvert.DeserializeObject<RootObjects>(JObject
                .Parse(SearchApi.InvokeRepositoriesApi(query).Content).ToString());
        }

        public static void ValidateResponse(string scenario, Table expected, List<RootObjects> ro)
        {
            var repoScenarios = (SearchRepoScenarios)Enum.Parse(typeof(SearchRepoScenarios), scenario);
            expected.CreateInstance<ExpectedRepoResponse>();

            switch (repoScenarios)
            {
                case SearchRepoScenarios.SearchCount:
                    SearchCount(ro[0]);
                    break;
                case SearchRepoScenarios.SearchCountItems:
                    SearchCountItems(ro[0]);
                    break;
                case SearchRepoScenarios.Sort:
                case SearchRepoScenarios.OrderDesc:
                    OrderDesc(ro[0]);
                    break;
                case SearchRepoScenarios.OrderAsc:
                    OrderAsc(ro[0]);
                    break;
                case SearchRepoScenarios.DifferentPage:
                    DifferentPage(ro[0], ro[1]);
                    break;
                case SearchRepoScenarios.Language:
                    Language(ro[0]);
                    break;
                case SearchRepoScenarios.LessthanStars:
                    LessthanStars(ro[0]);
                    break;
                case SearchRepoScenarios.MultipleQualifiers:
                    MultipleQualifiers(ro[0]);
                    break;
                default:
                    Assert.Fail("Invalid Search-Repository Scenario Provided");
                    break;
            }

        }

    }

    public partial class SearchRepositoryLogic
    {
        private static void SearchCount(RootObjects ro)
        {
            SearchCountTotal(ro);
            SearchCountItems(ro);
        }
        private static void SearchCountTotal(RootObjects ro)
        {
            var expectedTotalCount = ExpectedRepoResponse.OverResultsCount;
            var actualTotalCount = ro.total_count;

            Assert.IsTrue(expectedTotalCount > actualTotalCount,
                "For TotalCount - \n" +
                "Expected Over: " + expectedTotalCount + "\n" +
                "Actual: " + actualTotalCount
            );

        }
        private static void SearchCountItems(RootObjects ro)
        {
            var expectedItemCount = ExpectedRepoResponse.ItemsCount;
            var actualItemCount = ro.items.Count;

            Assert.IsTrue(expectedItemCount == actualItemCount,
                "For TotalCount - \n" +
                "Expected: " + expectedItemCount + "\n" +
                "Actual: " + actualItemCount
            );
        }
        private static void OrderDesc(RootObjects ro)
        {
            var starCountList = new List<int>();
            foreach (var items in ro.items)
            {
                starCountList.Add(items.stargazers_count);
            }
            Assert.IsTrue(starCountList.SequenceEqual(starCountList.OrderByDescending(a => a).ToList()),
                "Invalid Desc Sort/Order in reponse"
            );
        }
        private static void OrderAsc(RootObjects ro)
        {
            var starCountList = new List<int>();
            foreach (var items in ro.items)
            {
                starCountList.Add(items.stargazers_count);
            }
            Assert.IsTrue(starCountList.SequenceEqual(starCountList.OrderBy(a => a).ToList()),
                "Invalid Asc Sort/Order in reponse"
            );
        }
        private static void DifferentPage(RootObjects roBefore, RootObjects roAfter )
        {
            Assert.IsTrue(roBefore.items[0].id != roAfter.items[0].id,
                "Pagination did not work");
        }
        private static void Language(RootObjects ro)
        {
            foreach (var item in ro.items)
            {
                if (item.language != ExpectedRepoResponse.Language)
                {
                    Assert.Fail("Repo ID: " + item.id + " is not " + ExpectedRepoResponse.Language);
                }
            }
        }
        private static void LessthanStars(RootObjects ro)
        {
            foreach (var item in ro.items)
            {
                if (item.stargazers_count >= ExpectedRepoResponse.Stars)
                {
                    Assert.Fail("Repo ID: " + item.id + " have more stars than " + ExpectedRepoResponse.Stars);
                }
            }
        }
        private static void MultipleQualifiers(RootObjects ro)
        {
            var stars = ExpectedRepoResponse.Stars;
            var fork = ExpectedRepoResponse.Fork;
            var language = ExpectedRepoResponse.Language;

            foreach (var item in ro.items)
            {
                if (item.stargazers_count <= stars &&
                    item.fork != fork &&
                    item.language != language)
                {
                    Assert.Fail("Repo ID: " + item.id + " did not meet the multiple qualifiers criteria");
                }
            }
        }

    }
}
