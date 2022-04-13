Feature: CreditCardSpecflow

A short summary of the feature


Scenario: Verify that valid credit card
	Given the valid credit number is <cardnumber>
	And the valid card owner <cardowner>
	And the valid expiration date <exp>
	And the valid CVC <cvc>
	When I call the API /api/paymentLink with credit card number, card owner, expiration date and cvc
	Then I see receipt ID
	And I see response status code is OK

Examples:
	| cardnumber       | cardowner       | exp   | cvc |
	| 4012888888881881 | enis kurtay     | 08/29 | 555 |
	| 5204245250001488 | sergei prokopov | 07/26 | 666 |
	| 374251018720018  | uras koray      | 12/24 | 785 |
	| 4012888888881881 | gokcen ozder    | 11/24 | 125 |
	