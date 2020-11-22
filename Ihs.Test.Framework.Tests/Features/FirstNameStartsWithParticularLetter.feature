Feature: FirstNameStartsWithParticularLetter
Test 1 : Click “Run” button - Check Output window text as expected
Test 2 : 
       =>(If your first name starts with letter “A-B-C-D-E”):
			Select NuGet Packages: nUnit (3.12.0)
			Check that nUnit package is added
	   =>ELSE (If your first name starts with letter “F-G-H-I-J-K”):
			Click “Share” button
			Check that link starts with “https://dotnetfiddle.net/”
	   =>ELSE (If your first name starts with letter “L-M-N-O-P”):
			Click “<” button on “Options” panel to hide this panel
			Check that “Options” panel is hidden
	   =>ELSE (If your first name starts with letter “Q-R-S-T-U”):
			Click “Save” button
			Check that  “Log In” window appeared
	   =>ELSE (If your first name starts with letter “V-W-X-Y-Z”):
			Click “Getting Started” button
			Check that  “< Back to Editor” button appeared

@mytag
Scenario Outline: Test output window content equals input and perform action based first letter of your first name
	Given I Click the Run button, with text in code editor as Console.WriteLine("<message>")
	Then The text in the output window should match the input text, and should therefore be <message>
	Given My first name, <first name> starts with a particular letter
	Then Perform relevant actions
	And Verify outcomes are as expected

Examples: 
| message                     | first name  |
| Hello world                 | Michael     |
| IHS Markit - Preliminary    | Kenny       |
| Good morning                | Alex        |
| solve 3x + 2y -5 = 7        | Barbie      |
|                             | Charly-man  |
| Nearly Bye 2020, hello 2021 | Fey         |
| new message                 | Melony      |
| Oops                        | Omenga      |
| starts with W               | Wes         |
| name starts with Z          | Zeus        |
| How about Tango             | Texus       |
| Think of another name       | Really-Cant |
| Convert to int              | Eight       |
| K for...?                   | Kilo        |
	

