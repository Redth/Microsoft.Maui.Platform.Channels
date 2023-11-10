plugins {

    /**
     * Use `apply false` in the top-level build.gradle file to add a Gradle
     * plugin as a build dependency but not apply it to the current (root)
     * project. Don't use `apply false` in sub-projects. For more information,
     * see Applying external plugins with same version to subprojects.
     */

    id("com.android.application") version "8.0.0" apply false
    id("com.android.library") version "8.0.0" apply false
    id("org.jetbrains.kotlin.android") version "1.8.20" apply false
}

// This block encapsulates custom properties and makes them available to all
// modules in the project. The following are only a few examples of the types
// of properties you can define.
ext {
    extra["sdkVersion"] = 33
    extra["minVersion"] = 21
    // You can also create properties to specify versions for dependencies.
    // Having consistent versions between modules can avoid conflicts with behavior.
    extra["appcompatVersion"] = "1.6.1"
    extra["materialVersion"] = "1.9.1"

    extra["junitVersion"] = "4.13.2"
}