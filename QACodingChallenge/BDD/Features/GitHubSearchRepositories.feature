Feature: GitHubSearchRepositories
	In order to utilize GitHub Search Repositories API
	As a GitHub Users
	I want to view the Search Repositories Response

@SimpleSearch
Scenario: Validate Simple Search Parameter with Q
	Given I have contructed the api with following Search-Repository Parameter
	| Q   |
	| api |
	When I invoke the Search-Repository API
	Then the api response should match 'SearchCount' scenario
	| OverResultsCount | ItemsCount |
	| 6300000          | 30         |

Scenario: Search with Page Parameter - Compare Results
	Given I have contructed the api with following Search-Repository Parameter
	| Q   | Page |
	| api | 1    |
	When I invoke the Search-Repository API
	And I invoke another Search-Repository API with different Parameters
	| Q   | Page |
	| api | 2    |
	Then the api response should match 'DifferentPage' scenario
	| ItemsCount |


Scenario: Search with Page + PerPage Parameter
	Given I have contructed the api with following Search-Repository Parameter
	| Q   | Page | PerPage |
	| api | 2    | 5       |
	When I invoke the Search-Repository API
	Then the api response should match 'SearchCountItems' scenario
	| OverResultsCount | ItemsCount |
	| 6300000          | 5          |

Scenario: Search with Page + PerPage OutOfBoundry
	Given I have contructed the api with following Search-Repository Parameter
	| Q   | PerPage |
	| api | 5000    |
	When I invoke the Search-Repository API
	Then the api response should match 'SearchCountItems' scenario
	| ItemsCount |
	| 100        |

Scenario: Search with Sort Parameter
	Given I have contructed the api with following Search-Repository Parameter
	| Q   | Page | PerPage | Sort  |
	| api | 2    | 5       | stars |
	When I invoke the Search-Repository API
	Then the api response should match 'Sort' scenario
	| ItemsCount |

Scenario: Search with Order asc Parameter
	Given I have contructed the api with following Search-Repository Parameter
	| Q   | Page | PerPage | Sort  | Order |
	| api | 2    | 5       | stars | asc   |
	When I invoke the Search-Repository API
	Then the api response should match 'OrderAsc' scenario
	| OverResultsCount | ItemsCount |
	| 6300000          | 5          |

Scenario: Search with Order desc Parameter
	Given I have contructed the api with following Search-Repository Parameter
	| Q   | Page | PerPage | Sort  | Order |
	| api | 2    | 5       | stars | desc  |
	When I invoke the Search-Repository API
	Then the api response should match 'OrderDesc' scenario
	| OverResultsCount | ItemsCount |
	| 6300000          | 5          |

Scenario: Search with Language Qualifiers 
	Given I have contructed the api with following Search-Repository Parameter
	| Q                   |
	| api+language:csharp |
	When I invoke the Search-Repository API
	Then the api response should match 'Language' scenario
	| Language |
	| C#       |

Scenario: Search with Star Qualifiers 
	Given I have contructed the api with following Search-Repository Parameter
	| Q              |
	| api+stars:<500 |
	When I invoke the Search-Repository API
	Then the api response should match 'LessthanStars' scenario
	| Stars |
	| 499   |

Scenario: Search with MultipleQualifiers Stars-Fork-Language
	Given I have contructed the api with following Search-Repository Parameter
	| Q                                      |
	| api+stars:>500+fork:False+language:PHP |
	When I invoke the Search-Repository API
	Then the api response should match 'MultipleQualifiers' scenario
	| Stars | Fork  | Language |
	| 499   | False | PHP      |