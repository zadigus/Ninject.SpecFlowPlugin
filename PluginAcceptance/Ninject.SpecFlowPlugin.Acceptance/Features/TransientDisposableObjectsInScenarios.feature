@check-disposed-after-scenario
Feature: Transient disposable scenario objects disposed after scenario

As a tester,
I need my transient disposable scenario dependencies to be automatically disposed,
so that their resources are freed.

    Scenario: Transient disposable scenario dependencies are disposed after scenario

    In order to check if a transient disposable scenario dependency actually is disposed
    after scenario execution, we need to assert disposal in an AfterFeature hook,
    hence the tag adorning this feature.

    Transient disposable scenario dependencies are not disposed after scenario execution
    when we use BoDi as the DI framework instead of Ninject.

        Given I have injected a transient disposable scenario dependency in the binding class StepClassDisposableAfterScenario