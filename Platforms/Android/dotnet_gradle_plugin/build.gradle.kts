plugins {
    // Apply the Java Gradle plugin development plugin to add support for developing Gradle plugins
    id("com.gradle.plugin-publish") version "1.2.0"
}

group = "net.microsoft.plugin"
version = "0.1"

gradlePlugin {
    website.set("https://github.com/Redth/Microsoft.Maui.Platform.Channels")
    vcsUrl.set("https://github.com/Redth/Microsoft.Maui.Platform.Channels")

    // Define the plugin
    plugins {
        create("dotnetinterop") {
            id = "net.microsoft.plugin.dotnetinterop"
            implementationClass = "net.microsoft.plugin.DotNetInteropPlugin"
            displayName = ".NET Interop Gradle plugin"
            description = ".NET Interop Gradle plugin"
        }
    }
}

dependencies {
    // Use JUnit test framework for unit tests
    testImplementation("junit:junit:4.13.2")
}

// Add a source set and a task for a functional test suite
val functionalTest by sourceSets.creating
gradlePlugin.testSourceSets(functionalTest)

configurations[functionalTest.implementationConfigurationName].extendsFrom(configurations.testImplementation.get())

val functionalTestTask = tasks.register<Test>("functionalTest") {
    testClassesDirs = functionalTest.output.classesDirs
    classpath = configurations[functionalTest.runtimeClasspathConfigurationName] + functionalTest.output
}

tasks.check {
    // Run the functional tests as part of `check`
    dependsOn(functionalTestTask)
}