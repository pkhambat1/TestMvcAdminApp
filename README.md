# ContentHub

ContentHub is a project to demonstrate authentication and authorization of users in a web app. The authorization is designed in a hierarchy that goes from users down to roles, down to permissions and finally down to rights. Each right corresponds to one page of the website.

The website not only locks out users that are not logged in but it also checks for the user's assigned roles to determine whether they are allowed to view a particular page.

Users can be assigned roles such as "Executive", "Manager", "Content Creator", etc. which will determine the permissions they have. For instance, executives can read, add and modify all other users' roles, the permissions of each role and the rights of each permission. Managers can only view the same. The website successfully demonstrates how in real time these authorizations can be modified and immediately reflect in a user either gaining or losing access to a respective page.

The ContentHub structure of authorizations can be used in internal-use admin websites to establish nuance in the amount of access a user (employee) is granted.