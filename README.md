# TappApp
C# application using WPF and SQL database.  
Connects users who need help or want to help with translating txt files of other users.

## Current functionality of the app:

### CREATE PROJECT
Select file from file explorer and upload it to the database with file name: NAME_OriginalLanguage_TranslateLanguage.txt  
**example file name:** Test_English_German.txt

### FILTER PROJECTS
Write compination of filter commands in textbox to filter your loaded projects.  
**example filter commands:**
translated => shows only projects with translation
translated languages_czech spanish => shows translated projects where the original language is czech or spanish
languages_english_T => shows project where the translation language is english

### STATS
Generates csv file with statistics for shown projects. Statistics include word and sentence count for both files in project.

### REACH TRANSLATORS
"Sends email" to translators who speak both languages of selected project.  
Sending emails is not yet implemented. Execution of the Send function is logged in /assets/email_log.txt

### DELETE PROJECT
Deletes selected project from database

### GIVE UP
For all translations of translator sets translation file to NULL and translator_id to NULL.  
Then marks translator as inactive and shuts down the app.

### LOG OUT
Saves changed translations into database if possible and logs out the user.

### PURPLE MODE
Turns on connection to database wasn't possible or the user wasn't found as active.  
Allows some of apps's original functionality on temporary scale.
