# Administrator Password Cracker
# Description
This is a lightweight tool used to get the password of the administrator on a windows machine. It has a built-in dictionary (rockyou.txt).
# Usage
<h3>Step One:</h3>
> Open powershell or command promt and type:
<code>net localgroup "Administrators"</code><br>
> You should receive something like this:
<br>

```text
Alias name     Administrators
Comment        Administrators have complete and unrestricted access to the computer/domain

Members

-------------------------------------------------------------------------------
Administrator
<administartor name>
The command completed successfully.
```
Pick the name under Administrator text. If there is no name under, that means that the administartor is "Administrator".

<h3>Step Two:</h3>
> "cd" into the directory where <code>AdminPass.exe</code> is stored.<br>
> type the following command <code>AdminPass.exe <ADMIN_USER_NAME> -b</code> to launch the attack.
  
```text
  Launching AdminPass on [<user>] at [9/22/2021 3:34:18 PM]

            Current Passpharse: omarion   
  [Attempt] : Invalid password : omarion | [587/14344399] 
```
  
