# Selenium 4 dynamic grid

refs - https://github.com/SeleniumHQ/docker-selenium 

* docker compose up
* port 4442 used for publishing
* port 4443 used for subscribing
* port 4444 used for UI


# Create a xUnit Selenium Test Project and run test against a Grid

* Setup project
    ```
    mkdir xunit-selenium-tests
    cd .\xunit-selenium-tests\
    dotnet new xunit
    dotnet add package Selenium.WebDriver
    ```

* Add you tests ...

* Run a grid
    ```
    docker compose up
    ```
* Run tests
    ```
    dotnet test -l trx
    ```


# Testing Selenium Standalone (development)

```
docker run -p 4444:4444 -p 5900:5900  selenium/standalone-chrome
dotnet test --filter xunit_selenium_tests.UITests.Test1
```
