Feature: CreditCardSpecflow


@Done
Scenario: Verify that wrong credit card number not accepted
	Given the credit card number is <cvc>
	When I set credit card number validations, getting validators
	Then credit card number must invalid

Examples:
	| cvc    |
	| 123456 |
	| 12     |
	|        |
	| abc    |
	| 1a2    |

@Done
Scenario: Verify that wrong CVC not accepted
	Given the CVC is <cvc>
	When I set CVC validations, getting validators
	Then CVC must invalid

Examples:
	| cvc    |
	| 123456 |
	| 12     |
	|        |
	| abc    |
	| 1a2    |

@Done
Scenario: Verify that wrong expiration date not accepted
	Given the expiration date is <exp>
	When I set expiration date validations, getting validators
	Then Expiration date must invalid

Examples:
	| exp    |
	| 123456 |
	| 12/21  |
	| 12/20  |
	| 01/22  |
	| 12     |
	|        |
	| abc    |
	| 1a2    |
	| 122021 |

@Done
Scenario: Verify that wrong card owner information not accepted
	Given the card owner information is <cardowner>
	When I set card owner information validations, getting validators
	Then card owner information must invalid

Examples:
	| cardowner |
	| E         |
	| E_        |
	| 1220      |
	| 022       |
	| 12        |
	|           |
	| ab        |
	| 1a2       |


Scenario: Verify that expired credit card doesn't accepted

Scenario: Verify that unknown credit card doesn't accepted

Scenario: Verify that valid credit card