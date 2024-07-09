[![CC BY-SA 4.0][cc-by-sa-shield]][cc-by-sa]

IOC Dentist is a tool to defang Indicators of Compromise (IOCs) - also known as "observables" - generated during cybersecurity investigations.

IOC/observable: a piece of data that identifies malicious activity on a computer system.  
Defanging: altering an IOC such that it cannot be rendered as a navigable link, even if made clickable by an application, but in a way that does not obscure pertinent information in the indicator.  
Fanging: restoring a defanged IOC to its original form by removing alterations.

IOC types that require defanging: IP addresses (both IPv4 and IPv6), URLs, domains, email addresses.
Defanging examples:
hacker@example.com => hacker[AT]example[.]com  
https://c2.example.com/path/to/malicious.file => hxxps://c2.example[.]com/path/to/malicious[.]file  
8.8.8.8 => 8.8.8[.]8

In the above examples you will see that adding brackets around dots in the addresses is enough to disrupt the application's ability to generate a clickable link, but without undue disruption to the human readability of the data. For thoroughness, the HTTP protocol identifier in URLs is also altered, and the At sign in email addresses is given similar treatment. Some analysts will choose to enclose all dots in brackets, but this is a matter of preference.  
These defanged IOCs would be safe to copy and share in any application without risk of accidental navigation or execution.

This work is licensed under a [Creative Commons Attribution-ShareAlike 4.0 International License][cc-by-sa].

[![CC BY-SA 4.0][cc-by-sa-image]][cc-by-sa]

[cc-by-sa]: http://creativecommons.org/licenses/by-sa/4.0/
[cc-by-sa-image]: https://licensebuttons.net/l/by-sa/4.0/88x31.png
[cc-by-sa-shield]: https://img.shields.io/badge/License-CC%20BY--SA%204.0-lightgrey.svg