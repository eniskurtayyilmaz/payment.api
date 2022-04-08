Feature: CreditCardSpecflow

	

Scenario: Verify that wrong credit card number not accepted
	Given the credit card number is <cardnumber> from Examples
	When I call the API /api/paymentLink with credit card number
	Then I see in response that Card number must be numeric with 15-16 length
	And I see response status code is BadRequest

	Given the credit card number is empty
	When I call the API /api/paymentLink with credit card number
	Then I see in response that Card number can not be null or empty
	And I see response status code is BadRequest

	
	Given the credit card number is null
	When I call the API /api/paymentLink with credit card number
	Then I see in response that Card number can not be null or empty
	And I see response status code is BadRequest
	

Examples:
	| cardnumber               |
	| 5000-1234-5678-9001-0000 |
	| 5000 1234 5678 9001 0000 |
	| 5000-1234-5678-9001      |
	| 5000 1234 5678 9001      |
	| 123456                   |
	| 12                       |
	| abc                      |
	| 1a2                      |
	| 40128888888818           |
	| 52042452500014282        |


Scenario: Verify that wrong CVC not accepted
	Given the CVC is <cvc> from Examples
	When I call the API /api/paymentLink with CVC
	Then I see in response that CVC must be numeric with 3-4 length
	And I see response status code is BadRequest

	Given the credit card number is empty
	When I call the API /api/paymentLink with credit card number
	Then I see in response that CVC can not be null or empty
	And I see response status code is BadRequest

	
	Given the credit card number is null
	When I call the API /api/paymentLink with credit card number
	Then I see in response that CVC can not be null or empty
	And I see response status code is BadRequest

Examples:
	| cvc    |
	| 123456 |
	| 12     |
	| abc    |
	| 1a2    |


Scenario: Verify that wrong expiration date not accepted
	Given the expiration date is <exp> from Examples
	When I call the API /api/paymentLink with expiration date
	Then I see in response that expiration date must be MM/YY format
	And I see response status code is BadRequest

	Given the expiration date is empty
	When I call the API /api/paymentLink with expiration date
	Then I see in response that expiration date can not be null or empty
	And I see response status code is BadRequest

	
	Given the expiration date is null
	When I call the API /api/paymentLink with expiration date
	Then I see in response that expiration date can not be null or empty
	And I see response status code is BadRequest

Examples:
	| exp    |
	| 123456 |
	| 12     |
	| abc    |
	| 1a2    |
	| 122021 |


Scenario: Verify that wrong card owner information not accepted
	Given the card owner information is <cardowner> from Examples
	When I call the API /api/paymentLink with card owner information
	Then I see in response that card owner information must be alphabetic
	And I see response status code is BadRequest

	Given the card owner information is empty
	When I call the API /api/paymentLink with card owner information
	Then I see in response that card owner information can not be null or empty
	And I see response status code is BadRequest

	
	Given the card owner information is null
	When I call the API /api/paymentLink with card owner information
	Then I see in response that card owner information can not be null or empty
	And I see response status code is BadRequest

Examples:
	| cardowner |
	| E         |
	| E_        |
	| 1220      |
	| 022       |
	| 12        |
	| 1a2       |



Scenario: Verify that unknown credit card doesn't accepted
	Given the unknown credit card is <cardnumber> from Examples
	When I call the API /api/paymentLink with unknown credit card
	Then I see in response that Only American Express, Visa, or Mastercard cards accepted
	And I see response status code is BadRequest

Examples:
	| cardnumber       |
	| 3530111333300000 |
	| 6011111111111117 |
	| 38520000023237   |


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
	
	
	
	
	
Scenario: Verify that expired credit card doesn't accepted
	Given the expired expiration date is <exp>
	And the valid credit number is <cardnumber>
	When I call the API /api/paymentLink with credit card number, expiration date
	Then I see in response that the credit card has been expired
	And I see response status code is BadRequest
	
Examples:
	| exp   | cardnumber       |
	| 12/21 | 5204245250001488 |
	| 12/20 | 374251018720018  |
	| 01/22 | 4012888888881881 |
