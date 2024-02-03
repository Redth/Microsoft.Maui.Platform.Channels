plugins {
    id("com.android.library")
    id("maven-publish")
}

android {
    namespace = "com.microsoft.dotnet.platformchannels"
    compileSdk = 33

    defaultConfig {
        minSdk = 23
        targetSdk = 33

        aarMetadata {
            minCompileSdk = 23
        }
    }

    buildTypes {
        getByName("release") {
            isMinifyEnabled = true
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }

    publishing {
        singleVariant("release") {
        }
    }
}

// Because the components are created only during the afterEvaluate phase, you must
// configure your publications using the afterEvaluate() lifecycle method.
afterEvaluate {
    publishing {
        publications {
            register<MavenPublication>("release") {
                groupId = "com.microsoft"
                artifactId = "dotnet.platformchannels"
                version = "0.0.1"

                afterEvaluate {
                    from(components["release"])
                }
            }
        }
        repositories {
            maven {
                name = "platformchannels"
                url = uri("${project.buildDir}/repo")
            }
        }
    }
}

dependencies {
    testImplementation("junit:junit:4.13.2")
}
