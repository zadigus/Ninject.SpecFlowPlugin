@check-disposed-after-scenario
Feature: Singleton disposable objects after scenario

As a tester,
I need my disposable singleton scenario dependencies to be automatically disposed,
so that their resources are freed.

    Scenario: Singleton disposable scenario dependencies are disposed after scenario

    In order to check if a singleton disposable scenario dependency actually is disposed
    after scenario execution, we need to assert disposal in an AfterFeature hook,
    hence the tag adorning this feature.

        Given I have injected a singleton disposable scenario dependency in the binding class StepClassDisposableAfterScenario