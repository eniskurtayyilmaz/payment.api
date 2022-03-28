Feature: CreditCardSpecflow
Simple calculator for adding two numbers

    Scenario: Card Owner is ek
        Given the card owner is ek
        When I request with card number
        Then the result must invalid

    Scenario: Card Owner is e
        Given the card owner is e
        When I request with card number
        Then the result must invalid