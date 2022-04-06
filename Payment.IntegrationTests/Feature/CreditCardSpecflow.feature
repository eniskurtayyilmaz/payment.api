Feature: CreditCardSpecflow



Scenario: Verify that wrong credit card number not accepted
	Given the credit card number is <cardnumber>
	When I set credit card number validations, getting validators
	Then the credit card number must invalid

Examples:
	| cardnumber               |
	| 5000-1234-5678-9001-0000 |
	| 5000 1234 5678 9001 0000 |
	| 5000-1234-5678-9001      |
	| 5000 1234 5678 9001      |
	| 123456                   |
	| 12                       |
	|                          |
	| abc                      |
	| 1a2                      |
	| 40128888888818           |
	| 52042452500014282        |


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
	Given the expiration date is <exp>
	And the credit card is <cardnumber>
	When I set expiration date and credit card validations, getting validators
	Then Expiration date must invalid
	
Examples:
	| exp    | cardnumber       |
	| 123456 | 4012888888881881 |
	| 12/21  | 5204245250001488 |
	| 12/20  | 374251018720018  |
	| 01/22  | 4012888888881881 |
	| 12     | 5204245250001488 |
	|        | 374251018720018  |
	| abc    | 4012888888881881 |
	| 1a2    | 5204245250001488 |
	| 122021 | 374251018720018  |


Scenario: Verify that unknown credit card doesn't accepted
	Given the unknown credit card is <cardnumber>
	When I set known credit card validations, getting validators
	Then the unknown credit card must invalid

Examples:
	| cardnumber       |
	| 3530111333300000 |
	| 6011111111111117 |
	| 38520000023237  |


Scenario: Verify that valid credit card
	Given the valid credit number is <cardnumber>
	And the valid card owner <cardowner>
	And the valid expiration date <exp>
	And the valid CVC <cvc>
	When I set known credit card validations and other validations
	Then all of informatin must be valid

Examples:
	| cardnumber       | cardowner       | exp   | cvc |
	| 4012888888881881 | enis kurtay     | 08/29 | 555 |
	| 5204245250001488 | sergei prokopov | 07/26 | 666 |
	| 374251018720018  | uras koray      | 12/24 | 785 |
	| 4012888888881881 | gokcen ozder    | 11/24 | 125 |
	
	
	
	
	