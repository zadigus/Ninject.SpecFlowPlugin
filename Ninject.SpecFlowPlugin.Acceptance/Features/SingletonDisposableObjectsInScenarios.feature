Feature: Singleton disposable objects after scenario

As a tester,
I need my disposable singleton scenario dependencies to be automatically disposed,
so that their resources are freed.

    Scenario Outline: Singleton disposable scenario dependencies are disposed after scenario

    In order to check if a singleton disposable feature dependency actually is disposed
    after feature execution, we need to assert disposal on feature context.

        Given I have injected SingletonDisposableScenarioDependency<injected dependency> in the binding class StepClassDisposableAfterScenario
        Then SingletonDisposableScenarioDependency<disposed dependency> has been disposed if the previous scenario had to dispose it

        Examples:

          | injected dependency | disposed dependency |
          | 1                   | 2                   |
          | 2                   | 1                   |