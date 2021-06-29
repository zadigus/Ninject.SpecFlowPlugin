Feature: Transient disposable scenario objects disposed after scenario

As a tester,
I need my transient disposable scenario dependencies to be automatically disposed,
so that their resources are freed.

    Scenario Outline: Transient disposable scenario dependencies are disposed after scenario

    In order to check if a transient disposable scenario dependency actually is disposed
    after scenario execution, we need to assert disposal on feature context.

    Transient disposable scenario dependencies are not disposed after scenario execution
    when we use BoDi as the DI framework instead of Ninject.

        Given I have injected TransientDisposableScenarioDependency<injected dependency> in the binding class StepClassDisposableAfterScenario
        Then TransientDisposableScenarioDependency<disposed dependency> has been disposed if the previous scenario had to dispose it

        Examples:

          | injected dependency | disposed dependency |
          | 1                   | 2                   |
          | 2                   | 1                   |