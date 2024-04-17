<h1>Football-Insight</h1>
<p>An application for football where, as an admin, you can manage matches and, as a user, check what is happening in those matches.</p>
<h3>Application configuration</h3>
<ol>
  <li>The connection string located in appsettings.json should be replaced with your own string.</li>
  <li>Execute the command <strong>Update-Database</strong> in the Package Manager Console to apply the migration to the database.</li>
  <ul>
    <li>The <strong>Football-Insight.Infrastructure</strong> should be selected as the default project.</li>
  </ul>
  <li>Data is seeded during migration. Here are the users that can be used:</li>
  <ul>
    <li>user@fi.com / User123!</li>
    <li>admin@fi.com / Admin123!</li>
    <li>If you register, you will automatically be assigned the User role.</li>
    <li>When you are logged out, you only have access to the login and registration pages.</li>
  </ul>
  <li>You can configure the duration of one minute in a match by specifying the number of seconds in the constant named <strong>SettingSecondsInOneMinute</strong>. This setting is located in <strong>GlobalConstants.cs</strong> at <strong>Football-Insight.Core/Constants/GlobalConstants.cs</strong>.</li>
  <ul>
    <li>Currently, it is set to 5 seconds for testing purposes, so you don't have to wait a full 90 minutes for one match.</li>
    <li>It is recommended not to set it under 5 seconds. The system has been tested with this value and higher.</li>
  </ul>
  <li>Thats it!</li>
</ol>

Additionally, the application is published on Azure and can be accessed from this *[Link](https://footballinsight20240416202033.azurewebsites.net)*.
