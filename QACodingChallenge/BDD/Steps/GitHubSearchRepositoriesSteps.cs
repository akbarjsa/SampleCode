using TechTalk.SpecFlow;
using QACodingChallenge.Objects;
using QACodingChallenge.BDD.StepsBehind;
using System.Collections.Generic;

namespace QACodingChallenge.BDD.Steps
{
    [Binding]
    public class GitHubSearchRepositoriesSteps
    {
        private static string Query { get; set; }
        private static List<RootObjects> SearchRepositoryResponse { get; set; }

        [Given(@"I have contructed the api with following Search-Repository Parameter")]
        public void GivenIHaveContructedTheApiWithSearchRepositoryParameter(Table table)
        {
            Query = SearchRepositoryLogic.BuildtheApiQuery(table);
        }

        [When(@"I invoke the Search-Repository API")]
        public void WhenIInvokeTheAPI()
        {
            SearchRepositoryResponse = new List<RootObjects>();
            SearchRepositoryResponse.Add(SearchRepositoryLogic.ExecuteGetResponse(Query));
        }
        [When(@"I invoke another Search-Repository API with different Parameters")]
        public void WhenIInvokeAnotherSearch_RepositoryAPIWithDifferentParameters(Table table)
        {
            Query = "";
            Query = SearchRepositoryLogic.BuildtheApiQuery(table);
            SearchRepositoryResponse.Add(SearchRepositoryLogic.ExecuteGetResponse(Query));
        }


        [Then(@"the api response should match '(.*)' scenario")]
        public void ThenTheApiResponseShouldMatch(string scenario, Table table)
        {
            SearchRepositoryLogic.ValidateResponse(scenario, table, SearchRepositoryResponse);
        }
    }
}
