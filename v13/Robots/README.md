# Robots

When the package is installed, the robots file can be found at `/robots.txt`.

Underlying, the robots file is controlled by the `IRobotsTxtService` interface, where the package's default implementation is provided by the `RobotsTxtService` class.

To control the sitemap, you can create a custom class that implements the `IRobotsTxtService` interface, or extends the `RobotsTxtService` class. Extending the class is recommended, as you don't have to write the implementation from scratch, but can override relevant virtual methods instead.

## Properties

The default implementation will look for a `robotsTxt` property on the site node:

- `robotsTxt`  
Add a textarea property with this alias to the site node to allow users to add custom rules to the `robots.txt` file.