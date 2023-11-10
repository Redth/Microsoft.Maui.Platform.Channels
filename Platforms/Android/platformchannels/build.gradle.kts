plugins {
    id("com.android.library")
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
}

// Because the components are created only during the afterEvaluate phase, you must
// configure your publications using the afterEvaluate() lifecycle method.


dependencies {
    testImplementation("junit:junit:4.13.2")
}