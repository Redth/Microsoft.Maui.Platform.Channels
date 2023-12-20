package net.microsoft.plugin;

import org.gradle.testkit.runner.BuildResult;
import org.gradle.testkit.runner.GradleRunner;
import org.junit.Test;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Writer;
import java.nio.file.Files;
import java.util.ArrayList;

import static org.junit.Assert.assertTrue;


public class DotNetInteropPluginFunctionalTest {
    @Test
    public void canRunTask() throws IOException {
        // Setup the test build
        File projectDir = new File("build/functionalTest");
        File moduleDir = new File("build/functionalTest/app");

        Files.createDirectories(projectDir.toPath());
        Files.createDirectories(moduleDir.toPath());
        writeString(new File(projectDir, "gradle.properties"),
        "android.useAndroidX=true\n");

        writeString(new File(projectDir, "settings.gradle.kts"),

                "pluginManagement {\n" +
                        "    repositories {\n" +
                        "        gradlePluginPortal()\n" +
                        "        google()\n" +
                        "        mavenCentral()\n" +
                        "    }\n" +
                        "}\n" +
                        "\n" +
                        "dependencyResolutionManagement {\n" +
                        "    repositoriesMode.set(RepositoriesMode.FAIL_ON_PROJECT_REPOS)\n" +
                        "    repositories {\n" +
                        "        google()\n" +
                        "        mavenCentral()\n" +
                        "    }\n" +
                        "}\n" +
                        "\n" +
                        "\n" +
                        "rootProject.name = \"TestProject\"\ninclude(\":app\")");

        writeString(new File(projectDir, "build.gradle.kts"),
                "plugins {\n" +
                        "\n" +
                        "    /**\n" +
                        "     * Use `apply false` in the top-level build.gradle file to add a Gradle\n" +
                        "     * plugin as a build dependency but not apply it to the current (root)\n" +
                        "     * project. Don't use `apply false` in sub-projects. For more information,\n" +
                        "     * see Applying external plugins with same version to subprojects.\n" +
                        "     */\n" +
                        "\n" +
                        "    id(\"com.android.application\") version \"8.0.0\" apply false\n" +
                        "    id(\"com.android.library\") version \"8.0.0\" apply false\n" +
                        "    id(\"org.jetbrains.kotlin.android\") version \"1.8.20\" apply false\n" +
                        "  id(\"net.microsoft.plugin.dotnetinterop\")\n" +
                        "}\n");

        writeString(new File(moduleDir, "build.gradle.kts"),
            "plugins {\n" +
                    "    id(\"com.android.library\")\n" +
                    "  id(\"net.microsoft.plugin.dotnetinterop\")\n" +
                    "}\n" +
                    "\n" +
                    "//configurations.implementation.canBeResolved = true\n" +
                    "\n" +
                    "android {\n" +
                    "    namespace = \"com.microsoft.maui.platform.channels.sample\"\n" +
                    "\n" +
                    "    compileSdk = 33\n" +
                    "\n" +
                    "    defaultConfig {\n" +
                    "        minSdk = 21\n" +
                    "\n" +
                    "        testInstrumentationRunner = \"androidx.test.runner.AndroidJUnitRunner\"\n" +
                    "    }\n" +
                    "\n" +
                    "    buildTypes {\n" +
                    "        getByName(\"release\") {\n" +
                    "            isMinifyEnabled = true\n" +
                    "            proguardFiles(\n" +
                    "                getDefaultProguardFile(\"proguard-android-optimize.txt\"),\n" +
                    "                \"proguard-rules.pro\"\n" +
                    "            )\n" +
                    "        }\n" +
                    "    }\n" +
                    "}\n" +
                    "\n" +
                    "\n" +
                    "\n" +
                    "\n" +
                    "\n" +
                    "dependencies {\n" +
                    "\n" +
                    "    implementation(\"androidx.appcompat:appcompat:1.6.1\")\n" +
                    "    implementation(\"com.google.android.material:material:1.9.0\")\n" +
                    "    testImplementation(\"junit:junit:4.13.2\")\n" +
                    "}");

        var runner = GradleRunner.create();

        var builder = runner.forwardOutput()
                .withPluginClasspath()
                .withArguments("dotnetInteropResolveArtifacts")
                .withProjectDir(moduleDir);

        var result = builder.build();
        // Run the build

        // Verify the result
        assertTrue(result.getOutput().contains("MAVEN: com.google.android.material.material"));
    }

    private void writeString(File file, String string) throws IOException {
        try (Writer writer = new FileWriter(file)) {
            writer.write(string);
        }
    }
}
